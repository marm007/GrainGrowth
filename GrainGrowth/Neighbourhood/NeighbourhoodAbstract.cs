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

    public abstract void SetNeighbours(Grain[,] grains, int x, int y);
    
}
