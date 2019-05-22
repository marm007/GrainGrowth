using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class EnergyColors
{
    public static List<Color> colors = null;
    
    private static int maxNumberOfColors = 9;

    public static void InitializeColors()
    {
        colors = new List<Color>();
        int aR = 0; int aG = 0; int aB = 255;  
        int bR = 255; int bG = 0; int bB = 0;    

        for (int i = 0; i < maxNumberOfColors; i++)
        {
            float value = ((float) i / ((float) maxNumberOfColors));  
            int red = (int) ((float)(bR - aR) * value + aR);      
            int green = (int) ((float)(bG - aG) * value + aG);      
            int blue = (int) ((float)(bB - aB) * value + aB);     

            colors.Add(Color.FromArgb(red, green, blue));
        }
    }
}
