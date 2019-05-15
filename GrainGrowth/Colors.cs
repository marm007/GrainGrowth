using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Colors
{

    private static int COLOR_NUMBER = 0;

    private static Random random = new Random();



    public static Dictionary<int, Color> colors = new Dictionary<int, Color>();

    public static void Initialize()
    {
        COLOR_NUMBER = 0;
        colors = new Dictionary<int, Color>();
    }

    public static int RandomColor()
    {
        COLOR_NUMBER++;

        int r = random.Next(256);
        int g = random.Next(256);
        int b = random.Next(256);

        Colors.colors.Add(COLOR_NUMBER, Color.FromArgb(r, g, b));

        return COLOR_NUMBER;
    }
}

