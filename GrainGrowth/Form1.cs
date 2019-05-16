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
        bool isPlaying = false;

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

            toolTip1.SetToolTip(rowUpDown, "Set number of rows");
            toolTip1.SetToolTip(colUpDown, "Set number of columns");
            toolTip1.SetToolTip(radiusUpDown, "Set radius");
            toolTip1.SetToolTip(numberRadialUpDown, "Set max number of elements");
            toolTip1.SetToolTip(numberRandomUpDown, "Set max number of random elements");
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            // setting new sizes

            SET_SIZES(pictureBox1.Width / CELL_SIZE, pictureBox1.Height / CELL_SIZE);

            // --------

            grid = new Grid();
            grainGrowth = new Simulation();

            widthBox.Text = SIZE_X.ToString();
            heightBox.Text = SIZE_Y.ToString();
        }

        private void Form1_ResizeEnd(object sender, System.EventArgs e)
        {
            g = pictureBox1.CreateGraphics();

            if (!grid.ComputeBounds(pPictureBox, pictureBox1, grainGrowth, g))
            {
                flagResize = false;

                if (backgroundWorker != null && !flagStop)
                {
                    if (isPlaying)
                        Simulate();
                }
                return;
            }

            grainGrowth.Display(pictureBox1.CreateGraphics());


            widthBox.Text = SIZE_X.ToString();
            heightBox.Text = SIZE_Y.ToString();

            if (flagResize && !flagStop)
            {
                flagResize = false;

                if(isPlaying)
                    Simulate();
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

            if (x >= SIZE_X || y >= SIZE_Y)
                return;

            if (!isPlaying)
            {
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
            else
            {
                this.tab = new int[SIZE_Y, SIZE_X];

                for (int i = 0; i < SIZE_Y; i++)
                {
                    for (int j = 0; j < SIZE_X; j++)
                    {
                        this.tab[i, j] = 0;
                    }
                }

                if (grainGrowth.Tab[y, x] == 0)
                {
                    Console.WriteLine("XAD");
                    this.tab[y, x] = Colors.RandomColor();
                }
                else
                {
                    this.tab[y, x] = 0;
                }

                clickedButton = true;
            }
          
        }


        private void start_button_Click(object sender, EventArgs e)
        {
            if (flagStop)
            {
                flagStop = false;
            }

            isPlaying = true;

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
            isPlaying = false;
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


            if(backgroundWorker != null)
                backgroundWorker.CancelAsync();

            pictureBox1.Refresh();
            g = pictureBox1.CreateGraphics();

            g.FillRectangle(cellBrushClear, 0, 0, SIZE_X * CELL_SIZE + 1, SIZE_Y * CELL_SIZE + 1);

            flagStop = false;

            start_button.Enabled = true;
            stop_button.Enabled = false;
            clear_button.Enabled = false;

            for (int i = 0; i < SIZE_Y; i++)
            {
                for (int j = 0; j < SIZE_X; j++)
                {
                    grainGrowth.Tab[i, j] = 0;
                }
            }

            Colors.Initialize();
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

            int startSizeX = SIZE_X;
            int startSizeY = SIZE_Y;


            int[,] tabTmp = grainGrowth.Tab;

            int sizeX = SIZE_X;
            int sizeY = SIZE_Y;


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

            // setting new sizes

            SET_SIZES(sizeX, sizeY);

            // --------

            grainGrowth.Tab = new int[SIZE_Y, SIZE_X];

            int y = SIZE_Y < startSizeY ? SIZE_Y : startSizeY;
            int x = SIZE_X < startSizeX ? SIZE_X : startSizeX;


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

            widthBox.Text = SIZE_X.ToString();
            heightBox.Text = SIZE_Y.ToString();
        }

        private void cellSizeTrackBar_MouseUp(object sender, MouseEventArgs e)
        {

            if (backgroundWorker != null && isPlaying)
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

            if (!isPlaying)
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

            if(!isPlaying)
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

            if (!isPlaying)
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
                            for (int i = 0; i < SIZE_Y; i++)
                            {
                                for (int j = 0; j < SIZE_X; j++)
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

        private void Picturebox1_Paint(object sender, PaintEventArgs e)
        {
            grid.Draw(e.Graphics);
            grainGrowth.Display(e.Graphics);
        }
    }
}
