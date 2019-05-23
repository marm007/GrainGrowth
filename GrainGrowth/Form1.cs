using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Config;

namespace GrainGrowth
{
    public partial class Form1 : Form
    {

        private static int SLEEP_TIME = 1;
        private static int SLEEP_TIME_MIN = 10;

        private System.Drawing.Pen circuitPen = new System.Drawing.Pen(Color.Black, 1);
        private System.Drawing.Pen circuitPenClear = new System.Drawing.Pen(SystemColors.Control, 1);
        private System.Drawing.SolidBrush cellBrushClear = new System.Drawing.SolidBrush(SystemColors.Control);

        bool flagStop = false;
        bool flagResize = false;
        bool isPlaying = false;

        bool clickedButton = false;

        private Grain[,] tab = null;

        Grid grid = null;

        Simulation grainGrowth = null;

        PictureBox pPictureBox = null;

        private BackgroundWorker backgroundWorker = null;
        private BackgroundWorker monteCarloWorker = null;

        Random rnd = new Random();

        private MonteCarlo monteCarlo = null;

        private Bitmap previousBitmap = null;


        public Form1()
        {
            InitializeComponent();

            Colors.Initialize();


            start_button.Enabled = true;
            stop_button.Enabled = false;
            clear_button.Enabled = false;
            
            monteCarloEnergyButton.Enabled = false;
            monteCarlo_Button.Enabled = false;
            monteCarloStopButton.Enabled = false;


            toolTip1.SetToolTip(rowUpDown, "Set number of rows");
            toolTip1.SetToolTip(colUpDown, "Set number of columns");
            toolTip1.SetToolTip(radiusUpDown, "Set radius");
            toolTip1.SetToolTip(numberRadialUpDown, "Set max number of elements");
            toolTip1.SetToolTip(numberRandomUpDown, "Set max number of random elements");

            hexagonalComboBox.Items.Add(HexagonalNeighbourhood.Left);
            hexagonalComboBox.Items.Add(HexagonalNeighbourhood.Right);
            hexagonalComboBox.Items.Add(HexagonalNeighbourhood.Random);

            hexagonalComboBox.SelectedItem = HexagonalNeighbourhood.Left;
            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;


        }



        private void Form1_Load(object sender, EventArgs e)
        {
            // setting new sizes

            SET_SIZES(pictureBox1.Width / CELL_SIZE, pictureBox1.Height / CELL_SIZE);

            // --------

            grid = new Grid();
            grainGrowth = new Simulation();
            monteCarlo = new MonteCarlo();

            Bitmap bitmap = new Bitmap(SIZE_X * CELL_SIZE, SIZE_Y * CELL_SIZE);
            pictureBox1.Image = bitmap;

            EnergyColors.InitializeColors();

            widthBox.Text = SIZE_X.ToString();
            heightBox.Text = SIZE_Y.ToString();
        }

        private void Form1_ResizeEnd(object sender, System.EventArgs e)
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image;


            if (!grid.ComputeBounds(pPictureBox, pictureBox1, grainGrowth))
            {
                flagResize = false;

                if (backgroundWorker != null && !flagStop)
                {
                    if (isPlaying)
                        Simulate();
                }

                return;
            }
            Bitmap bitmapNew = new Bitmap(SIZE_X * CELL_SIZE, SIZE_Y * CELL_SIZE);

            grainGrowth.Display(bitmapNew);
            pictureBox1.Image = bitmapNew;

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

            if (monteCarloWorker != null)
                monteCarloWorker.CancelAsync();
        }

     

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            int x = coordinates.X / CELL_SIZE;
            int y = coordinates.Y / CELL_SIZE;

            if (x >= SIZE_X || y >= SIZE_Y)
                return;

            Bitmap bitmap = (Bitmap)pictureBox1.Image;

            if (!isPlaying)
            {
                if (grainGrowth.Tab[y, x].State == 0)
                {

                    grainGrowth.Tab[y, x].State = Colors.RandomColor();
                    grainGrowth.Tab[y, x].Display(bitmap);

                }
                else
                {
                    grainGrowth.Tab[y, x].State = 0;
                    grainGrowth.Tab[y, x].Display(bitmap);
                }
            }
            else
            {
                this.tab = new Grain[SIZE_Y, SIZE_X];

                for (int i = 0; i < SIZE_Y; i++)
                {
                    for (int j = 0; j < SIZE_X; j++)
                    {
                        this.tab[i, j] = new Grain(j, i, 0);
                    }
                }

                if (grainGrowth.Tab[y, x].State == 0)
                {
                    this.tab[y, x].State = Colors.RandomColor();
                }
                else
                {
                    this.tab[y, x].State = 0;
                }

                clickedButton = true;
            }

