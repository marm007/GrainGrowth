using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Config;

public class GrainEnergy
{
    private static Random random = new Random();

    private GrainEnergyCoords[,] tab;


    public struct GrainEnergyCoords
    {
        public float x, y;

        public GrainEnergyCoords(float x1, float y1)
        {
            x = x1;
            y = y1;
        }
    }

    public GrainEnergy()
    {
        this.tab = new GrainEnergyCoords[SIZE_Y, SIZE_X];

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                float x = (float)Math.Round(random.NextDouble() * (CELL_SIZE - RADIUS) + (j * CELL_SIZE), 1) + RADIUS / 2;
                float y = (float)Math.Round(random.NextDouble() * (CELL_SIZE - RADIUS) + (i * CELL_SIZE), 1) + RADIUS / 2;
                this.tab[i, j] = new GrainEnergyCoords(x, y);
            }
        }
    }

    public GrainEnergyCoords[,] Tab { get { return tab; } set { tab = value; } }

    public void Display(Graphics g)
    {
        if (ENERGY_STATE == EnergyState.Disable)
            return;

        System.Drawing.Pen pen = new System.Drawing.Pen(Color.Red, 1);
        System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(Color.Red);

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                g.FillEllipse(brush, this.tab[i, j].x - RADIUS / 2.0f, this.tab[i, j].y - RADIUS / 2.0f, RADIUS, RADIUS);
            }
        }
    }
    public void Display(Graphics g, int x, int y)
    {
        if (ENERGY_STATE == EnergyState.Disable)
            return;

        System.Drawing.Pen pen = new System.Drawing.Pen(Color.Red, 1);
        System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(Color.Red);

    
        g.FillEllipse(brush, this.tab[y, x].x - RADIUS / 2.0f, this.tab[y, x].y - RADIUS / 2.0f, RADIUS, RADIUS);
    }

}
