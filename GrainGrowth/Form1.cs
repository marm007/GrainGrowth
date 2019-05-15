using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Config;

namespace GrainGrowth
{
    public partial class Form1 : Form
    {

        private static int SLEEP_TIME = 1;
        private static int SLEEP_TIME_MIN = 10;

        private System.Drawing.Graphics g;
        private System.Drawing.Pen circuitPen = new System.Drawing.Pen(Color.Black, 1);
        private System.Drawing.Pen circuitPenClear = new System.Drawing.Pen(SystemColors.Control, 1);
        private System.Drawing.SolidBrush cellBrushClear = new System.Drawing.SolidBrush(SystemColors.Control);

        bool flagStop = false;
        bool flagResize = false;
        bool isPlayling = false;

        bool clickedButton = false;

        private int[,] tab = null;

        Grid grid = null;

        Simulation grainGrowth = null;

        PictureBox pPictureBox = null;

        private BackgroundWorker backgroundWorker = null;

        Random rnd = new Random();


        public Form1()
        {
            InitializeComponent();

            Colors.Initialize();

            g = pictureBox1.CreateGraphics();

            start_button.Enabled = true;
            stop_button.Enabled = false;
            clear_button.Enabled = false;

            toolTip1.SetToolTip(rowUpDown, "Set max number of rows");
            toolTip1.SetToolTip(colUpDown, "Set max number of cols");
            toolTip1.SetToolTip(radiusUpDown, "Set radius");
            toolTip1.SetToolTip(numberRadialUpDown, "Set max number of elements");
            toolTip1.SetToolTip(numberRandomUpDown, "Set max number of random elements");
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            grid = new Grid(pictureBox1);
            grainGrowth = new Simulation(grid.SizeX, grid.SizeY);

            widthBox.Text = grid.SizeX.ToString();
            heightBox.Text = grid.SizeY.ToString();
        }

        private void Form1_ResizeEnd(object sender, System.EventArgs e)
        {
            g = pictureBox1.CreateGraphics();

            if (!grid.ComputeBounds(pPictureBox, pictureBox1, grainGrowth, g))
            {
                flagResize = false;

                if (backgroundWorker != null && !flagStop)
                {
                    isPlayling = true;

                    Simulate();
                }
                return;
            }

            grainGrowth.Display(pictureBox1.CreateGraphics());


            widthBox.Text = grid.SizeX.ToString();
            heightBox.Text = grid.SizeY.ToString();

            if (flagResize && !flagStop)
            {
                flagResize = false;
                isPlayling = true;

                Simulate();
            }
            else
            {
                flagResize = false;
            }
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {

            pPictureBox = new PictureBox();
            pPictureBox.Width = pictureBox1.Width;
            pPictureBox.Height = pictureBox1.Height;

            if (backgroundWorker != null && !flagResize && !flagStop)
            {
                flagResize = true;
                backgroundWorker.CancelAsync();
            }
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker != null)
                backgroundWorker.CancelAsync();
        }

     

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            int x = coordinates.X / CELL_SIZE;
            int y = coordinates.Y / CELL_SIZE;

            if (x >= grid.SizeX || y >= grid.SizeY)
                return;

            if (grainGrowth.Tab[y, x] == 0)
            {

                grainGrowth.Tab[y, x] = Colors.RandomColor();

                grainGrowth.Display(pictureBox1.CreateGraphics(), x, y, grainGrowth.Tab[y, x]);
                
            }
            else
            {
                grainGrowth.Tab[y, x] = 0;
                grainGrowth.Display(pictureBox1.CreateGraphics(), x, y, grainGrowth.Tab[y, x]);
            }
        }


