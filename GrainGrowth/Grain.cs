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

    public int State { get => state; set => state = value; }
    public int X { get => x; }
    public int Y { get => y; }
    public int Q { get => q; set => q = value; }
    internal GrainEnergyCoords EnergyCoords { get => energyCoords; }

    public void Display(Bitmap bitmap)
    {
       
            Graphics g = Graphics.FromImage(bitmap);

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


    public void Display(Graphics g)
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

    public void DisplayEnergy(Bitmap bitmap)
    {
       

        if (ENERGY_STATE == EnergyState.Disable)
            return;


        Graphics g = Graphics.FromImage(bitmap);

        System.Drawing.Pen pen = new System.Drawing.Pen(Color.Red, 1);
        System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(Color.Red);


        g.FillEllipse(brush, this.EnergyCoords.X - RADIUS / 2.0f, this.EnergyCoords.Y - RADIUS / 2.0f, RADIUS, RADIUS);
    }

}
