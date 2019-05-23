using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
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


    public void Display(Bitmap bitmap)
    {
        Graphics g = Graphics.FromImage(bitmap);

        for (int i = 0; i < this.Tab.GetLength(0); i++)
        {
            for (int j = 0; j < this.Tab.GetLength(1); j++)
            {

                if (this.Tab[i, j].State != 0)
                {
                    this.Tab[i, j].Display(bitmap);
                }
            }
        }
    }

    public void DisplayEnergy(Bitmap bitmap)
    {
        if (ENERGY_STATE == EnergyState.Disable)
            return;


        Graphics g = Graphics.FromImage(bitmap);

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


    public void Simulate(Graphics g)
    {
        Grain[,] tabTmp = new Grain[SIZE_Y, SIZE_X];
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                tabTmp[i, j] = this.Tab[i, j].Copy();
                tabTmp[i, j].State = 0;
            }
        }


        switch (NEIGHBOURHOOD)
        {
            case Neighbourhood.Radial:
                tabTmp = ChangeStateRadial(g, tabTmp);
                break;
            default:
                tabTmp = ChangeState(g, tabTmp);
                break;
        }
       

        this.Tab = tabTmp;
    }

    public async Task<Bitmap> Simulate(Bitmap bitmap)
    {
        Graphics g = Graphics.FromImage(bitmap);

        return await Task.Run(() =>
        {
            Grain[,] tabTmp = new Grain[SIZE_Y, SIZE_X];
            for (int i = 0; i < SIZE_Y; i++)
            {
                for (int j = 0; j < SIZE_X; j++)
                {
                    tabTmp[i, j] = this.Tab[i, j].Copy();
                    tabTmp[i, j].State = 0;
                }
            }


            switch (NEIGHBOURHOOD)
            {
                case Neighbourhood.Radial:
                    tabTmp = ChangeStateRadial(g, tabTmp);
                    break;
                default:
                    tabTmp = ChangeState(g, tabTmp);
                    break;
            }

            this.Tab = tabTmp;
            return bitmap;
        });

    }


    private Grain[,] ChangeState(Graphics g, Grain[,] tabTmp)
    {
        NeighbourhoodAbstract neighbourhood = NeighbourhoodFactory.Create();

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                neighbourhood.GetNeighbours(this.Tab, j, i);

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

    private Grain[,] ChangeStateRadial(Graphics g, Grain[,] tabTmp)
    {
        NeighbourhoodAbstract neighbourhood = NeighbourhoodFactory.Create();

        for (int i = 0; i < SIZE_Y; i++)
        {
            if (BREAK_SIMULATION)
            {
                BREAK_SIMULATION = false;
                break;
            }

            for (int j = 0; j < SIZE_X; j++)
            {
                if (BREAK_SIMULATION)
                {
                    break;
                }

                if (this.Tab[i, j].State == 0)
                    continue;


                tabTmp[i, j] = this.Tab[i, j];


                neighbourhood.GetNeighbours(this.Tab, j, i);

                foreach (Grain grain in neighbourhood.NeighboursGrains)
                {
                    tabTmp[grain.Y, grain.X].State = this.Tab[i, j].State;

                    tabTmp[grain.Y, grain.X].Display(g);
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
