using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Config;

public static class Neighbourss
{
    private static int ERROR = -1;


    public static int GetNeighbours(Grain[,] grains, int x, int y)
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
            default:
                return ERROR;
        }
    }

    public static int vonNeumann(Grain[,] grains, int x, int y)
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

        _Neighbours = _Neighbours.Where(n => n != 0).ToList();

        if (_Neighbours.Count == 0)
            return 0;

        int maxCount = _Neighbours.Max(n => n);

        return maxCount;
    }

    public static int Moore(Grain[,] grains, int x, int y)
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

        _Neighbours = _Neighbours.Where(n => n != 0).ToList();

        if (_Neighbours.Count == 0)
            return 0;

        int maxCount = _Neighbours.Max(n => n);

        return maxCount;
    }

    public static int Pentagonal(Grain[,] grains, int x, int y)
    {
        List<int> _Neighbours = new List<int>();

        if (BOUNDARY_CONDITION == BoundaryCondition.Periodic)
        {
            int x_l = x - 1 < 0 ? SIZE_X - 1 : x - 1;
            int x_p = x + 1 >= SIZE_X ? 0 : x + 1;
            int y_d = y + 1 >= SIZE_Y ? 0 : y + 1;
            int y_g = y - 1 < 0 ? SIZE_Y - 1 : y - 1;

            switch (PENTAGONAL_NEIGHBOURHOOD)
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

            switch (PENTAGONAL_NEIGHBOURHOOD)
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

        _Neighbours = _Neighbours.Where(n => n != 0).ToList();

        if (_Neighbours.Count == 0)
            return 0;

        int maxCount = _Neighbours.Max(n => n);

        return maxCount;

    }

    public static int Hexagonal(Grain[,] grains, int x, int y)
    {
        List<int> _Neighbours = new List<int>();

        if (BOUNDARY_CONDITION == BoundaryCondition.Periodic)
        {
            int x_l = x - 1 < 0 ? SIZE_X - 1 : x - 1;
            int x_p = x + 1 >= SIZE_X ? 0 : x + 1;
            int y_d = y + 1 >= SIZE_Y ? 0 : y + 1;
            int y_g = y - 1 < 0 ? SIZE_Y - 1 : y - 1;

            switch (HEXAGONAL_NEIGHBOURHOOD)
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

            switch (HEXAGONAL_NEIGHBOURHOOD)
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

        _Neighbours = _Neighbours.Where(n => n != 0).ToList();

        if (_Neighbours.Count == 0)
            return 0;

        int maxCount = _Neighbours.Max(n => n);

        return maxCount;
    }
}