        private void start_button_Click(object sender, EventArgs e)
        {
            if (flagStop)
            {
                flagStop = false;
            }

            isPlayling = true;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;

            Simulate();

            cellSizeTracBar.Enabled = false;
            gridCheckBox.Enabled = false;

            start_button.Enabled = false;
            stop_button.Enabled = true;
            clear_button.Enabled = false;
            step_button.Enabled = false;
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            isPlayling = false;
            flagStop = true;
            backgroundWorker.CancelAsync();

            cellSizeTracBar.Enabled = true;
            gridCheckBox.Enabled = true;

            start_button.Enabled = true;
            stop_button.Enabled = false;
            clear_button.Enabled = true;
            step_button.Enabled = true;
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            Colors.Initialize();

            int sizeX = grid.SizeX;
            int sizeY = grid.SizeY;

            if(backgroundWorker != null)
                backgroundWorker.CancelAsync();

            pictureBox1.Refresh();

            flagStop = false;

            start_button.Enabled = true;
            stop_button.Enabled = false;
            clear_button.Enabled = false;

            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    grainGrowth.Tab[i, j] = 0;
                }
            }
        }

        private void step_button_Click(object sender, EventArgs e)
        {
            grainGrowth.Simulate(pictureBox1);
            clear_button.Enabled = true;
        }

        private void neumann_button_CheckedChanged(object sender, EventArgs e)
        {
            NEIGHBOURHOOD = Neighbourhood.von_Neumann;
        }

        private void moore_button_CheckedChanged(object sender, EventArgs e)
        {
            NEIGHBOURHOOD = Neighbourhood.Moore;
        }

        private void probability_up_down_ValueChanged(object sender, EventArgs e)
        {
        }

        private void nonperiodic_button_CheckedChanged(object sender, EventArgs e)
        {
            BOUNDARY_CONDITION = BoundaryCondition.Nonperiodic;

        }

        private void periodic_button_CheckedChanged(object sender, EventArgs e)
        {
            BOUNDARY_CONDITION = BoundaryCondition.Periodic;
        }


        private void applySettings_Click(object sender, EventArgs e)
        {
            flagStop = true;

            if (backgroundWorker != null)
                backgroundWorker.CancelAsync();

            start_button.Enabled = true;
            stop_button.Enabled = false;
            clear_button.Enabled = true;

            int maxSizeX = pictureBox1.Width / CELL_SIZE;
            int maxSizeY = pictureBox1.Height / CELL_SIZE;

            int startSizeX = grid.SizeX;
            int startSizeY = grid.SizeY;

            int sizeX = grid.SizeX;
            int sizeY = grid.SizeY;

            int[,] tabTmp = grainGrowth.Tab;



            bool success = Int32.TryParse(this.widthBox.Text, out sizeX);
            if (!success)
            {
                sizeX = maxSizeX;
                widthBox.Text = sizeX.ToString();
            }

            if (sizeX > maxSizeX || sizeX <= 0)
            {
                sizeX = maxSizeX;
                widthBox.Text = sizeX.ToString();
            }

            success = Int32.TryParse(this.heightBox.Text, out sizeY);
            if (!success)
            {
                sizeY = maxSizeY;
                heightBox.Text = sizeY.ToString();
            }

            if (sizeY > maxSizeY || sizeY <= 0)
            {
                sizeY = maxSizeY;
                heightBox.Text = sizeY.ToString();
            }

            grid.SizeX = sizeX;
            grid.SizeY = sizeY;

            grainGrowth.Tab = new int[sizeY, sizeX];

            int y = sizeY < startSizeY ? sizeY : startSizeY;
            int x = sizeX < startSizeX ? sizeX : startSizeX;


            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    grainGrowth.Tab[i, j] = tabTmp[i, j];
                }
            }


            BackgroundWorker renderWroker = new BackgroundWorker();

            renderWroker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                grid.RenderGridAndRefresh(pictureBox1.CreateGraphics(), pictureBox1);
                grainGrowth.Display(pictureBox1.CreateGraphics());
            });

            renderWroker.RunWorkerAsync();
        }


        private void speedTracBar_Scroll(object sender, EventArgs e)
        {
            SLEEP_TIME = speedTracBar.Value * 10;
        }

        private void cellSizeTracBar_Scroll(object sender, EventArgs e)
        {
            CELL_SIZE = 4 * cellSizeTracBar.Value;
            grid.SetNewCellSizeAndDraw(pictureBox1.CreateGraphics(), pictureBox1, grainGrowth);

            widthBox.Text = grid.SizeX.ToString();
            heightBox.Text = grid.SizeY.ToString();
        }

        private void cellSizeTrackBar_MouseUp(object sender, MouseEventArgs e)
        {

            if (backgroundWorker != null && isPlayling)
            {
                Simulate();
            }
        }

        private void cellSizeTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (backgroundWorker != null)
            {
                backgroundWorker.CancelAsync();
            }
        }


        private void homogeneus_button_Click(object sender, EventArgs e)
        {
            int row = Decimal.ToInt32(rowUpDown.Value);
            int col = Decimal.ToInt32(colUpDown.Value);

            if (!isPlayling)
            {
                Nucleation.Homogeneus(grid, grainGrowth, pictureBox1, row, col);
            }
            else
            {
                this.tab = Nucleation.Homogeneus(grid, grainGrowth, row, col);
                clickedButton = true;
            }


        }

        private void radial_button_Click(object sender, EventArgs e)
        {
            int r = Decimal.ToInt32(radiusUpDown.Value);
            int number = Decimal.ToInt32(numberRadialUpDown.Value);

            if(!isPlayling)
            {
                Nucleation.Radial(grid, grainGrowth, r, number, pictureBox1); 
            }
            else
            {
                this.tab = Nucleation.Radial(grid, grainGrowth, r, number);
                clickedButton = true;

            }
        }

        private void random_button_Click(object sender, EventArgs e)
        {
            int number = Decimal.ToInt32(numberRandomUpDown.Value);

            if (!isPlayling)
            {
                Nucleation.Random(grid, grainGrowth, number, pictureBox1);    
            }
            else
            {
                this.tab = Nucleation.Random(grid, grainGrowth, number);

                clickedButton = true;
            }
                   
        }


        private void Simulate()
        {
            backgroundWorker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                while (true)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        break;
                    }

                    grainGrowth.Simulate(pictureBox1);

                    int time = 0;

                    while (time * SLEEP_TIME_MIN <= SLEEP_TIME)
                    {
                        if (clickedButton)
                        {
                            for (int i = 0; i < grid.SizeY; i++)
                            {
                                for (int j = 0; j < grid.SizeX; j++)
                                {
                                    if (grainGrowth.Tab[i, j] == 0 && this.tab[i, j] != 0)
                                    {
                                        grainGrowth.Tab[i, j] = this.tab[i, j];
                                        grainGrowth.Display(pictureBox1.CreateGraphics(), j, i, this.tab[i, j]);
                                    }
                                }
                            }


                            clickedButton = false;
                        }

                        time++;

                        System.Threading.Thread.Sleep(SLEEP_TIME_MIN);
                    }
                }
            });

            if (!backgroundWorker.IsBusy)
                backgroundWorker.RunWorkerAsync();
        }

        private void gridCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GRID_STATE = GRID_STATE == GridState.Enable ? GridState.Disable : GridState.Enable;
            BackgroundWorker renderWroker = new BackgroundWorker();

            renderWroker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                grid.RenderGridAndRefresh(pictureBox1.CreateGraphics(), pictureBox1);
                grainGrowth.Display(pictureBox1.CreateGraphics());
            });

            renderWroker.RunWorkerAsync();

        }
    }
}
