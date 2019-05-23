using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Config;

public class Radial : NeighbourhoodAbstract
{
    public override List<string> Neighbours { get { return _Neighbours; } }

    public override List<Grain> NeighboursGrains  { get { return _NeighboursGrains; } }

    private List<string> _Neighbours;
    private List<Grain> _NeighboursGrains;

    public override void GetNeighbours(Grain[,] grains, int x1, int y1)
    {
        _Neighbours = new List<string>();
        _NeighboursGrains = new List<Grain>();


        if (BOUNDARY_CONDITION == BoundaryCondition.Periodic)
        {
            float x_sr = grains[y1, x1].EnergyCoords.X;
            float y_sr = grains[y1, x1].EnergyCoords.Y;

            int maxY = (int)Math.Ceiling((double)(y1 + RADIUS >= SIZE_Y ? SIZE_Y - 1 : y1 + RADIUS));
            int maxX = (int)Math.Ceiling((double)(x1 + RADIUS >= SIZE_X ? SIZE_X - 1 : x1 + RADIUS));
            int minY = (int)Math.Floor((double)(y1 - RADIUS < 0 ? 0 : y1 - RADIUS));
            int minX = (int)Math.Floor((double)(x1 - RADIUS < 0 ? 0 : x1 - RADIUS));

            for (int a = (int)(-RADIUS); a <= (int)(RADIUS); ++a)
            {
                for (int b = (int)(-RADIUS); b <= (int)(RADIUS); ++b)
                {
                    int x = x1 + b >= SIZE_X ? (0 + (x1 + b) - SIZE_X) : (x1 + b < 0 ? (SIZE_X + (x1 + b)) : x1 + b);
                    int y = y1 + a >= SIZE_Y ? (0 + (y1 + a) - SIZE_Y) : (y1 + a < 0 ? (SIZE_Y + (y1 + a)) : y1 + a);

                    bool isGreaterX = x1 + b >= SIZE_X;
                    bool isLowerX = x1 + b < 0;
                    bool isGreaterY = y1 + b >= SIZE_Y;
                    bool isLowerY = y1 + b < 0;

                    float x_r = grains[y, x].EnergyCoords.X;
                    float y_r = grains[y, x].EnergyCoords.Y;

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


                    if (grains[y, x].State == 0 )
                    {
                        if (Math.Sqrt(Math.Pow((double)(x_sr - x_r), 2) + Math.Pow((double)(y_sr - y_r), 2)) < RADIUS * CELL_SIZE)
                        {
                            _Neighbours.Add(grains[y, x].State.ToString());
                            _NeighboursGrains.Add(grains[y, x]);
                        }
                    }
                }
            }
        }
        else
        {
            int x = x1;
            int y = y1;

            float x_sr = grains[y, x].EnergyCoords.X;
            float y_sr = grains[y, x].EnergyCoords.Y;

            int maxY = (int)Math.Ceiling((double)(y + RADIUS >= SIZE_Y ? SIZE_Y - 1 : y + RADIUS));
            int maxX = (int)Math.Ceiling((double)(x + RADIUS >= SIZE_X ? SIZE_X - 1 : x + RADIUS));
            int minY = (int)Math.Floor((double)(y - RADIUS < 0 ? 0 : y - RADIUS));
            int minX = (int)Math.Floor((double)(x - RADIUS < 0 ? 0 : x - RADIUS));


            for (int xx = minX; xx <= maxX; ++xx)
            {
                for (int yy = minY; yy <= maxY; ++yy)
                {
                    if (grains[yy, xx].State == 0)
                    {
                        if (Math.Sqrt(Math.Pow((double)(x_sr - grains[yy, xx].EnergyCoords.X), 2) + Math.Pow((double)(y_sr - grains[yy, xx].EnergyCoords.Y), 2)) < RADIUS * CELL_SIZE)
                        {
                            _Neighbours.Add(grains[yy, xx].State.ToString());
                            _NeighboursGrains.Add(grains[yy, xx]);
                        }
                    }
                }
            }
        }
    }

    public override void GetAllNeighbours(Grain[,] grains, int x1, int y1)
    {
        _Neighbours = new List<string>();
        _NeighboursGrains = new List<Grain>();


        if (BOUNDARY_CONDITION == BoundaryCondition.Periodic)
        {
            float x_sr = grains[y1, x1].EnergyCoords.X;
            float y_sr = grains[y1, x1].EnergyCoords.Y;

            int maxY = (int)Math.Ceiling((double)(y1 + RADIUS >= SIZE_Y ? SIZE_Y - 1 : y1 + RADIUS));
            int maxX = (int)Math.Ceiling((double)(x1 + RADIUS >= SIZE_X ? SIZE_X - 1 : x1 + RADIUS));
            int minY = (int)Math.Floor((double)(y1 - RADIUS < 0 ? 0 : y1 - RADIUS));
            int minX = (int)Math.Floor((double)(x1 - RADIUS < 0 ? 0 : x1 - RADIUS));

            for (int a = (int)(-RADIUS); a <= (int)(RADIUS); ++a)
            {
                for (int b = (int)(-RADIUS); b <= (int)(RADIUS); ++b)
                {
                    int x = x1 + b >= SIZE_X ? (0 + (x1 + b) - SIZE_X) : (x1 + b < 0 ? (SIZE_X + (x1 + b)) : x1 + b);
                    int y = y1 + a >= SIZE_Y ? (0 + (y1 + a) - SIZE_Y) : (y1 + a < 0 ? (SIZE_Y + (y1 + a)) : y1 + a);

                    bool isGreaterX = x1 + b >= SIZE_X;
                    bool isLowerX = x1 + b < 0;
                    bool isGreaterY = y1 + b >= SIZE_Y;
                    bool isLowerY = y1 + b < 0;

                    float x_r = grains[y, x].EnergyCoords.X;
                    float y_r = grains[y, x].EnergyCoords.Y;

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


                    if (Math.Sqrt(Math.Pow((double)(x_sr - x_r), 2) + Math.Pow((double)(y_sr - y_r), 2)) < RADIUS * CELL_SIZE)
                    {
                        _Neighbours.Add(grains[y, x].State.ToString());
                        _NeighboursGrains.Add(grains[y, x]);
                    }
                }
            }
        }
        else
        {
            int x = x1;
            int y = y1;

            float x_sr = grains[y, x].EnergyCoords.X;
            float y_sr = grains[y, x].EnergyCoords.Y;

            int maxY = (int)Math.Ceiling((double)(y + RADIUS >= SIZE_Y ? SIZE_Y - 1 : y + RADIUS));
            int maxX = (int)Math.Ceiling((double)(x + RADIUS >= SIZE_X ? SIZE_X - 1 : x + RADIUS));
            int minY = (int)Math.Floor((double)(y - RADIUS < 0 ? 0 : y - RADIUS));
            int minX = (int)Math.Floor((double)(x - RADIUS < 0 ? 0 : x - RADIUS));


            for (int xx = minX; xx <= maxX; ++xx)
            {
                for (int yy = minY; yy <= maxY; ++yy)
                {
                    if (Math.Sqrt(Math.Pow((double)(x_sr - grains[yy, xx].EnergyCoords.X), 2) + Math.Pow((double)(y_sr - grains[yy, xx].EnergyCoords.Y), 2)) < RADIUS * CELL_SIZE)
                    {
                        _Neighbours.Add(grains[yy, xx].State.ToString());
                        _NeighboursGrains.Add(grains[yy, xx]);
                    }
                }
            }
        }
    }

}
