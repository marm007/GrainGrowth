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

    private static int RECRYSTALIZATION_COLOR_NUMBER = 0;


    public static Dictionary<int, Color> colors = new Dictionary<int, Color>();
    public static List<Color> recrystallizationColors = new List<Color>();

    public static void Initialize()
    {
        COLOR_NUMBER = 0;
        colors = new Dictionary<int, Color>();
    }

    public static void InitializeRecrystallizationColrs()
    {
        RECRYSTALIZATION_COLOR_NUMBER = 0;

        int r = 255;
        int g = 75;
        int b = 75;

        while(r >= 75)
        {
            if(g == 0 && b == 0)
            {
                r--;
            }

            if(g > 0 && b > 0)
            {
                g--;
                b--;
            }

            Colors.recrystallizationColors.Add(Color.FromArgb(r, g, b));
        }
    }

    public static Color GetRecrystallizationColor()
    {
        if (RECRYSTALIZATION_COLOR_NUMBER >= recrystallizationColors.Count)
            RECRYSTALIZATION_COLOR_NUMBER = 0;

        Color color = recrystallizationColors.ElementAt(RECRYSTALIZATION_COLOR_NUMBER);

        RECRYSTALIZATION_COLOR_NUMBER++;

        return color;
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

