using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Colors;

public class Nucleation
{
    public static void Homogeneus(Grid grid, Simulation grainGrowth, PictureBox pictureBox, int row, int col)
    {
        Random rnd = new Random();

        for (int i = 0; i < grid.SizeY; i++)
        {
            for (int j = 0; j < grid.SizeX; j++)
            {
                if (i % row == 0 && j % col == 0 && grainGrowth.Tab[i, j] == 0)
                {
                    grainGrowth.Tab[i, j] = Colors.RandomColor();
                    grainGrowth.Display(pictureBox.CreateGraphics(), j, i, grainGrowth.Tab[i, j]);
                }
            }
        }
    }

    public static int[,] Homogeneus(Grid grid, Simulation grainGrowth, int row, int col)
    {
        int sizeX = grid.SizeX;
        int sizeY = grid.SizeY;

        int [,] tab = new int[sizeY, sizeX];
        for (int i = 0; i < grid.SizeY; i++)
        {

            for (int j = 0; j < grid.SizeX; j++)
            {
                tab[i, j] = 0;
            }
        }

        Random rnd = new Random();

        for (int i = 0; i < grid.SizeY; i++)
        {
            for (int j = 0; j < grid.SizeX; j++)
            {
                if (i % row == 0 && j % col == 0 && grainGrowth.Tab[i, j] == 0)
                {
                    tab[i, j] = Colors.RandomColor();
                }
            }
        }

        return tab;
    }

    
    public static void Random(Grid grid, Simulation grainGrowth, int number, PictureBox pictureBox)
    {
        Random rnd = new Random();



        List<int> listX = new List<int>();
        List<int> listY = new List<int>();

        for (int i = 0; i < grid.SizeY; i++)
        {
            for (int j = 0; j < grid.SizeX; j++)
            {
                if (grainGrowth.Tab[i, j] == 0)
                {
                    listX.Add(j);
                    listY.Add(i);
                }
            }
        }

        for (int i = 0; i < number; i++)
        {
            if (listX.Count > 0)
            {
                int index = rnd.Next(listX.Count);

                int x = listX.ElementAt(index);
                int y = listY.ElementAt(index);

                listX.RemoveAt(index);
                listY.RemoveAt(index);

                grainGrowth.Tab[y, x] = Colors.RandomColor();
                grainGrowth.Display(pictureBox.CreateGraphics(), x, y, grainGrowth.Tab[y, x]);
            }

        }
    }

    public static int[,] Random(Grid grid, Simulation grainGrowth, int number)
    {
        Random rnd = new Random();

        int sizeX = grid.SizeX;
        int sizeY = grid.SizeY;

        int[,] tab = new int[sizeY, sizeX];
        for (int i = 0; i < grid.SizeY; i++)
        {

            for (int j = 0; j < grid.SizeX; j++)
            {
                tab[i, j] = 0;
            }
        }

        List<int> listX = new List<int>();
        List<int> listY = new List<int>();

        for (int i = 0; i < grid.SizeY; i++)
        {
            for (int j = 0; j < grid.SizeX; j++)
            {
                if (grainGrowth.Tab[i, j] == 0)
                {
                    listX.Add(j);
                    listY.Add(i);
                }
            }
        }

        for (int i = 0; i < number; i++)
        {
            if (listX.Count > 0)
            {
                int index = rnd.Next(listX.Count);

                int x = listX.ElementAt(index);
                int y = listY.ElementAt(index);

                listX.RemoveAt(index);
                listY.RemoveAt(index);


                tab[y, x] = Colors.RandomColor();
            }
        }

        return tab;
    }

    
    public static void Radial(Grid grid, Simulation grainGrowth, int r, int number, PictureBox pictureBox)
    {

        Random rnd = new Random();

        int n = 0;

        for (int i = 0; i < grid.SizeY; i++)
        {
            for (int j = 0; j < grid.SizeX; j++)
            {
                if (grainGrowth.Tab[i, j] != 0)
                    continue;

                int maxY = i + r >= grid.SizeY ? grid.SizeY - 1  : i + r;
                int maxX = j + r >= grid.SizeX ? grid.SizeX - 1 : j + r;
                int minY = i - r < 0 ? 0 : i - r;
                int minX = j - r < 0 ? 0 : j - r;

                bool isInRange = false;

                for (int y = i, index = 0, iterator = 0; y <= maxY; y++)
                {
                    for(int x = j; x <= maxX + index; x++)
                    {
                        if (!(y == i && x == j) && grainGrowth.Tab[y, x] != 0)
                        {
                            isInRange = true;
                            break;
                        }
                    }

                    if(j + r - iterator < grid.SizeX )
                    {
                        index--;
                    }

                    iterator++;

                    if (isInRange)
                        break;
                }

                if (isInRange)
                    continue;

                for (int y = i, index = 0, iterator = 0; y >= minY; y--)
                {
                    for (int x = j; x >= minX + index; x--)
                    {
                        if (!(y == i && x == j) && grainGrowth.Tab[y, x] != 0)
                        {
                            isInRange = true;
                            break;
                        }
                    }

                    if (j - r + iterator >= 0)
                    {
                        index++;
                    }

                    iterator++;


                    if (isInRange)
                        break;
                }

                if (isInRange)
                    continue;

                for (int y = i, index = 0, iterator = 0; y <= maxY; y++)
                {
                    for (int x = j; x >= minX + index; x--)
                    {
                        if (!(y == i && x == j) && grainGrowth.Tab[y, x] != 0)
                        {
                            isInRange = true;
                            break;
                        }
                    }

                    if (j - r + iterator >= 0)
                    {
                        index++;
                    }

                    iterator++;

                    if (isInRange)
                        break;
                }

                if (isInRange)
                    continue;

                for (int y = i, index = 0, iterator = 0; y >= minY; y--)
                {
                    for (int x = j; x <= maxX + index; x++)
                    {
                        if (!(y == i && x == j) && grainGrowth.Tab[y, x] != 0)
                        {
                            isInRange = true;
                            break;
                        }
                    }

                    if (j + r - iterator < grid.SizeX)
                    {
                        index--;
                    }

                    iterator++;

                    if (isInRange)
                        break;
                }


                if (isInRange)
                    continue;


                grainGrowth.Tab[i, j] = Colors.RandomColor();
                grainGrowth.Display(pictureBox.CreateGraphics(), j, i, grainGrowth.Tab[i, j]);
                n++;

                if (n >= number)
                    break;
            }

            if (n >= number)
                break;
        }
    }

