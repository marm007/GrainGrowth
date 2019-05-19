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

    private int[,] tab;

	public Simulation()
	{
        this.tab = new int[SIZE_Y, SIZE_X];

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                this.tab[i, j] = 0;
            }
        }
    }

    public int[,] Tab { get { return tab; } set { tab = value; } }

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


    public void Simulate(PictureBox p, GrainEnergy energy)
    {
        Graphics g = p.CreateGraphics();

        int[,] tabTmp = new int[SIZE_Y, SIZE_X];

        Console.WriteLine(BOUNDARY_CONDITION);
        Console.WriteLine(NEIGHBOURHOOD);

        switch (BOUNDARY_CONDITION)
        {
            case BoundaryCondition.Nonperiodic:

                switch (NEIGHBOURHOOD)
                {
                    case Neighbourhood.von_Neumann:
                        tabTmp = Nonperiodic_vonNeumann(g, tabTmp);
                        break;

                    case Neighbourhood.Moore:
                        tabTmp = Nonperiodic_Moore(g, tabTmp);
                        break;

                    case Neighbourhood.Pentagonal:
                        switch (PENTAGONAL_NEIGHBOURHOOD)
                        {
                            case PentagonalNeighbourhood.Bottom:
                                tabTmp = Nonperiodic_PentagonalBottom(g, tabTmp);
                                break;

                            case PentagonalNeighbourhood.Top:
                                tabTmp = Nonperiodic_PentagonalTop(g, tabTmp);
                                break;

                            case PentagonalNeighbourhood.Left:
                                tabTmp = Nonperiodic_PentagonalLeft(g, tabTmp);
                                break;

                            case PentagonalNeighbourhood.Right:
                                tabTmp = Nonperiodic_PentagonalRight(g, tabTmp);
                                break;
                        }
                        break;

                    case Neighbourhood.Hexagonal:
                        switch (HEXAGONAL_NEIGHBOURHOOD)
                        {
                            case HexagonalNeighbourhood.Left:
                                tabTmp = Nonperiodic_HexagonalLeft(g, tabTmp);
                                break;

                            case HexagonalNeighbourhood.Right:
                                tabTmp = Nonperiodic_HexagonalRight(g, tabTmp);
                                break;

                            case HexagonalNeighbourhood.Random:

                                break;
                        }
                        break;

                    case Neighbourhood.Radial:
                        tabTmp = Nonperiodic_Radial(g, tabTmp, energy);
                        break;

                }

                break;

            case BoundaryCondition.Periodic:

                switch (NEIGHBOURHOOD)
                {
                    case Neighbourhood.von_Neumann:
                        tabTmp = Periodic_vonNeumann(g, tabTmp);
                        break;

                    case Neighbourhood.Moore:
                        tabTmp = Periodic_Moore(g, tabTmp);
                        break;

                    case Neighbourhood.Pentagonal:

                        switch (PENTAGONAL_NEIGHBOURHOOD)
                        {
                            case PentagonalNeighbourhood.Bottom:
                                tabTmp = Periodic_PentagonalBottom(g, tabTmp);
                                break;

                            case PentagonalNeighbourhood.Top:
                                tabTmp = Periodic_PentagonalTop(g, tabTmp);
                                break;

                            case PentagonalNeighbourhood.Left:
                                tabTmp = Periodic_PentagonalLeft(g, tabTmp);
                                break;

                            case PentagonalNeighbourhood.Right:
                                tabTmp = Periodic_PentagonalRight(g, tabTmp);
                                break;
                        }

                        break;

                    case Neighbourhood.Hexagonal:
                        switch (HEXAGONAL_NEIGHBOURHOOD)
                        {
                            case HexagonalNeighbourhood.Left:
                                tabTmp = Periodic_HexagonalLeft(g, tabTmp);
                                break;

                            case HexagonalNeighbourhood.Right:
                                tabTmp = Periodic_HexagonalRight(g, tabTmp);
                                break;

                            case HexagonalNeighbourhood.Random:

                                break;
                        }
                        break;

                    case Neighbourhood.Radial:
                        tabTmp = Periodic_Radial(g, tabTmp, energy);
                        break;

                }

                break;
        }

        this.tab = tabTmp;
    }


    private int[,] Periodic_vonNeumann(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {

                int s_l = -200;
                int s_p = -200;

                int s_d = -200;
                int s_g = -200;


                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? SIZE_X - 1 : j - 1;
                int x_p = j + 1 >= SIZE_X ? 0 : j + 1;
                int y_d = i + 1 >= SIZE_Y ? 0 : i + 1;
                int y_g = i - 1 < 0 ? SIZE_Y - 1 : i - 1;

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

    private int[,] Periodic_Moore(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
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

                int x_l = j - 1 < 0 ? SIZE_X - 1 : j - 1;
                int x_p = j + 1 >= SIZE_X ? 0 : j + 1;
                int y_d = i + 1 >= SIZE_Y ? 0 : i + 1;
                int y_g = i - 1 < 0 ? SIZE_Y - 1 : i - 1;

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


    private int[,] Periodic_PentagonalRight(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_l = -200;

                int s_d = -200;
                int s_g = -200;

                int s_g_l = -200;

                int s_d_l = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? SIZE_X - 1 : j - 1;
                int y_d = i + 1 >= SIZE_Y ? 0 : i + 1;
                int y_g = i - 1 < 0 ? SIZE_Y - 1 : i - 1;

                s_l = tab[i, x_l];
                s_d = tab[y_d, j];
                s_g = tab[y_g, j];

                s_d_l = tab[y_d, x_l];

                s_g_l = tab[y_g, x_l];


                string[] neighbours = new string[] { s_l.ToString(), s_d.ToString(),
                    s_g.ToString(), s_d_l.ToString(), s_g_l.ToString() };

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

    private int[,] Periodic_PentagonalLeft(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_p = -200;

                int s_d = -200;
                int s_g = -200;

                int s_g_p = -200;

                int s_d_p = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_p = j + 1 >= SIZE_X ? 0 : j + 1;
                int y_d = i + 1 >= SIZE_Y ? 0 : i + 1;
                int y_g = i - 1 < 0 ? SIZE_Y - 1 : i - 1;

                s_p = tab[i, x_p];
                s_d = tab[y_d, j];
                s_g = tab[y_g, j];

                s_d_p = tab[y_d, x_p];

                s_g_p = tab[y_g, x_p];


                string[] neighbours = new string[] { s_p.ToString(), s_d.ToString(),
                    s_g.ToString(), s_d_p.ToString(), s_g_p.ToString() };

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

    private int[,] Periodic_PentagonalBottom(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_l = -200;
                int s_p = -200;

                int s_g = -200;

                int s_g_l = -200;
                int s_g_p = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? SIZE_X - 1 : j - 1;
                int x_p = j + 1 >= SIZE_X ? 0 : j + 1;
                int y_g = i - 1 < 0 ? SIZE_Y - 1 : i - 1;

                s_l = tab[i, x_l];
                s_p = tab[i, x_p];
                s_g = tab[y_g, j];

                s_g_l = tab[y_g, x_l];
                s_g_p = tab[y_g, x_p];


                string[] neighbours = new string[] { s_l.ToString(), s_p.ToString(),
                    s_g.ToString(), s_g_l.ToString(), s_g_p.ToString() };

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

    private int[,] Periodic_PentagonalTop(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_l = -200;
                int s_p = -200;

                int s_d = -200;

                int s_d_l = -200;
                int s_d_p = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? SIZE_X - 1 : j - 1;
                int x_p = j + 1 >= SIZE_X ? 0 : j + 1;
                int y_d = i + 1 >= SIZE_Y ? 0 : i + 1;

                s_l = tab[i, x_l];
                s_p = tab[i, x_p];
                s_d = tab[y_d, j];

                s_d_l = tab[y_d, x_l];
                s_d_p = tab[y_d, x_p];


                string[] neighbours = new string[] { s_l.ToString(), s_p.ToString(), s_d.ToString(),
                     s_d_l.ToString(), s_d_p.ToString() };

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



    private int[,] Periodic_HexagonalRight(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_l = -200;
                int s_p = -200;

                int s_d = -200;
                int s_g = -200;

                int s_g_p = -200;

                int s_d_l = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? SIZE_X - 1 : j - 1;
                int x_p = j + 1 >= SIZE_X ? 0 : j + 1;
                int y_d = i + 1 >= SIZE_Y ? 0 : i + 1;
                int y_g = i - 1 < 0 ? SIZE_Y - 1 : i - 1;

                s_l = tab[i, x_l];
                s_p = tab[i, x_p];
                s_d = tab[y_d, j];
                s_g = tab[y_g, j];

                s_d_l = tab[y_d, x_l];

                s_g_p = tab[y_g, x_p];


                string[] neighbours = new string[] { s_l.ToString(), s_p.ToString(), s_d.ToString(),
                    s_g.ToString(), s_d_l.ToString(), s_g_p.ToString() };

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

    private int[,] Periodic_HexagonalLeft(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_l = -200;
                int s_p = -200;

                int s_d = -200;
                int s_g = -200;

                int s_g_l = -200;

                int s_d_p = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? SIZE_X - 1 : j - 1;
                int x_p = j + 1 >= SIZE_X ? 0 : j + 1;
                int y_d = i + 1 >= SIZE_Y ? 0 : i + 1;
                int y_g = i - 1 < 0 ? SIZE_Y - 1 : i - 1;

                s_l = tab[i, x_l];
                s_p = tab[i, x_p];
                s_d = tab[y_d, j];
                s_g = tab[y_g, j];

                s_d_p = tab[y_d, x_p];

                s_g_l = tab[y_g, x_l];


                string[] neighbours = new string[] { s_l.ToString(), s_p.ToString(), s_d.ToString(),
                    s_g.ToString(), s_d_p.ToString(), s_g_l.ToString() };

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


    private int[,] Periodic_Radial(Graphics g, int[,] tabTmp, GrainEnergy energy)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                if (this.tab[i, j] == 0)
                    continue;

                tabTmp[i, j] = this.tab[i, j];

                float x_sr = energy.Tab[i, j].x;
                float y_sr = energy.Tab[i, j].y;

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
                        bool isGreaterY = i + b >= SIZE_X;
                        bool isLowerY = i + b < 0;
                        // Console.WriteLine("j = " + j + " b + " + b);
                        // Console.WriteLine(j+b>=SIZE_X);
                        // Console.WriteLine("x = " + x + " y = " + y);

                        float x_r = energy.Tab[y, x].x;
                        float y_r = energy.Tab[y, x].y;

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

                        // Console.WriteLine("x = " + x + " y = " + y);
                       // Console.WriteLine("x_r = " + x_r + " y_r = " + y_r);

                        if (this.tab[y, x] == 0 && tabTmp[y, x] == 0)
                        {
                            if (Math.Sqrt(Math.Pow((double)(x_sr - x_r), 2) + Math.Pow((double)(y_sr - y_r), 2)) < RADIUS * CELL_SIZE)
                            {
                                tabTmp[y, x] = this.tab[i, j];

                                Display(g, x, y, tabTmp[y, x]);

                                energy.Display(g, x, y);
                            }
                        }
                    }
                }
            }
        }

        return tabTmp;
    }


    private int[,] Nonperiodic_vonNeumann(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_l = -200;
                int s_p = -200;

                int s_d = -200;
                int s_g = -200;


                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? j : j - 1;
                int x_p = j + 1 >= SIZE_X ? j : j + 1;
                int y_d = i + 1 >= SIZE_Y ? i : i + 1;
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

    private int[,] Nonperiodic_Moore(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
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
                int x_p = j + 1 >= SIZE_X ? j : j + 1;
                int y_d = i + 1 >= SIZE_Y ? i : i + 1;
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



    private int[,] Nonperiodic_PentagonalRight(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_l = -200;

                int s_d = -200;
                int s_g = -200;

                int s_g_l = -200;

                int s_d_l = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? j : j - 1;
                int y_d = i + 1 >= SIZE_Y ? i : i + 1;
                int y_g = i - 1 < 0 ? i : i - 1;

                s_l = tab[i, x_l];
                s_d = tab[y_d, j];
                s_g = tab[y_g, j];

                s_d_l = tab[y_d, x_l];

                s_g_l = tab[y_g, x_l];


                string[] neighbours = new string[] { s_l.ToString(), s_d.ToString(),
                    s_g.ToString(), s_d_l.ToString(), s_g_l.ToString() };

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

    private int[,] Nonperiodic_PentagonalLeft(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_p = -200;

                int s_d = -200;
                int s_g = -200;

                int s_g_p = -200;

                int s_d_p = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_p = j + 1 >= SIZE_X ? j : j + 1;
                int y_d = i + 1 >= SIZE_Y ? i : i + 1;
                int y_g = i - 1 < 0 ? i : i - 1;

                s_p = tab[i, x_p];
                s_d = tab[y_d, j];
                s_g = tab[y_g, j];

                s_d_p = tab[y_d, x_p];

                s_g_p = tab[y_g, x_p];


                string[] neighbours = new string[] { s_p.ToString(), s_d.ToString(),
                    s_g.ToString(), s_d_p.ToString(), s_g_p.ToString() };

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

    private int[,] Nonperiodic_PentagonalBottom(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_l = -200;
                int s_p = -200;

                int s_g = -200;

                int s_g_l = -200;
                int s_g_p = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? j : j - 1;
                int x_p = j + 1 >= SIZE_X ? j : j + 1;
                int y_g = i - 1 < 0 ? i : i - 1;

                s_l = tab[i, x_l];
                s_p = tab[i, x_p];
                s_g = tab[y_g, j];

                s_g_l = tab[y_g, x_l];
                s_g_p = tab[y_g, x_p];


                string[] neighbours = new string[] { s_l.ToString(), s_p.ToString(),
                    s_g.ToString(), s_g_l.ToString(), s_g_p.ToString() };

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

    private int[,] Nonperiodic_PentagonalTop(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_l = -200;
                int s_p = -200;

                int s_d = -200;

                int s_d_l = -200;
                int s_d_p = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? j : j - 1;
                int x_p = j + 1 >= SIZE_X ? j : j + 1;
                int y_d = i + 1 >= SIZE_Y ? i : i + 1;

                s_l = tab[i, x_l];
                s_p = tab[i, x_p];
                s_d = tab[y_d, j];

                s_d_l = tab[y_d, x_l];
                s_d_p = tab[y_d, x_p];


                string[] neighbours = new string[] { s_l.ToString(), s_p.ToString(), s_d.ToString(),
                     s_d_l.ToString(), s_d_p.ToString() };

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



    private int[,] Nonperiodic_HexagonalRight(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_l = -200;
                int s_p = -200;

                int s_d = -200;
                int s_g = -200;

                int s_g_p = -200;

                int s_d_l = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? j : j - 1;
                int x_p = j + 1 >= SIZE_X ? j : j + 1;
                int y_d = i + 1 >= SIZE_Y ? i : i + 1;
                int y_g = i - 1 < 0 ? i : i - 1;

                s_l = tab[i, x_l];
                s_p = tab[i, x_p];
                s_d = tab[y_d, j];
                s_g = tab[y_g, j];

                s_d_l = tab[y_d, x_l];

                s_g_p = tab[y_g, x_p];


                string[] neighbours = new string[] { s_l.ToString(), s_p.ToString(), s_d.ToString(),
                    s_g.ToString(), s_d_l.ToString(), s_g_p.ToString() };

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

    private int[,] Nonperiodic_HexagonalLeft(Graphics g, int[,] tabTmp)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {


                int s_l = -200;
                int s_p = -200;

                int s_d = -200;
                int s_g = -200;

                int s_g_l = -200;

                int s_d_p = -200;

                int cellBegin = this.tab[i, j];
                int cellEnd = this.tab[i, j];

                int x_l = j - 1 < 0 ? j : j - 1;
                int x_p = j + 1 >= SIZE_X ? j : j + 1;
                int y_d = i + 1 >= SIZE_Y ? i : i + 1;
                int y_g = i - 1 < 0 ? i : i - 1;

                s_l = tab[i, x_l];
                s_p = tab[i, x_p];
                s_d = tab[y_d, j];
                s_g = tab[y_g, j];

                s_d_p = tab[y_d, x_p];

                s_g_l = tab[y_g, x_l];


                string[] neighbours = new string[] { s_l.ToString(), s_p.ToString(), s_d.ToString(),
                    s_g.ToString(), s_d_p.ToString(), s_g_l.ToString() };

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



    private int[,] Nonperiodic_Radial(Graphics g, int[,] tabTmp, GrainEnergy energy)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                if (this.tab[i, j] == 0)
                    continue;

                tabTmp[i, j] = this.tab[i, j];

                float x_sr = energy.Tab[i, j].x;
                float y_sr = energy.Tab[i, j].y;

                int maxY = (int) Math.Ceiling((double) (i + RADIUS >= SIZE_Y ? SIZE_Y - 1 : i + RADIUS));
                int maxX = (int) Math.Ceiling((double)(j + RADIUS >= SIZE_X ? SIZE_X - 1 : j + RADIUS));
                int minY = (int) Math.Floor((double)(i - RADIUS < 0 ? 0 : i - RADIUS));
                int minX = (int) Math.Floor((double)(j - RADIUS < 0 ? 0 : j - RADIUS));


                for (int x = minX; x <= maxX; ++x)
                {
                    for (int y = minY; y <= maxY; ++y)
                    {
                        if (this.tab[y, x] == 0 && tabTmp[y, x] == 0)
                        {
                            if(Math.Sqrt( Math.Pow((double)(x_sr - energy.Tab[y, x].x ), 2) + Math.Pow((double)(y_sr - energy.Tab[y, x].y), 2)) < RADIUS * CELL_SIZE)
                            {
                                tabTmp[y, x] = this.tab[i, j];

                                Display(g, x, y, tabTmp[y, x]);

                                energy.Display(g, x, y);
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
                if (tab[y, x] == 0)
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
