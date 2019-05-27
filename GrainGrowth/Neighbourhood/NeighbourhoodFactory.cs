using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Config;

public static class NeighbourhoodFactory
{
    private static int ERROR = -1;
    private static Random random = new Random();
    private static Array pentagonalNeighbourhoods = Enum.GetValues(typeof(PentagonalNeighbourhood));
    private static HexagonalNeighbourhood[] hexagonalNeighbourhoods = new HexagonalNeighbourhood[2] { HexagonalNeighbourhood.Left, HexagonalNeighbourhood.Right };

    public static int GetNeighbours(Grain[,] grains, int x, int y)
    {
        List<int> _Neighbours = null;
        switch (NEIGHBOURHOOD)
        {
            case Neighbourhood.vonNeumann:
                _Neighbours = vonNeumann(grains, x, y);
                break;
            case Neighbourhood.Moore:
                _Neighbours = Moore(grains, x, y);
                break;
            case Neighbourhood.Pentagonal:
                _Neighbours = Pentagonal(grains, x, y);
                break;
            case Neighbourhood.Hexagonal:
                _Neighbours = Hexagonal(grains, x, y);
                break;
            case Neighbourhood.Radial:
                _Neighbours = Radial(grains, x, y);
                break;
            default:
                return ERROR;
        }


        var count = new Dictionary<int, int>();

        foreach (int value in _Neighbours)
        {
            if (value != 0)
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

        List<int> mostCommon = new List<int>();

        foreach (KeyValuePair<int, int> pair in count)
        {
            if (pair.Value > highestCount)
            {
                mostCommon = new List<int>();
                mostCommon.Add(pair.Key);

                highestCount = pair.Value;
            }
            else if (pair.Value == highestCount)
            {
                mostCommon.Add(pair.Key);
            }
        }

        if (mostCommon.Count != 0)
        {
            int index = random.Next(mostCommon.Count);

            return mostCommon[index];
        }
        return 0;

    }

    public static List<int> GetNeighboursMonteCarlo(Grain[,] grains, int x, int y)
    {
        switch (NEIGHBOURHOOD)
        {
            case Neighbourhood.vonNeumann:
                return vonNeumann(grains, x, y);
            case Neighbourhood.Moore:
                return Moore(grains, x, y);
            case Neighbourhood.Pentagonal:
                return Pentagonal(grains, x, y);
            case Neighbourhood.Hexagonal:
                return Hexagonal(grains, x, y);
            case Neighbourhood.Radial:
                return RadialMoteCarlo(grains, x, y);
            default:
                return null;
        }
    }


    public static List<int> vonNeumann(Grain[,] grains, int x, int y)
    {
        List<int> _Neighbours = new List<int>();

        int x_l = -200;
        int x_p = -200;
        int y_d = -200;
        int y_g = -200;

        if (BOUNDARY_CONDITION == BoundaryCondition.Periodic)
        {
            x_l = x - 1 < 0 ? SIZE_X - 1 : x - 1;
            x_p = x + 1 >= SIZE_X ? 0 : x + 1;
            y_d = y + 1 >= SIZE_Y ? 0 : y + 1;
            y_g = y - 1 < 0 ? SIZE_Y - 1 : y - 1;

        }
        else
        {
            x_l = x - 1 < 0 ? x : x - 1;
            x_p = x + 1 >= SIZE_X ? x : x + 1;
            y_d = y + 1 >= SIZE_Y ? y : y + 1;
            y_g = y - 1 < 0 ? y : y - 1;
        }

        _Neighbours.Add(grains[y, x_l].State); // s_l
        _Neighbours.Add(grains[y, x_p].State); // s_p
        _Neighbours.Add(grains[y_d, x].State); // s_d
        _Neighbours.Add(grains[y_g, x].State); // s_g


        return _Neighbours;

    }

    public static List<int> Moore(Grain[,] grains, int x, int y)
    {
        List<int> _Neighbours = new List<int>();

        int x_l = -200;
        int x_p = -200;
        int y_d = -200;
        int y_g = -200;



        if (BOUNDARY_CONDITION == BoundaryCondition.Periodic)
        {
            x_l = x - 1 < 0 ? SIZE_X - 1 : x - 1;
            x_p = x + 1 >= SIZE_X ? 0 : x + 1;
            y_d = y + 1 >= SIZE_Y ? 0 : y + 1;
            y_g = y - 1 < 0 ? SIZE_Y - 1 : y - 1;

        }
        else
        {
            x_l = x - 1 < 0 ? x : x - 1;
            x_p = x + 1 >= SIZE_X ? x : x + 1;
            y_d = y + 1 >= SIZE_Y ? y : y + 1;
            y_g = y - 1 < 0 ? y : y - 1;
        }

        _Neighbours.Add(grains[y, x_l].State); // s_l
        _Neighbours.Add(grains[y, x_p].State); // s_p
        _Neighbours.Add(grains[y_d, x].State); // s_d
        _Neighbours.Add(grains[y_g, x].State); // s_g

        _Neighbours.Add(grains[y_d, x_l].State); // s_d_l
        _Neighbours.Add(grains[y_d, x_p].State); // s_d_p

        _Neighbours.Add(grains[y_g, x_l].State); // s_g_l
        _Neighbours.Add(grains[y_g, x_p].State); // s_g_p

        return _Neighbours;
    }

    public static List<int> Pentagonal(Grain[,] grains, int x, int y)
    {
        List<int> _Neighbours = new List<int>();

        PentagonalNeighbourhood pentagonalNeighbourhood = (PentagonalNeighbourhood)pentagonalNeighbourhoods.GetValue(SIMULATION_RANDOM.Next(pentagonalNeighbourhoods.Length));

        if (BOUNDARY_CONDITION == BoundaryCondition.Periodic)
        {
            int x_l = x - 1 < 0 ? SIZE_X - 1 : x - 1;
            int x_p = x + 1 >= SIZE_X ? 0 : x + 1;
            int y_d = y + 1 >= SIZE_Y ? 0 : y + 1;
            int y_g = y - 1 < 0 ? SIZE_Y - 1 : y - 1;

            switch (pentagonalNeighbourhood)
            {
                case PentagonalNeighbourhood.Top:
                    _Neighbours.Add(grains[y, x_l].State); // s_l
                    _Neighbours.Add(grains[y, x_p].State); // s_p
                    _Neighbours.Add(grains[y_d, x].State); // s_d

                    _Neighbours.Add(grains[y_d, x_l].State); // s_d_l
                    _Neighbours.Add(grains[y_d, x_p].State); // s_d_p

                    break;
                case PentagonalNeighbourhood.Right:
                    _Neighbours.Add(grains[y, x_l].State); // s_l
                    _Neighbours.Add(grains[y_d, x].State); // s_d
                    _Neighbours.Add(grains[y_g, x].State); // s_g

                    _Neighbours.Add(grains[y_d, x_l].State); // s_d_l

                    _Neighbours.Add(grains[y_g, x_l].State); // s_g_l
                    break;
                case PentagonalNeighbourhood.Bottom:
                    _Neighbours.Add(grains[y, x_l].State); // s_l
                    _Neighbours.Add(grains[y, x_p].State); // s_p
                    _Neighbours.Add(grains[y_g, x].State); // s_g

                    _Neighbours.Add(grains[y_g, x_l].State); // s_g_l
                    _Neighbours.Add(grains[y_g, x_p].State); // s_g_p
                    break;
                case PentagonalNeighbourhood.Left:
                    _Neighbours.Add(grains[y, x_p].State); // s_p
                    _Neighbours.Add(grains[y_d, x].State); // s_d
                    _Neighbours.Add(grains[y_g, x].State); // s_g

                    _Neighbours.Add(grains[y_d, x_p].State); // s_d_p

                    _Neighbours.Add(grains[y_g, x_p].State); // s_g_p
                    break;

            }

        }
        else
        {
            int x_l = x - 1 < 0 ? x : x - 1;
            int x_p = x + 1 >= SIZE_X ? x : x + 1;
            int y_d = y + 1 >= SIZE_Y ? y : y + 1;
            int y_g = y - 1 < 0 ? y : y - 1;

            switch (pentagonalNeighbourhood)
            {
                case PentagonalNeighbourhood.Top:
                    _Neighbours.Add(grains[y, x_l].State); // s_l
                    _Neighbours.Add(grains[y, x_p].State); // s_p
                    _Neighbours.Add(grains[y_d, x].State); // s_d

                    _Neighbours.Add(grains[y_d, x_l].State); // s_d_l
                    _Neighbours.Add(grains[y_d, x_p].State); // s_d_p

                    break;
                case PentagonalNeighbourhood.Right:
                    _Neighbours.Add(grains[y, x_l].State); // s_l
                    _Neighbours.Add(grains[y_d, x].State); // s_d
                    _Neighbours.Add(grains[y_g, x].State); // s_g

                    _Neighbours.Add(grains[y_d, x_l].State); // s_d_l

                    _Neighbours.Add(grains[y_g, x_l].State); // s_g_l
                    break;
                case PentagonalNeighbourhood.Bottom:
                    _Neighbours.Add(grains[y, x_l].State); // s_l
                    _Neighbours.Add(grains[y, x_p].State); // s_p
                    _Neighbours.Add(grains[y_g, x].State); // s_g

                    _Neighbours.Add(grains[y_g, x_l].State); // s_g_l
                    _Neighbours.Add(grains[y_g, x_p].State); // s_g_p
                    break;
                case PentagonalNeighbourhood.Left:
                    _Neighbours.Add(grains[y, x_p].State); // s_p
                    _Neighbours.Add(grains[y_d, x].State); // s_d
                    _Neighbours.Add(grains[y_g, x].State); // s_g

                    _Neighbours.Add(grains[y_d, x_p].State); // s_d_p

                    _Neighbours.Add(grains[y_g, x_p].State); // s_g_p
                    break;

            }


        }

        return _Neighbours;

    }

    public static List<int> Hexagonal(Grain[,] grains, int x, int y)
    {
        List<int> _Neighbours = new List<int>();

        HexagonalNeighbourhood hexagonalNeighbourhood = HEXAGONAL_NEIGHBOURHOOD;

        if(hexagonalNeighbourhood == HexagonalNeighbourhood.Random)
        {
            hexagonalNeighbourhood = (HexagonalNeighbourhood)hexagonalNeighbourhoods.GetValue(SIMULATION_RANDOM.Next(hexagonalNeighbourhoods.Length));
        }

        if (BOUNDARY_CONDITION == BoundaryCondition.Periodic)
        {
            int x_l = x - 1 < 0 ? SIZE_X - 1 : x - 1;
            int x_p = x + 1 >= SIZE_X ? 0 : x + 1;
            int y_d = y + 1 >= SIZE_Y ? 0 : y + 1;
            int y_g = y - 1 < 0 ? SIZE_Y - 1 : y - 1;

            switch (hexagonalNeighbourhood)
            {
                case HexagonalNeighbourhood.Left:
                    _Neighbours.Add(grains[y, x_l].State); // s_l
                    _Neighbours.Add(grains[y, x_p].State); // s_p
                    _Neighbours.Add(grains[y_d, x].State); // s_d
                    _Neighbours.Add(grains[y_g, x].State); // s_g

                    _Neighbours.Add(grains[y_d, x_p].State); // s_d_p

                    _Neighbours.Add(grains[y_g, x_l].State); // s_g_l
                    break;

                case HexagonalNeighbourhood.Right:
                    _Neighbours.Add(grains[y, x_l].State); // s_l
                    _Neighbours.Add(grains[y, x_p].State); // s_p
                    _Neighbours.Add(grains[y_d, x].State); // s_d
                    _Neighbours.Add(grains[y_g, x].State); // s_g

                    _Neighbours.Add(grains[y_d, x_l].State); // s_d_l

                    _Neighbours.Add(grains[y_g, x_p].State); // s_g_p
                    break;
                
            }

        }
        else
        {
            int x_l = x - 1 < 0 ? x : x - 1;
            int x_p = x + 1 >= SIZE_X ? x : x + 1;
            int y_d = y + 1 >= SIZE_Y ? y : y + 1;
            int y_g = y - 1 < 0 ? y : y - 1;

            switch (hexagonalNeighbourhood)
            {
                case HexagonalNeighbourhood.Left:
                    _Neighbours.Add(grains[y, x_l].State); // s_l
                    _Neighbours.Add(grains[y, x_p].State); // s_p
                    _Neighbours.Add(grains[y_d, x].State); // s_d
                    _Neighbours.Add(grains[y_g, x].State); // s_g

                    _Neighbours.Add(grains[y_d, x_p].State); // s_d_p

                    _Neighbours.Add(grains[y_g, x_l].State); // s_g_l
                    break;

                case HexagonalNeighbourhood.Right:
                    _Neighbours.Add(grains[y, x_l].State); // s_l
                    _Neighbours.Add(grains[y, x_p].State); // s_p
                    _Neighbours.Add(grains[y_d, x].State); // s_d
                    _Neighbours.Add(grains[y_g, x].State); // s_g

                    _Neighbours.Add(grains[y_d, x_l].State); // s_d_l

                    _Neighbours.Add(grains[y_g, x_p].State); // s_g_p
                    break;
            }
        }

        return _Neighbours;
    }

    public static List<int> Radial(Grain[,] grains, int x1, int y1)
    {
        List<int> _Neighbours = new List<int>();


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


                    if (grains[y, x].State == 0)
                    {
                        if (Math.Sqrt(Math.Pow((double)(x_sr - x_r), 2) + Math.Pow((double)(y_sr - y_r), 2)) < RADIUS * CELL_SIZE)
                        {
                            _Neighbours.Add(grains[y, x].State);
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
                            _Neighbours.Add(grains[yy, xx].State);
                        }
                    }
                }
            }
        }

        return _Neighbours;
    }

    public static List<int> RadialMoteCarlo(Grain[,] grains, int x1, int y1)
    {
        List<int> _Neighbours = new List<int>();


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
                        _Neighbours.Add(grains[y, x].State);
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
                        _Neighbours.Add(grains[yy, xx].State);
                    }
                }
            }
        }

        return _Neighbours;
    }

    public static List<Grain> GetRadialGrains(Grain[,] grains, int x1, int y1)
    {
        List<Grain> _Neighbours = new List<Grain>();


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


                    if (grains[y, x].State == 0)
                    {
                        if (Math.Sqrt(Math.Pow((double)(x_sr - x_r), 2) + Math.Pow((double)(y_sr - y_r), 2)) < RADIUS * CELL_SIZE)
                        {
                            _Neighbours.Add(grains[y, x]);
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
                            _Neighbours.Add(grains[yy, xx]);
                        }
                    }
                }
            }
        }

        return _Neighbours;
    }

}
