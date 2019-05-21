using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Config;

public static class NeighbourhoodFactory
{
    public static NeighbourhoodAbstract Create()
    {
        var type = Type.GetType(typeof(NeighbourhoodAbstract).Namespace + "." + NEIGHBOURHOOD.ToString(), throwOnError: false);

        if (type == null)
        {
            throw new InvalidOperationException(NEIGHBOURHOOD.ToString() + " is not a known dto type");
        }

        if (!typeof(NeighbourhoodAbstract).IsAssignableFrom(type))
        {
            throw new InvalidOperationException(type.Name + " does not inherit from AbstractDto");
        }

        return (NeighbourhoodAbstract)Activator.CreateInstance(type);
    }
}