    public static int[,] Radial(Grid grid, Simulation grainGrowth, int r, int number)
    {
        int sizeX = grid.SizeX;
        int sizeY = grid.SizeY;

        int[,] tab = new int[sizeY, sizeX];
        for (int i = 0; i < grid.SizeY; i++)
        {

            for (int j = 0; j < grid.SizeX; j++)
            {
                tab[i, j] = 0;
            }
        }

        Random rnd = new Random();

        int n = 0;

        for (int i = 0; i < grid.SizeY; i++)
        {
            for (int j = 0; j < grid.SizeX; j++)
            {
                if (tab[i,j] != 0 || grainGrowth.Tab[i, j] != 0)
                {
                    continue;
                }

                int maxY = i + r >= grid.SizeY ? grid.SizeY - 1 : i + r;
                int maxX = j + r >= grid.SizeX ? grid.SizeX - 1 : j + r;
                int minY = i - r < 0 ? 0 : i - r;
                int minX = j - r < 0 ? 0 : j - r;

                bool isInRange = false;

                for (int y = i, index = 0, iterator = 0; y <= maxY; y++)
                {
                    for (int x = j; x <= maxX + index; x++)
                    {
                        if (!(y == i && x == j) && (tab[y, x] != 0 ||  grainGrowth.Tab[y, x] != 0))
                        {
                            isInRange = true;
                            break;
                        }
                    }

                    if (j + r - iterator < grid.SizeX)
                    {
                        index--;
                    }

                    iterator++;

                    if (isInRange)
                        break;
                }

                if (isInRange)
                    continue;

                for (int y = i, index = 0, iterator = 0; y >= minY; y--)
                {
                    for (int x = j; x >= minX + index; x--)
                    {
                        if (!(y == i && x == j) && (tab[y, x] != 0 ||  grainGrowth.Tab[y, x] != 0))
                        {
                            isInRange = true;
                            break;
                        }
                    }

                    if (j - r + iterator >= 0)
                    {
                        index++;
                    }

                    iterator++;


                    if (isInRange)
                        break;
                }

                if (isInRange)
                    continue;

                for (int y = i, index = 0, iterator = 0; y <= maxY; y++)
                {
                    for (int x = j; x >= minX + index; x--)
                    {
                        if (!(y == i && x == j) && (tab[y, x] != 0 ||  grainGrowth.Tab[y, x] != 0))
                        {
                            isInRange = true;
                            break;
                        }
                    }

                    if (j - r + iterator >= 0)
                    {
                        index++;
                    }

                    iterator++;

                    if (isInRange)
                        break;
                }

                if (isInRange)
                    continue;

                for (int y = i, index = 0, iterator = 0; y >= minY; y--)
                {
                    for (int x = j; x <= maxX + index; x++)
                    {
                        if (!(y == i && x == j) && (tab[y, x] != 0 ||  grainGrowth.Tab[y, x] != 0))
                        {
                            isInRange = true;
                            break;
                        }
                    }

                    if (j + r - iterator < grid.SizeX)
                    {
                        index--;
                    }

                    iterator++;

                    if (isInRange)
                        break;
                }


                if (isInRange)
                    continue;

                tab[i, j] = Colors.RandomColor();

                n++;

                if (n >= number)
                    break;
            }

            if (n >= number)
                break;
        }

        return tab;
    }
}

