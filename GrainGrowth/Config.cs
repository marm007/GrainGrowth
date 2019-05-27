using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Config
{
    public static int CELL_SIZE = 20;
    public static float RADIUS = 2.0f;
    public static float ENERGY_RADIUS = 2.0f;
    public static Random SIMULATION_RANDOM = new Random();

    private static int sIZE_X = 0;
    private static int sIZE_Y = 0;

    public static GridState GRID_STATE = GridState.Disable;
    public static EnergyState ENERGY_STATE = EnergyState.Disable;


    public static Neighbourhood NEIGHBOURHOOD = Neighbourhood.vonNeumann;
    public static Nucleation NUCLEATION = Nucleation.Homogeneus;
    public static BoundaryCondition BOUNDARY_CONDITION = BoundaryCondition.Periodic;
    public static PentagonalNeighbourhood PENTAGONAL_NEIGHBOURHOOD = PentagonalNeighbourhood.Left;
    public static HexagonalNeighbourhood HEXAGONAL_NEIGHBOURHOOD = HexagonalNeighbourhood.Right;

    public static int SIZE_X { get { return sIZE_X; } }

    public static int SIZE_Y { get { return sIZE_Y; } }

    public static bool BREAK_SIMULATION = false;


    public enum Neighbourhood { vonNeumann, Moore, Pentagonal, Hexagonal, Radial };
    public enum PentagonalNeighbourhood { Left, Right, Top, Bottom };
    public enum HexagonalNeighbourhood { Right, Left, Random };
    public enum Nucleation { Homogeneus, Radial, Random, Banned};
    public enum BoundaryCondition { Periodic, Nonperiodic };
    public enum GridState { Enable = 1, Disable = 0 };
    public enum EnergyState { Enable, Disable };
    

    public static void SET_SIZES(int sX, int sY)
    {
        sIZE_X = sX;
        sIZE_Y = sY;
    }


}
