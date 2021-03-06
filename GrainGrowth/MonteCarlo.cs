﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Config;

public class MonteCarlo
{
    private static Random random = new Random();

    private float J = 1.0f;
    private float kT = 0.1f;

    public float JH { get { return J; } set { J = value; } }
    public float KT { get { return kT; } set { kT = value; } }

    public MonteCarlo()
    {

    }

    public void Simulate(Simulation grainGrowth, Graphics gB, Graphics g)
    {

        Grain[,] grains = grainGrowth.Tab;

        List<Grain> all = grains.OfType<Grain>().ToList();

        while (all.Count > 0)
        {
            if (BREAK_SIMULATION)
            {
                BREAK_SIMULATION = false;
                break;
            }

        

            int index = random.Next(all.Count);
            Grain grain = all.ElementAt(index);
            all.RemoveAt(index);

            int stateBefore = grain.State;

            List<int> neighbours = NeighbourhoodFactory.GetNeighboursMonteCarlo(grains, grain.X, grain.Y);

            int energyBefore = CalculateEnergy(neighbours, stateBefore);

            grains[grain.Y, grain.X].Q = energyBefore;

            int stateAfter = (neighbours.ElementAt(random.Next(neighbours.Count)));

            int energyAfter = CalculateEnergy(neighbours, stateAfter);

            int deltaEnergy = energyAfter - energyBefore;
            if (deltaEnergy <= 0)
            {
                grains[grain.Y, grain.X].State = stateAfter;
                grain.Display(g, gB);
                grains[grain.Y, grain.X].Q = energyAfter;

            }
            else
            {
                float probability = (float)Math.Exp(-(deltaEnergy / this.KT)) * 100;
                float value = (float)random.NextDouble() * 100;

                if (value <= probability)
                {
                    grains[grain.Y, grain.X].State = stateAfter;
                    grain.Display(g, gB);
                    grains[grain.Y, grain.X].Q = energyAfter;

                }


            }
        }
    }

    private int CalculateEnergy(List<int> neighbours, int state)
    {
        return (int)(this.J) * neighbours.Where(s => s != state).ToList().Count;
    }

    public void CalculateEnergy(Simulation grainGrowth)
    {

        for (int y = 0; y < SIZE_Y; y++)
        {
            for (int x = 0; x < SIZE_X; x++)
            {

          

                int stateBefore = grainGrowth.Tab[y, x].State;

                List<int> neighbours = NeighbourhoodFactory.GetNeighboursMonteCarlo(grainGrowth.Tab, x, y);


                int energyBefore = CalculateEnergy(neighbours, stateBefore);

                grainGrowth.Tab[y, x].Q = energyBefore;
            }
        }
    }

    public void DisplayEnergy(Grain[,] grains, Graphics g)
    {
        System.Drawing.SolidBrush cellBrushClear = new System.Drawing.SolidBrush(SystemColors.Control);

        for (int y = 0; y < SIZE_Y; y++)
        {
            for(int x = 0; x < SIZE_X; x++)
            {
                EnergyColors.InitializeColors(grains[y, x].Q + 1);

                int colorIndex = grains[y, x].Q; ; //> 1 ? 8 : grains[y, x].Q;

                System.Drawing.SolidBrush cellBrush = new System.Drawing.SolidBrush(EnergyColors.colors[colorIndex]);

                g.FillRectangle(cellBrush, (grains[y, x].X) * CELL_SIZE + (int)GRID_STATE, (grains[y, x].Y) * CELL_SIZE + (int)GRID_STATE,
             CELL_SIZE - (int)GRID_STATE, CELL_SIZE - (int)GRID_STATE);
            }
        }
    }
}
