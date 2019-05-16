using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Config
{
    public static int CELL_SIZE = 20;
    private static int sIZE_X = 0;
    private static int sIZE_Y = 0;

    public static GridState GRID_STATE = GridState.Disable;


    public static Neighbourhood NEIGHBOURHOOD = Neighbourhood.von_Neumann;
    public static Nucleation NUCLEATION = Nucleation.Homogeneus;
    public static BoundaryCondition BOUNDARY_CONDITION = BoundaryCondition.Periodic;

    public static int SIZE_X { get { return sIZE_X; } }

    public static int SIZE_Y { get { return sIZE_Y; } }

    public enum Neighbourhood { von_Neumann, Moore };
    public enum Nucleation { Homogeneus, Radial, Random, Banned};
    public enum BoundaryCondition { Periodic, Nonperiodic };
    public enum GridState { Enable = 1, Disable = 0 };
    

    public static void SET_SIZES(int sX, int sY)
    {
        sIZE_X = sX;
        sIZE_Y = sY;
    }


}
