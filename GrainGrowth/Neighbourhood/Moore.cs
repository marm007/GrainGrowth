using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Config;

public class Moore : NeighbourhoodAbstract
{
    public Moore()
    {

    }

    public override List<string> Neighbours { get { return _Neighbours; }  }

    public override List<Grain> NeighboursGrains => throw new NotImplementedException();

    private List<string> _Neighbours;


    public override void GetNeighbours(Grain[,] grains, int x, int y)
    {
        _Neighbours = new List<string>();

        if (BOUNDARY_CONDITION == BoundaryCondition.Periodic)
        {
            int x_l = x - 1 < 0 ? SIZE_X - 1 : x - 1;
            int x_p = x + 1 >= SIZE_X ? 0 : x + 1;
            int y_d = y + 1 >= SIZE_Y ? 0 : y + 1;
            int y_g = y - 1 < 0 ? SIZE_Y - 1 : y - 1;

            _Neighbours.Add(grains[y, x_l].State.ToString()); // s_l
            _Neighbours.Add(grains[y, x_p].State.ToString()); // s_p
            _Neighbours.Add(grains[y_d, x].State.ToString()); // s_d
            _Neighbours.Add(grains[y_g, x].State.ToString()); // s_g

            _Neighbours.Add(grains[y_d, x_l].State.ToString()); // s_d_l
            _Neighbours.Add(grains[y_d, x_p].State.ToString()); // s_d_p

            _Neighbours.Add(grains[y_g, x_l].State.ToString()); // s_g_l
            _Neighbours.Add(grains[y_g, x_p].State.ToString()); // s_g_p
        }
        else
        {
            int x_l = x - 1 < 0 ? x : x - 1;
            int x_p = x + 1 >= SIZE_X ? x : x + 1;
            int y_d = y + 1 >= SIZE_Y ? y : y + 1;
            int y_g = y - 1 < 0 ? y : y - 1;


            _Neighbours.Add(grains[y, x_l].State.ToString()); // s_l
            _Neighbours.Add(grains[y, x_p].State.ToString()); // s_p
            _Neighbours.Add(grains[y_d, x].State.ToString()); // s_d
            _Neighbours.Add(grains[y_g, x].State.ToString()); // s_g

            _Neighbours.Add(grains[y_d, x_l].State.ToString()); // s_d_l
            _Neighbours.Add(grains[y_d, x_p].State.ToString()); // s_d_p

            _Neighbours.Add(grains[y_g, x_l].State.ToString()); // s_g_l
            _Neighbours.Add(grains[y_g, x_p].State.ToString()); // s_g_p

        }
    }

    public override void GetAllNeighbours(Grain[,] grains, int x, int y)
    {
        throw new NotImplementedException();
    }
}
