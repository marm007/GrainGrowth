using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static Config;

public class Simulation
{
    private static int DEAD = 0;

    private System.Drawing.Pen circuitPen = new System.Drawing.Pen(Color.Black, 1);
    private System.Drawing.SolidBrush cellBrushClear = new System.Drawing.SolidBrush(SystemColors.Control);

    private int[,] tab;

	public Simulation(int sizeX, int sizeY)
	{
        this.tab = new int[sizeY, sizeX];

        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                this.tab[i, j] = 0;
            }
        }
    }

    public int[,] Tab { get => tab; set => tab = value; }

    public void Display(Graphics g)
    {

        for (int i = 0; i < this.tab.GetLength(0); i++)
        {
            for (int j = 0; j < this.tab.GetLength(1); j++)
            {

                if (this.tab[i, j] != 0)
                {
                    System.Drawing.SolidBrush cellBrush = new System.Drawing.SolidBrush(Colors.colors[this.tab[i, j]]);

                    g.FillRectangle(cellBrush, j * CELL_SIZE + (int) GRID_STATE, i * CELL_SIZE + (int) GRID_STATE,
                        CELL_SIZE - (int) GRID_STATE, CELL_SIZE - (int) GRID_STATE);
                }
            }
        }
    }

    public void Display(Graphics g, int x, int y, int state)
    {
        
        System.Drawing.SolidBrush cellBrushClear = new System.Drawing.SolidBrush(SystemColors.Control);
        
          
        if(state == 0)
            g.FillRectangle(cellBrushClear, (x) * CELL_SIZE + (int) GRID_STATE, (y) * CELL_SIZE + (int) GRID_STATE,
                CELL_SIZE - (int) GRID_STATE, CELL_SIZE - (int) GRID_STATE);
        else
        {
            System.Drawing.SolidBrush cellBrush = new System.Drawing.SolidBrush(Colors.colors[state]);
            g.FillRectangle(cellBrush, (x) * CELL_SIZE + (int) GRID_STATE, (y) * CELL_SIZE + (int) GRID_STATE,
              CELL_SIZE - (int) GRID_STATE, CELL_SIZE - (int) GRID_STATE);
        }
    }


    public void Simulate(PictureBox p)
    {
        Graphics g = p.CreateGraphics();

        int sizeX = this.tab.GetLength(1);
        int sizeY = this.tab.GetLength(0);

        int[,] tabTmp = new int[sizeY, sizeX];

        if (NEIGHBOURHOOD == Neighbourhood.von_Neumann)
        {
           if(BOUNDARY_CONDITION == BoundaryCondition.Periodic)
            {
                tabTmp = Periodic_vonNeumann(g, sizeX, sizeY, tabTmp);
            }
            else if(BOUNDARY_CONDITION == BoundaryCondition.Nonperiodic)
            {
                tabTmp = Nonperiodic_vonNeumann(g, sizeX, sizeY, tabTmp);
            }
        }
        else if(NEIGHBOURHOOD == Neighbourhood.Moore)
        {
            if (BOUNDARY_CONDITION == BoundaryCondition.Periodic)
            {
                tabTmp = Periodic_Moore(g, sizeX, sizeY, tabTmp);
            }
            else if (BOUNDARY_CONDITION == BoundaryCondition.Nonperiodic)
            {
                tabTmp = Nonperiodic_Moore(g, sizeX, sizeY, tabTmp);
            }
        }

        this.tab = tabTmp;
    }

    private int[,] Periodic_vonNeumann(Graphics g, int sizeX, int sizeY, int[,] tabTmp)
    {
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {

                int s_l = -200;
                int s_p = -200;

                int s_d = -200;
                int s_g = -200;


                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? sizeX - 1 : j - 1;
                int x_p = j + 1 >= sizeX ? 0 : j + 1;
                int y_d = i + 1 >= sizeY ? 0 : i + 1;
                int y_g = i - 1 < 0 ? sizeY - 1 : i - 1;

                s_l = tab[i, x_l];
                s_p = tab[i, x_p];
                s_d = tab[y_d, j];
                s_g = tab[y_g, j];


                string[] neighbours = new string[] { s_l.ToString(), s_p.ToString(), s_d.ToString(),
                    s_g.ToString() };

                var count = new Dictionary<string, int>();

                foreach (string value in neighbours)
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

                tabTmp[i, j] = cellEnd;

                if (cellEnd != cellBegin)
                {
                    Display(g, j, i, cellEnd);
                }
            }
        }

        return tabTmp;
    }

    private int[,] Periodic_Moore(Graphics g, int sizeX, int sizeY, int[,] tabTmp)
    {
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {


                int s_l = -200;
                int s_p = -200;

                int s_d = -200;
                int s_g = -200;

                int s_g_l = -200;
                int s_g_p = -200;

                int s_d_l = -200;
                int s_d_p = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? sizeX - 1 : j - 1;
                int x_p = j + 1 >= sizeX ? 0 : j + 1;
                int y_d = i + 1 >= sizeY ? 0 : i + 1;
                int y_g = i - 1 < 0 ? sizeY - 1 : i - 1;

                s_l = tab[i, x_l];
                s_p = tab[i, x_p];
                s_d = tab[y_d, j];
                s_g = tab[y_g, j];

                s_d_l = tab[y_d, x_l];
                s_d_p = tab[y_d, x_p];

                s_g_l = tab[y_g, x_l];
                s_g_p = tab[y_g, x_p];


                string[] neighbours = new string[] { s_l.ToString(), s_p.ToString(), s_d.ToString(),
                    s_g.ToString(), s_d_l.ToString(), s_d_p.ToString(), s_g_l.ToString(), s_g_p.ToString() };

                var count = new Dictionary<string, int>();

                foreach (string value in neighbours)
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

                tabTmp[i, j] = cellEnd;

                if (cellEnd != cellBegin)
                {
                    Display(g, j, i, cellEnd);
                }
            }
        }

        return tabTmp;
    }



    private int[,] Nonperiodic_vonNeumann(Graphics g, int sizeX, int sizeY, int[,] tabTmp)
    {
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {


                int s_l = -200;
                int s_p = -200;

                int s_d = -200;
                int s_g = -200;


                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? j : j - 1;
                int x_p = j + 1 >= sizeX ? j : j + 1;
                int y_d = i + 1 >= sizeY ? i : i + 1;
                int y_g = i - 1 < 0 ? i : i - 1;

                s_l = tab[i, x_l];
                s_p = tab[i, x_p];
                s_d = tab[y_d, j];
                s_g = tab[y_g, j];




                string[] neighbours = new string[] { s_l.ToString(), s_p.ToString(), s_d.ToString(),
                    s_g.ToString() };

                var count = new Dictionary<string, int>();

                foreach (string value in neighbours)
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

                tabTmp[i, j] = cellEnd;

                if (cellEnd != cellBegin)
                {
                    Display(g, j, i, cellEnd);
                }
            }
        }

        return tabTmp;
    }

    private int[,] Nonperiodic_Moore(Graphics g, int sizeX, int sizeY, int[,] tabTmp)
    {
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {


                int s_l = -200;
                int s_p = -200;

                int s_d = -200;
                int s_g = -200;

                int s_g_l = -200;
                int s_g_p = -200;

                int s_d_l = -200;
                int s_d_p = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? j : j - 1;
                int x_p = j + 1 >= sizeX ? j : j + 1;
                int y_d = i + 1 >= sizeY ? i : i + 1;
                int y_g = i - 1 < 0 ? i : i - 1;

                s_l = tab[i, x_l];
                s_p = tab[i, x_p];
                s_d = tab[y_d, j];
                s_g = tab[y_g, j];

                s_d_l = tab[y_d, x_l];
                s_d_p = tab[y_d, x_p];

                s_g_l = tab[y_g, x_l];
                s_g_p = tab[y_g, x_p];


                string[] neighbours = new string[] { s_l.ToString(), s_p.ToString(), s_d.ToString(),
                    s_g.ToString(), s_d_l.ToString(), s_d_p.ToString(), s_g_l.ToString(), s_g_p.ToString() };

                var count = new Dictionary<string, int>();

                foreach (string value in neighbours)
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

                tabTmp[i, j] = cellEnd;

                if (cellEnd != cellBegin)
                {
                    Display(g, j, i, cellEnd);
                }
            }
        }

        return tabTmp;
    }

}
