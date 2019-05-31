using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Config;

public class Grain
{
    private GrainEnergyCoords energyCoords;

    private int state;
    private int x;
    private int y;

    private int q;

    private static Object locker = new Object();
    public Grain(int x, int y, int state)
    {
        this.energyCoords = new GrainEnergyCoords(x, y);
        this.state = state;
        this.x = x;
        this.y = y;
    }

    public int State { get { return state; } set {  state = value; } }
    public int X { get { return x; } }
    public int Y { get { return y; } }
    public int Q { get { return q; } set { q = value; } }
    public GrainEnergyCoords EnergyCoords { get { return energyCoords; } set { energyCoords.X = value.X; energyCoords.Y = value.Y; } }
    public static int Counter = 0;


    public void Display(Graphics g)
    {
        lock (locker)
        {
            if (this.state == 0)
            {
                System.Drawing.SolidBrush cellBrushClear = new System.Drawing.SolidBrush(SystemColors.Control);

                g.FillRectangle(cellBrushClear, (this.x) * CELL_SIZE + (int)GRID_STATE, (this.y) * CELL_SIZE + (int)GRID_STATE,
                    CELL_SIZE - (int)GRID_STATE, CELL_SIZE - (int)GRID_STATE);

            }
            else
            {
                System.Drawing.SolidBrush cellBrush = new System.Drawing.SolidBrush(Colors.colors[State]);

                g.FillRectangle(cellBrush, (this.x) * CELL_SIZE + (int)GRID_STATE, (this.y) * CELL_SIZE + (int)GRID_STATE,
                    CELL_SIZE - (int)GRID_STATE, CELL_SIZE - (int)GRID_STATE);
            }
        }
       


    }

    public void Display(Graphics g, Graphics gB)
    {

        if (this.state == 0)
        {
            System.Drawing.SolidBrush cellBrushClear = new System.Drawing.SolidBrush(SystemColors.Control);

            g.FillRectangle(cellBrushClear, (this.x) * CELL_SIZE + (int)GRID_STATE, (this.y) * CELL_SIZE + (int)GRID_STATE,
                CELL_SIZE - (int)GRID_STATE, CELL_SIZE - (int)GRID_STATE);

            gB.FillRectangle(cellBrushClear, (this.x) * CELL_SIZE + (int)GRID_STATE, (this.y) * CELL_SIZE + (int)GRID_STATE,
                CELL_SIZE - (int)GRID_STATE, CELL_SIZE - (int)GRID_STATE);

        }
        else
        {
            System.Drawing.SolidBrush cellBrush = new System.Drawing.SolidBrush(Colors.colors[State]);

            g.FillRectangle(cellBrush, (this.x) * CELL_SIZE + (int)GRID_STATE, (this.y) * CELL_SIZE + (int)GRID_STATE,
                CELL_SIZE - (int)GRID_STATE, CELL_SIZE - (int)GRID_STATE);

            gB.FillRectangle(cellBrush, (this.x) * CELL_SIZE + (int)GRID_STATE, (this.y) * CELL_SIZE + (int)GRID_STATE,
                CELL_SIZE - (int)GRID_STATE, CELL_SIZE - (int)GRID_STATE);
        }

    }


    public void DisplayEnergy(Graphics g)
    {
        if (ENERGY_STATE == EnergyState.Disable)
            return;


        System.Drawing.Pen pen = new System.Drawing.Pen(Color.Red, 1);
        System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(Color.Red);


        g.DrawEllipse(pen, this.EnergyCoords.X - ENERGY_RADIUS , this.EnergyCoords.Y - ENERGY_RADIUS, ENERGY_RADIUS * 2, ENERGY_RADIUS * 2);
        // Console.WriteLine("one = " + (this.EnergyCoords.X - ENERGY_RADIUS / 2.0f) +
        //   " two = " + (this.EnergyCoords.Y - ENERGY_RADIUS / 2.0f) + " three = " + ENERGY_RADIUS);
    }

    public Grain Copy()
    {
        Grain grain = new Grain(this.x, this.y, this.state);
        grain.EnergyCoords = this.energyCoords;
        return grain;
    }

}
