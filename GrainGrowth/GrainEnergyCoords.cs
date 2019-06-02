using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Config;

public struct GrainEnergyCoords
{
    private static Random random = new Random();

    float x;
    float y;

    public GrainEnergyCoords(int x1, int y1)
    {
        x = (float) Math.Round(random.NextDouble() * (((float)CELL_SIZE) - ENERGY_RADIUS * 2) + (float)(x1 * CELL_SIZE), 1) + ENERGY_RADIUS ;
        y = (float) Math.Round(random.NextDouble() * (((float)CELL_SIZE) - ENERGY_RADIUS * 2) + (float)(y1 * CELL_SIZE), 1) + ENERGY_RADIUS ;
        // Console.WriteLine("x = " + x + " y = " + y);
    }

    public GrainEnergyCoords(GrainEnergyCoords energyCoords)
    {
        x = energyCoords.X;
        y = energyCoords.Y;
    }

     public float X { get { return x; } set { x = value; } } 
     public float Y { get { return y; } set { y = value; } }
 
}
