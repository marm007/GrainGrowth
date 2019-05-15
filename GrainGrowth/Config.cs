using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Config
{
    public static int CELL_SIZE = 20;
    public static GridState GRID_STATE = GridState.Disable;


    public static Neighbourhood NEIGHBOURHOOD = Neighbourhood.von_Neumann;
    public static Nucleation NUCLEATION = Nucleation.Homogeneus;
    public static BoundaryCondition BOUNDARY_CONDITION = BoundaryCondition.Periodic;

    public enum Neighbourhood { von_Neumann, Moore };
    public enum Nucleation { Homogeneus, Radial, Random, Banned};
    public enum BoundaryCondition { Periodic, Nonperiodic };
    public enum GridState { Enable = 1, Disable = 0 };


}
