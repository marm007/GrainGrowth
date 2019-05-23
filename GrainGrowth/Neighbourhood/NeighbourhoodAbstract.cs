using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class NeighbourhoodAbstract
{
    public abstract List<string> Neighbours
    {
        get;
    }

    public abstract List<Grain> NeighboursGrains
    {
        get;
    }

    public abstract void GetNeighbours(Grain[,] grains, int x, int y);

    public abstract void GetAllNeighbours(Grain[,] grains, int x, int y);

   

}
