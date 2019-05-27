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
        private static int SLEEP_TIME_MIN = 1;

        bool flagStop = false;
        bool flagResize = false;
        bool isPlaying = false;

        bool clickedButton = false;

        private Grain[,] tab = null;
        private Grid grid = null;
        private Simulation grainGrowth = null;
        private MonteCarlo monteCarlo = null;

        private PictureBox pPictureBox = null;

        private BackgroundWorker backgroundWorker = null;
        private BackgroundWorker monteCarloWorker = null;

        Random rnd = new Random();

        private Bitmap simulationBitmap = null;
        private Bitmap previousBitmap = null;
        private Graphics g = null;

        private Thread myUIthred;

        private int monteCarloIterations = 0;

        public Form1()
        {
            myUIthred = Thread.CurrentThread;
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
            // System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // setting new sizes

            SET_SIZES(pictureBox1.Width / CELL_SIZE, pictureBox1.Height / CELL_SIZE);

            // --------

            grid = new Grid();
            grainGrowth = new Simulation();
            monteCarlo = new MonteCarlo();

            simulationBitmap = new Bitmap(SIZE_X * CELL_SIZE, SIZE_Y * CELL_SIZE);
            g = Graphics.FromImage(simulationBitmap);

            pictureBox1.Image = simulationBitmap;

            EnergyColors.InitializeColors();

            widthBox.Text = SIZE_X.ToString();
            heightBox.Text = SIZE_Y.ToString();

        }

        private void Form1_ResizeEnd(object sender, System.EventArgs e)
        {


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

            simulationBitmap = new Bitmap(SIZE_X * CELL_SIZE, SIZE_Y * CELL_SIZE);
            g = Graphics.FromImage(simulationBitmap);

            grid.Draw(g);
            grainGrowth.Display(g);
            grainGrowth.DisplayEnergy(g);

            pictureBox1.Image = simulationBitmap;

            widthBox.Text = SIZE_X.ToString();
            heightBox.Text = SIZE_Y.ToString();

            if (flagResize && !flagStop)
            {
                flagResize = false;

                if (isPlaying)
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



            if (!isPlaying)
            {
                if (grainGrowth.Tab[y, x].State == 0)
                {

                    grainGrowth.Tab[y, x].State = Colors.RandomColor();
                    grainGrowth.Tab[y, x].Display(g);

                }
                else
                {
                    grainGrowth.Tab[y, x].State = 0;
                    grainGrowth.Tab[y, x].Display(g);
                }
            }
            else
            {
                this.tab = new Grain[SIZE_Y, SIZE_X];

                for (int i = 0; i < SIZE_Y; i++)
                {
                    for (int j = 0; j < SIZE_X; j++)
                    {
                        this.tab[i, j] = grainGrowth.Tab[i, j].Copy();
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

            grainGrowth.Tab[y, x].DisplayEnergy(g);

            pictureBox1.Image = simulationBitmap;

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

            monteCarloEnergyButton.Enabled = false;
            monteCarlo_Button.Enabled = false;
            monteCarloStopButton.Enabled = false;

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
            BREAK_SIMULATION = true;
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

            flagStop = false;

            start_button.Enabled = true;
            stop_button.Enabled = false;
            clear_button.Enabled = false;


            grainGrowth.Clear();

            simulationBitmap = new Bitmap(SIZE_X * CELL_SIZE, SIZE_Y * CELL_SIZE);
            g = Graphics.FromImage(simulationBitmap);

            grainGrowth.DisplayEnergy(g);
            grid.Draw(g, pictureBox1);

            pictureBox1.Image = simulationBitmap;

            Colors.Initialize();
        }

        private void step_button_Click(object sender, EventArgs e)
        {
            CheckEnergyButton();

            grainGrowth.Simulate(g);

            pictureBox1.Image = simulationBitmap;
            clear_button.Enabled = true;
        }





        private void nonperiodic_button_CheckedChanged(object sender, EventArgs e)
        {
            if (nonperiodic_button.Checked)
                BOUNDARY_CONDITION = BoundaryCondition.Nonperiodic;

        }

        private void periodic_button_CheckedChanged(object sender, EventArgs e)
        {
            if (periodic_button.Checked)
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
            for (int i = 0; i < SIZE_Y; i++)
            {
                for (int j = 0; j < SIZE_X; j++)
                {
                    grainGrowth.Tab[i, j] = new Grain(j, i, 0);
                }
            }

            int y = SIZE_Y < startSizeY ? SIZE_Y : startSizeY;
            int x = SIZE_X < startSizeX ? SIZE_X : startSizeX;


            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    grainGrowth.Tab[i, j] = tabTmp[i, j].Copy();
                }
            }

            simulationBitmap = new Bitmap(SIZE_X * CELL_SIZE, SIZE_Y * CELL_SIZE);
            g = Graphics.FromImage(simulationBitmap);

            BackgroundWorker renderWroker = new BackgroundWorker();

            renderWroker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                grid.RenderGridAndRefresh(pictureBox1, g);
                grainGrowth.Display(g);

                pictureBox1.Image = simulationBitmap;
            });

            renderWroker.RunWorkerAsync();
        }


        private void speedTracBar_Scroll(object sender, EventArgs e)
        {
            SLEEP_TIME = speedTracBar.Value * 10;
        }

        private void cellSizeTracBar_Scroll(object sender, EventArgs e)
        {
            CELL_SIZE = 1 * cellSizeTracBar.Value;

            int maxSizeX = pictureBox1.Width / CELL_SIZE;
            int maxSizeY = pictureBox1.Height / CELL_SIZE;

            simulationBitmap = new Bitmap(maxSizeX * CELL_SIZE, maxSizeY * CELL_SIZE);
            g = Graphics.FromImage(simulationBitmap);

            grid.SetNewCellSizeAndDraw(pictureBox1, g, grainGrowth);
            grainGrowth.DisplayEnergy(g);

            pictureBox1.Image = simulationBitmap;

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
                Nucleation.Homogeneus(grid, grainGrowth, g, row, col);
                pictureBox1.Image = simulationBitmap;

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

            if (!isPlaying)
            {
                Nucleation.Radial(grid, grainGrowth, r, number, g, alertTextBox, this);
                pictureBox1.Image = simulationBitmap;
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

            if (!isPlaying)
            {
                Nucleation.Random(grid, grainGrowth, number, g);
                pictureBox1.Image = simulationBitmap;
            }
            else
            {
                this.tab = Nucleation.Random(grid, grainGrowth, number);

                clickedButton = true;
            }

        }


        private void Simulate()
        {
            Stopwatch stopwatch = new Stopwatch();

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;

            backgroundWorker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                stopwatch.Start();

                while (true)
                {
                    try
                    {
                        SetBitmapOnUIThread(null);

                        grainGrowth.Simulate(g);

                        if (backgroundWorker.CancellationPending)
                        {
                            SetBitmapOnUIThread(null);

                            break;
                        }

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
                                            grainGrowth.Tab[i, j].Display(pictureBox1.CreateGraphics(), g);
                                        }
                                    }
                                }


                                clickedButton = false;
                            }

                            time++;

                            System.Threading.Thread.Sleep(SLEEP_TIME_MIN);
                        }

                        SetBitmapOnUIThread(null);


                        if (grainGrowth.SimulationEnded())
                        {
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            ts.Hours, ts.Minutes, ts.Seconds,
                            ts.Milliseconds / 10);
                            Console.WriteLine("RunTime " + elapsedTime);
                            SimulationEndedAction("finish");
                            break;
                        }


                    }
                    catch (InvalidOperationException invalidOperation)
                    {
                        Console.WriteLine(invalidOperation);
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
                grid.RenderGridAndRefresh(pictureBox1, g);
                grainGrowth.Display(g);
                grainGrowth.DisplayEnergy(g);
            });

            renderWroker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object ss, RunWorkerCompletedEventArgs ee) =>
            {
                pictureBox1.Image = simulationBitmap;
            });

            renderWroker.RunWorkerAsync();

        }

        private void Picturebox1_Paint(object sender, PaintEventArgs e)
        {
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

            renderWroker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                grid.RenderGridAndRefresh(pictureBox1, g);
                grainGrowth.Display(g);
                grainGrowth.DisplayEnergy(g);

            });

            renderWroker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object ss, RunWorkerCompletedEventArgs ee) =>
            {
                pictureBox1.Image = simulationBitmap;
            });

            renderWroker.RunWorkerAsync();
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

            if (monteCarloIterationsUpDown.Value == monteCarloIterations)
                monteCarloIterations = 0;

            if (monteCarloIterations < monteCarloIterationsUpDown.Value)
                iterationLabel.Text = (monteCarloIterations + 1) + " / " + monteCarloIterationsUpDown.Value + " iteration";
            else
                iterationLabel.Text = (monteCarloIterations) + " / " + monteCarloIterationsUpDown.Value + " iteration";

            monteCarloWorker = new BackgroundWorker();
            monteCarloWorker.WorkerSupportsCancellation = true;

            monteCarloWorker.DoWork += new DoWorkEventHandler((state, args) =>
           {


               while (monteCarloIterations < monteCarloIterationsUpDown.Value)
               {
                   try
                   {

                       monteCarlo.Simulate(grainGrowth, g, pictureBox1.CreateGraphics());

                       if (monteCarloWorker.CancellationPending)
                       {

                           break;
                       }

                       System.Threading.Thread.Sleep(SLEEP_TIME_MIN);

                       SetBitmapOnUIThread(null);

                   }
                   catch (InvalidOperationException invalidOperation)
                   {

                       Console.WriteLine(monteCarloIterations);
                       Console.WriteLine(invalidOperation);
                   }

                   monteCarloIterations++;
                   MonteCarloAction(monteCarloIterations.ToString());

               }

               MonteCarloAction("finish");

           });

            monteCarloWorker.RunWorkerAsync();


        }

        private void monteCarloStopButton_Click(object sender, EventArgs e)
        {
            BREAK_SIMULATION = true;

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
            if (monteCarloEnergyButton.Text == "Energy / ON")
            {
                monteCarloEnergyButton.Text = "Energy / OFF";
                monteCarlo.CalculateEnergy(grainGrowth);
                previousBitmap = (Bitmap)pictureBox1.Image.Clone();
                monteCarlo.DisplayEnergy(grainGrowth.Tab, Graphics.FromImage(previousBitmap));
                pictureBox1.Image = previousBitmap;

            }
            else
            {
                monteCarloEnergyButton.Text = "Energy / ON";
                pictureBox1.Image = simulationBitmap;

            }


        }

        public void MonteCarloAction(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(MonteCarloAction), new object[] { value });
                return;
            }

            if (value == "finish")
            {

                start_button.Enabled = true;
                stop_button.Enabled = false;
                clear_button.Enabled = true;
                step_button.Enabled = true;


                monteCarloIterationsUpDown.Enabled = true;
                monteCarloStopButton.Enabled = false;
                monteCarloEnergyButton.Enabled = true;
                monteCarlo_Button.Enabled = true;
            }
            else
            {
                if (int.Parse(value) < monteCarloIterationsUpDown.Value)
                    iterationLabel.Text = (int.Parse(value) + 1) + " / " + monteCarloIterationsUpDown.Value + " iteration";
                else
                    iterationLabel.Text = (int.Parse(value)) + " / " + monteCarloIterationsUpDown.Value + " iteration";
            }

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


        public void SetBitmapOnUIThread(Bitmap bitmap)
        {
            if (Thread.CurrentThread != myUIthred)
            {
                BeginInvoke(new Action<Bitmap>(SetBitmapOnUIThread), new object[] { bitmap });
                return;
            }

            try
            {
                pictureBox1.Image = simulationBitmap;
            }
            catch (InvalidOperationException invalidOperation)
            {
                Console.WriteLine(invalidOperation);
            }
        }

        private void monteCarloIterationsUpDown_ValueChanged(object sender, EventArgs e)
        {
            monteCarloIterations = 0;

            iterationLabel.Text = monteCarloIterations + " / " + monteCarloIterationsUpDown.Value + " iteration";

        }

        private void kTNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            monteCarlo.KT = (float)kTNumericUpDown.Value;

        }

        private void JNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            monteCarlo.JH = (float)JNumericUpDown.Value;
        }



        private void neighbourhoodGropuBox_CheckedChange(object sender, EventArgs e)
        {
            RadioButton neighbourhoodRadioButton = (RadioButton)sender;

            if (neighbourhoodRadioButton.Checked)
            {
                switch (neighbourhoodRadioButton.Name)
                {
                    case "neumann_button":
                        NEIGHBOURHOOD = Neighbourhood.vonNeumann;
                        break;
                    case "moore_button":
                        NEIGHBOURHOOD = Neighbourhood.Moore;
                        break;
                    case "pentagonalRadioButton":
                        pentagonalLabel.Visible = true;
                        break;
                    case "hexagonalRadioButton":
                        hexagonalComboBox.Enabled = true;

                        HEXAGONAL_NEIGHBOURHOOD = (HexagonalNeighbourhood)hexagonalComboBox.SelectedItem;
                        NEIGHBOURHOOD = Neighbourhood.Hexagonal;
                        break;
                    case "radialNeighbourhood_RadioButton":
                                NEIGHBOURHOOD = Neighbourhood.Radial;
                                radialNeighbourhood_UpDown.Enabled = true;
                                RADIUS = (float)radialNeighbourhood_UpDown.Value;                 
                        break;
                }
            }
            else
            {
                switch (neighbourhoodRadioButton.Name)
                {
                    case "pentagonalRadioButton":
                        pentagonalLabel.Text = "";
                        pentagonalLabel.Visible = false;
                        break;
                    case "radialNeighbourhood_RadioButton":
                        radialNeighbourhood_UpDown.Enabled = false;
                        break;
                }
            }
        }
    }

}