            grainGrowth.Tab[y, x].DisplayEnergy(bitmap);

            pictureBox1.Image = bitmap;
          
        }


        private void start_button_Click(object sender, EventArgs e)
        {
            CheckEnergyButton();

            if (flagStop)
            {
                flagStop = false;
            }

            isPlaying = true;

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

            monteCarloEnergyButton.Enabled = true;
            monteCarlo_Button.Enabled = true;
            monteCarloStopButton.Enabled = false;

        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            if (backgroundWorker != null)
                backgroundWorker.CancelAsync();

            Bitmap bitmap = new Bitmap(SIZE_X * CELL_SIZE, SIZE_Y * CELL_SIZE);
            pictureBox1.Refresh();


            flagStop = false;

            start_button.Enabled = true;
            stop_button.Enabled = false;
            clear_button.Enabled = false;
            grid.Draw(pictureBox1);

            for (int i = 0; i < SIZE_Y; i++)
            {
                for (int j = 0; j < SIZE_X; j++)
                {
                    grainGrowth.Tab[i, j].State = 0;
                }
            }

            grainGrowth.DisplayEnergy(bitmap);
            pictureBox1.Image = bitmap;

            Colors.Initialize();
        }

        private void step_button_Click(object sender, EventArgs e)
        {
            CheckEnergyButton();

            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            Graphics g = Graphics.FromImage(bitmap);

            grainGrowth.Simulate(g);

            pictureBox1.Image = bitmap;
            clear_button.Enabled = true;
        }

        private void neumann_button_CheckedChanged(object sender, EventArgs e)
        {
            if (neumann_button.Checked)
                NEIGHBOURHOOD = Neighbourhood.vonNeumann;
        }

        private void moore_button_CheckedChanged(object sender, EventArgs e)
        {
            if (moore_button.Checked)
                NEIGHBOURHOOD = Neighbourhood.Moore;
        }


        private void pentagonalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (pentagonalRadioButton.Checked)
            {
                pentagonalLabel.Visible = true;
            }
            else
            {
                pentagonalLabel.Text = "";
                pentagonalLabel.Visible = false;
            }

        }

        private void hexagonalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (hexagonalRadioButton.Checked)
            {
                hexagonalComboBox.Enabled = true;

                HEXAGONAL_NEIGHBOURHOOD = (HexagonalNeighbourhood) hexagonalComboBox.SelectedItem;
                NEIGHBOURHOOD = Neighbourhood.Hexagonal;
            }
        }

        private void probability_up_down_ValueChanged(object sender, EventArgs e)
        {
        }

        private void nonperiodic_button_CheckedChanged(object sender, EventArgs e)
        {
            if(nonperiodic_button.Checked)
                BOUNDARY_CONDITION = BoundaryCondition.Nonperiodic;

        }

        private void periodic_button_CheckedChanged(object sender, EventArgs e)
        {
            if(periodic_button.Checked)
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


            Grain[,] tabTmp = grainGrowth.Tab;

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

            grainGrowth.Tab = new Grain[SIZE_Y, SIZE_X];

            int y = SIZE_Y < startSizeY ? SIZE_Y : startSizeY;
            int x = SIZE_X < startSizeX ? SIZE_X : startSizeX;


            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    grainGrowth.Tab[i, j] = tabTmp[i, j];
                }
            }

            Bitmap bitmap = (Bitmap)pictureBox1.Image;

            BackgroundWorker renderWroker = new BackgroundWorker();

            renderWroker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                grid.RenderGridAndRefresh(pictureBox1, bitmap);
                grainGrowth.Display(bitmap);

                pictureBox1.Image = bitmap;
            });

            renderWroker.RunWorkerAsync();
        }


        private void speedTracBar_Scroll(object sender, EventArgs e)
        {
            SLEEP_TIME = speedTracBar.Value * 10;
        }

        private void cellSizeTracBar_Scroll(object sender, EventArgs e)
        {
            CELL_SIZE = 2 * cellSizeTracBar.Value;


            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            grid.SetNewCellSizeAndDraw(pictureBox1, bitmap, grainGrowth);
            grainGrowth.DisplayEnergy(bitmap);

            pictureBox1.Image = bitmap;

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

            Bitmap bitmap = (Bitmap)pictureBox1.Image;

            if (!isPlaying)
            {
                Nucleation.Homogeneus(grid, grainGrowth, bitmap, row, col);
                pictureBox1.Image = bitmap;

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
            Bitmap bitmap = (Bitmap)pictureBox1.Image;

            if (!isPlaying)
            {
                Nucleation.Radial(grid, grainGrowth, r, number, bitmap, alertTextBox, this);
                pictureBox1.Image = bitmap;
            }
            else
            {
                this.tab = Nucleation.Radial(grid, grainGrowth, r, number, alertTextBox, this);
                clickedButton = true;

            }
        }

        private void random_button_Click(object sender, EventArgs e)
        {
            int number = Decimal.ToInt32(numberRandomUpDown.Value);
            Bitmap bitmap = (Bitmap)pictureBox1.Image;

            if (!isPlaying)
            {
                Nucleation.Random(grid, grainGrowth, number, bitmap);
                pictureBox1.Image = bitmap;
            }
            else
            {
                this.tab = Nucleation.Random(grid, grainGrowth, number);

                clickedButton = true;
            }
                   
        }


        private void Simulate()
        {
          

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
         

            backgroundWorker.DoWork += new DoWorkEventHandler(async (state, args) =>
            {
                while (true)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        break;
                    }


                    Bitmap bitmap = (Bitmap)pictureBox1.Image;

                    bitmap = await grainGrowth.Simulate(bitmap);

                    await Task.Run(() =>
                    {
                        pictureBox1.Image = bitmap;
                    });

                    int time = 0;

                    while (time * SLEEP_TIME_MIN <= SLEEP_TIME)
                    {
                        if (clickedButton)
                        {
                            for (int i = 0; i < SIZE_Y; i++)
                            {
                                for (int j = 0; j < SIZE_X; j++)
                                {
                                    if (grainGrowth.Tab[i, j].State == 0 && this.tab[i, j].State != 0)
                                    {
                                        grainGrowth.Tab[i, j] = this.tab[i, j];
                                        grainGrowth.Tab[i, j].Display(pictureBox1.CreateGraphics(), Graphics.FromImage(bitmap));
                                    }
                                }
                            }


                            clickedButton = false;
                        }

                        time++;

                        System.Threading.Thread.Sleep(SLEEP_TIME_MIN);
                    }

                  


                    if (grainGrowth.SimulationEnded())
                    {
                        SimulationEndedAction("finish");
                        break;

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
            Bitmap bitmap = (Bitmap)pictureBox1.Image;

            renderWroker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                grid.RenderGridAndRefresh(pictureBox1, bitmap);
                grainGrowth.Display(bitmap);
                grainGrowth.DisplayEnergy(bitmap);
            });

            renderWroker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object ss, RunWorkerCompletedEventArgs ee) =>
            {
                pictureBox1.Image = bitmap;
            });

            renderWroker.RunWorkerAsync();

        }

        private void Picturebox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image != null)
                return;
        }

        public void AlertTextBoxAction(string value, bool visibility)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, bool>(AlertTextBoxAction), new object[] { value, visibility });
                return;
            }
            alertTextBox.Text = value;
            alertTextBox.Visible = visibility;
        }

        public void SimulationEndedAction(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SimulationEndedAction), new object[] { value });
                return;
            }
            isPlaying = false;
            flagStop = true;

            cellSizeTracBar.Enabled = true;
            gridCheckBox.Enabled = true;

            start_button.Enabled = true;
            stop_button.Enabled = false;
            clear_button.Enabled = true;
            step_button.Enabled = true;

            monteCarloIterationsUpDown.Enabled = true;
            monteCarloStopButton.Enabled = false;
            monteCarloEnergyButton.Enabled = true;
            monteCarlo_Button.Enabled = true;
        }

        private void hexagonalComboBox_SelectedItemChanged(object sender, EventArgs e)
        {
            if (hexagonalComboBox.Enabled)
            {
                HEXAGONAL_NEIGHBOURHOOD = (HexagonalNeighbourhood)hexagonalComboBox.SelectedItem;
                NEIGHBOURHOOD = Neighbourhood.Hexagonal;
            }
        }

        private void pentagonalRadioButton_Click(object sender, MouseEventArgs e)
        {
            Array values = Enum.GetValues(typeof(PentagonalNeighbourhood));
            PENTAGONAL_NEIGHBOURHOOD = (PentagonalNeighbourhood)values.GetValue(rnd.Next(values.Length));
            pentagonalLabel.Text = PENTAGONAL_NEIGHBOURHOOD.ToString();
            NEIGHBOURHOOD = Neighbourhood.Pentagonal;
        }

        private void energyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ENERGY_STATE = ENERGY_STATE == EnergyState.Enable ? EnergyState.Disable : EnergyState.Enable;
            BackgroundWorker renderWroker = new BackgroundWorker();
            Bitmap bitmap = (Bitmap)pictureBox1.Image;

            renderWroker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                grid.RenderGridAndRefresh(pictureBox1, bitmap);
                grainGrowth.Display(bitmap);
                grainGrowth.DisplayEnergy(bitmap);

            });

            renderWroker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object ss, RunWorkerCompletedEventArgs ee) =>
            {
                pictureBox1.Image = bitmap;
            });

            renderWroker.RunWorkerAsync();
        }

        private void radialNeighbourhood_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radialNeighbourhood_RadioButton.Checked)
            {
                NEIGHBOURHOOD = Neighbourhood.Radial;
                radialNeighbourhood_UpDown.Enabled = true;
                RADIUS = (float) radialNeighbourhood_UpDown.Value;

            }
            else
            {
                radialNeighbourhood_UpDown.Enabled = false;
            }
        }

        private void radialNegihbourhoodUpDown_ValueChanged(object sender, EventArgs e)
        {
            RADIUS = (float)radialNeighbourhood_UpDown.Value;
        }

        private void monteCarlo_Button_Click(object sender, EventArgs e)
        {
            CheckEnergyButton();

            start_button.Enabled = false;
            stop_button.Enabled = false;
            clear_button.Enabled = false;
            step_button.Enabled = false;

            monteCarloIterationsUpDown.Enabled = false;
            monteCarloStopButton.Enabled = true;
            monteCarloEnergyButton.Enabled = false;
            monteCarlo_Button.Enabled = false;

            monteCarloWorker = new BackgroundWorker();
            monteCarloWorker.WorkerSupportsCancellation = true;

            monteCarloWorker.DoWork += new DoWorkEventHandler(async (state, args) =>
            {
                int iteartions = 0;
                while(iteartions < monteCarloIterationsUpDown.Value)
                {
                    if (monteCarloWorker.CancellationPending)
                    {
                        break;
                    }

                    Bitmap bitmap = (Bitmap)pictureBox1.Image;

                    bitmap = await monteCarlo.Simulate(grainGrowth, bitmap, pictureBox1.CreateGraphics());

                    await Task.Run(() =>
                    {
                        pictureBox1.Image = bitmap;
                    });

                    System.Threading.Thread.Sleep(SLEEP_TIME_MIN);

                    iteartions++;
                }

                MonteCarloEndedAction("finish");
            });

            monteCarloWorker.RunWorkerAsync();

         
        }

        private void monteCarloStopButton_Click(object sender, EventArgs e)
        {
            monteCarloWorker.CancelAsync();

            start_button.Enabled = true;
            stop_button.Enabled = false;
            clear_button.Enabled = true;
            step_button.Enabled = true;

            monteCarloIterationsUpDown.Enabled = true;
            monteCarloStopButton.Enabled = false;
            monteCarloEnergyButton.Enabled = true;
            monteCarlo_Button.Enabled = true;
        }

        private void monteCarloEnergyButton_Click(object sender, EventArgs e)
        {
            if(monteCarloEnergyButton.Text == "Energy / ON")
            {
                monteCarloEnergyButton.Text = "Energy / OFF";
                monteCarlo.CalculateEnergy(grainGrowth);
                previousBitmap = (Bitmap)pictureBox1.Image;
                monteCarlo.DisplayEnergy(grainGrowth.Tab, pictureBox1);
            }
            else
            {
                monteCarloEnergyButton.Text = "Energy / ON";

                pictureBox1.Image = previousBitmap;

            }


        }

        public void MonteCarloEndedAction(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(MonteCarloEndedAction), new object[] { value });
                return;
            }

            start_button.Enabled = true;
            stop_button.Enabled = false;
            clear_button.Enabled = true;
            step_button.Enabled = true;


            monteCarloIterationsUpDown.Enabled = true;
            monteCarloStopButton.Enabled = false;
            monteCarloEnergyButton.Enabled = true;
            monteCarlo_Button.Enabled = true;
        }

        public void CheckEnergyButton()
        {
            if (monteCarloEnergyButton.Text == "Energy / OFF")
            {
                monteCarloEnergyButton.Text = "Energy / ON";

                if (previousBitmap != null)
                    pictureBox1.Image = previousBitmap;
            }
        }
    }

   
}
