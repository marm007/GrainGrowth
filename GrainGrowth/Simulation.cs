using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
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

    private Grain[,] previousGrains;


    public Simulation()
	{
        this.Tab = new Grain[SIZE_Y, SIZE_X];
        this.previousGrains = new Grain[SIZE_Y, SIZE_X];

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                this.Tab[i, j] = new Grain(j, i, 0);
                this.previousGrains[i, j] = this.Tab[i, j].Copy();
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


        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                this.tab[i, j].DisplayEnergy(g);
            }
        }
    }


    public void Simulate(Graphics g)
    {
      
        switch (NEIGHBOURHOOD)
        {
            case Neighbourhood.Radial:
                ChangeStateRadial(g);
                break;
            default:
                ChangeState(g);
                break;
        }

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                if(this.tab[i, j].State == DEAD && this.tab[i, j].State != previousGrains[i, j].State)
                    this.tab[i, j].State = previousGrains[i, j].State;
            }
        }

    }


    private void ChangeState(Graphics g)
    {

      for(int i = 0; i < SIZE_Y; i++)
      {

          for(int j = 0; j < SIZE_X; j++)
          {
              if (this.tab[i, j].State == 0)
              {
                  int cellBegin = this.tab[i, j].State;
                  int cellEnd = this.tab[i, j].State;

                  cellEnd = NeighbourhoodFactory.GetNeighbours(this.tab, j, i);

                  if (cellEnd != cellBegin)
                  {
                      previousGrains[i, j].State = cellEnd;
                      previousGrains[i, j].Display(g);
                  }
              }
          };
      };
    }


    private void ChangeStateRadial(Graphics g)
    {

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {

                if (this.tab[i, j].State == 0)
                    continue;

                foreach (Grain grain in NeighbourhoodFactory.GetRadialGrains(this.tab, j, i))
                {
                    previousGrains[grain.Y, grain.X].State = this.Tab[i, j].State;

                    previousGrains[grain.Y, grain.X].Display(g);
                }
            }
        }

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


    public void Clear()
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                this.tab[i, j].State = 0;
                this.previousGrains[i, j].State = 0;
            }
        }
    }

    public void Resize()
    {
        this.previousGrains = new Grain[SIZE_Y, SIZE_X];

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                this.previousGrains[i, j] = this.tab[i, j].Copy();
            }
        }
    }

}
