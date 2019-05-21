using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Config;

struct GrainEnergyCoords
{
    private static Random random = new Random();

    float x;
    float y;

    public GrainEnergyCoords(int x1, int y1)
    {
        x = (float) Math.Round(random.NextDouble() * (CELL_SIZE - RADIUS) + (x1 * CELL_SIZE), 1) + RADIUS / 2;
        y = (float) Math.Round(random.NextDouble() * (CELL_SIZE - RADIUS) + (y1 * CELL_SIZE), 1) + RADIUS / 2;
    }

    public float X { get => x; set => x = value; }
    public float Y { get => y; set => y = value; }
}
