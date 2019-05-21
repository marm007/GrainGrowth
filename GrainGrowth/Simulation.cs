using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static Config;

public class Simulation
{
    private static int DEAD = 0;

    private System.Drawing.Pen circuitPen = new System.Drawing.Pen(Color.Black, 1);
    private System.Drawing.SolidBrush cellBrushClear = new System.Drawing.SolidBrush(SystemColors.Control);

    
    private Grain[,] tab;

    internal Grain[,] Tab { get => tab; set => tab = value; }

    public Simulation()
	{
        this.Tab = new Grain[SIZE_Y, SIZE_X];

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                this.Tab[i, j] = new Grain(j, i, 0);
            }
        }
    }


    public void Display(Graphics g)
    {

        for (int i = 0; i < this.Tab.GetLength(0); i++)
        {
            for (int j = 0; j < this.Tab.GetLength(1); j++)
            {

                if (this.Tab[i, j].State != 0)
                {
                    this.Tab[i, j].Display(g);
                }
            }
        }
    }

    public void DisplayEnergy(Graphics g)
    {
        if (ENERGY_STATE == EnergyState.Disable)
            return;

        System.Drawing.Pen pen = new System.Drawing.Pen(Color.Red, 1);
        System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(Color.Red);

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                g.FillEllipse(brush, this.tab[i, j].EnergyCoords.X - RADIUS / 2.0f, this.tab[i, j].EnergyCoords.Y - RADIUS / 2.0f, RADIUS, RADIUS);
            }
        }
    }

    public void Simulate(PictureBox p)
    {
        Graphics g = p.CreateGraphics();

        Grain[,] tabTmp = new Grain[SIZE_Y, SIZE_X];
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                tabTmp[i, j] = new Grain(j, i, 0);
            }
        }

        // Console.WriteLine(BOUNDARY_CONDITION);
        // Console.WriteLine(NEIGHBOURHOOD);

        switch (NEIGHBOURHOOD)
        {
            case Neighbourhood.Radial:
                switch (BOUNDARY_CONDITION)
                {
                    case BoundaryCondition.Nonperiodic:

                        tabTmp = Nonperiodic_Radial(g, tabTmp);
                        break;

                    case BoundaryCondition.Periodic:
                        tabTmp = Periodic_Radial(g, tabTmp);
                        break;
                }
                break;
            default:
                tabTmp = Oblicz(g, tabTmp);
                break;
        }
       

        this.Tab = tabTmp;
    }


    private Grain[,] Oblicz(Graphics g, Grain[,] tabTmp)
    {
        NeighbourhoodAbstract neighbourhood = NeighbourhoodFactory.Create();

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                neighbourhood.SetNeighbours(this.Tab, j, i);

                int cellBegin = this.Tab[i, j].State;
                int cellEnd = this.Tab[i, j].State;

              
                var count = new Dictionary<string, int>();

                foreach (string value in neighbourhood.Neighbours)
                {
                    if (value != DEAD.ToString())
                    {
                        if (count.ContainsKey(value))
                        {
                            count[value]++;
                        }
                        else
                        {
                            count.Add(value, 1);
                        }
                    }
                }

                int highestCount = 0;

                List<string> mostCommon = new List<string>();

                foreach (KeyValuePair<string, int> pair in count)
                {
                    if (pair.Value > highestCount)
                    {
                        mostCommon = new List<string>();
                        mostCommon.Add(pair.Key);

                        highestCount = pair.Value;
                    }
                    else if (pair.Value == highestCount)
                    {
                        mostCommon.Add(pair.Key);
                    }
                }

                if (mostCommon.Count != 0 && cellBegin == DEAD)
                {
                    Random random = new Random();
                    int index = random.Next(mostCommon.Count);

                    cellEnd = int.Parse(mostCommon[index]);
                }

                tabTmp[i, j].State = cellEnd;

                if (cellEnd != cellBegin)
                {
                    tabTmp[i, j].Display(g);
                }
            }
        }

        return tabTmp;
    }


    private Grain[,] Periodic_Radial(Graphics g, Grain[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                if (this.Tab[i, j].State == 0)
                    continue;

                tabTmp[i, j] = this.Tab[i, j];

                float x_sr = this.Tab[i, j].EnergyCoords.X;
                float y_sr = this.Tab[i, j].EnergyCoords.Y;

                int maxY = (int) Math.Ceiling((double)(i + RADIUS >= SIZE_Y ? SIZE_Y - 1 : i + RADIUS));
                int maxX = (int) Math.Ceiling((double)(j + RADIUS >= SIZE_X ? SIZE_X - 1 : j + RADIUS));
                int minY = (int) Math.Floor((double)(i - RADIUS < 0 ? 0 : i - RADIUS));
                int minX = (int) Math.Floor((double)(j - RADIUS < 0 ? 0 : j - RADIUS));

                for (int a = (int)(-RADIUS); a <= (int)(RADIUS); ++a) 
                {
                    for (int b = (int)(-RADIUS); b <= (int)(RADIUS); ++b) 
                    {
                        int x = j + b >= SIZE_X ? (0 + (j + b) - SIZE_X) : (j + b < 0 ? (SIZE_X + (j + b)) : j + b);
                        int y = i + a >= SIZE_Y ? (0 + (i + a) - SIZE_Y) : (i + a < 0 ? (SIZE_Y + (i + a)) : i + a);
                        
                        bool isGreaterX = j + b >= SIZE_X;
                        bool isLowerX = j + b < 0;
                        bool isGreaterY = i + b >= SIZE_Y;
                        bool isLowerY = i + b < 0;
                        // Console.WriteLine("j = " + j + " b + " + b);
                        // Console.WriteLine(j+b>=SIZE_X);
                        // Console.WriteLine("x = " + x + " y = " + y);

                        float x_r = this.Tab[y, x].EnergyCoords.X;
                        float y_r = this.Tab[y, x].EnergyCoords.Y;

                        if (isGreaterX)
                        {
                            x_r = x_r + CELL_SIZE * SIZE_X;
                        }

                        if (isGreaterY)
                        {
                            y_r = y_r + CELL_SIZE * SIZE_Y;
                        }

                        if (isLowerX)
                        {
                            x_r = x_r - CELL_SIZE * SIZE_X;

                        }

                        if (isLowerY)
                        {
                            y_r = y_r - CELL_SIZE * SIZE_Y;

                        }

                        //Console.WriteLine("x = " + x + " y = " + y);
                        //Console.WriteLine("x_r = " + x_r + " y_r = " + y_r);

                        if (this.Tab[y, x].State == 0 && tabTmp[y, x].State == 0)
                        {
                            if (Math.Sqrt(Math.Pow((double)(x_sr - x_r), 2) + Math.Pow((double)(y_sr - y_r), 2)) < RADIUS * CELL_SIZE)
                            {
                                tabTmp[y, x].State = this.Tab[i, j].State;

                                tabTmp[y, x].Display(g);

                                this.Tab[i,j].DisplayEnergy(g);
                            }
                        }
                    }
                }
            }
        }

        return tabTmp;
    }


    private Grain[,] Nonperiodic_Radial(Graphics g, Grain[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                if (this.Tab[i, j].State == 0)
                    continue;

                tabTmp[i, j] = this.Tab[i, j];

                float x_sr = this.Tab[i, j].EnergyCoords.X;
                float y_sr = this.Tab[i, j].EnergyCoords.Y;

                int maxY = (int) Math.Ceiling((double) (i + RADIUS >= SIZE_Y ? SIZE_Y - 1 : i + RADIUS));
                int maxX = (int) Math.Ceiling((double)(j + RADIUS >= SIZE_X ? SIZE_X - 1 : j + RADIUS));
                int minY = (int) Math.Floor((double)(i - RADIUS < 0 ? 0 : i - RADIUS));
                int minX = (int) Math.Floor((double)(j - RADIUS < 0 ? 0 : j - RADIUS));


                for (int x = minX; x <= maxX; ++x)
                {
                    for (int y = minY; y <= maxY; ++y)
                    {
                        if (this.Tab[y, x].State == 0 && tabTmp[y, x].State == 0)
                        {
                            if(Math.Sqrt( Math.Pow((double)(x_sr - this.Tab[y, x].EnergyCoords.X ), 2) + Math.Pow((double)(y_sr - this.Tab[y, x].EnergyCoords.Y), 2)) < RADIUS * CELL_SIZE)
                            {
                                tabTmp[y, x].State = this.Tab[i, j].State;

                                tabTmp[y, x].Display(g);

                                this.Tab[i, j].DisplayEnergy(g);
                            }
                        }
                    }
                }
            }
        }

        return tabTmp;
    }

    public bool SimulationEnded()
    {
        bool simulationEnded = true;

        for(int y = 0; y < SIZE_Y; y++)
        {
            for(int x = 0; x < SIZE_X; x++)
            {
                if (Tab[y, x].State == 0)
                {
                    simulationEnded = false;
                    break;

                }
            }

            if (!simulationEnded)
                break;
        }

        return simulationEnded;
    }


}
