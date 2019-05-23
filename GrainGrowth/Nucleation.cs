using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Colors;
using static Config;

public class Nucleation
{

    public static void Homogeneus(Grid grid, Simulation grainGrowth, Bitmap bitmap, int numberInRow, int numberInCol)
    {
        Random rnd = new Random();

        int rowSpace = SIZE_X / numberInRow;
        int colSpace = SIZE_Y / numberInCol;

        int offsetRow = (SIZE_X - rowSpace) / 2;
        int offsetCol = (SIZE_Y - colSpace) / 2;


        for (int i = 0; i < numberInCol; i++)
        {
            for (int j = 0; j < numberInRow; j++)
            {
                int y = i * colSpace ;
                int x = j * rowSpace ;

                if (grainGrowth.Tab[y, x].State != 0)
                    return;


                grainGrowth.Tab[y,x].State = Colors.RandomColor();
                grainGrowth.Tab[y, x].Display(bitmap);
            }
        }
    }

    public static Grain[,] Homogeneus(Grid grid, Simulation grainGrowth, int numberInRow, int numberInCol)
    {
        int sizeX = SIZE_X;
        int sizeY = SIZE_Y;

        Grain[,] tab = new Grain[sizeY, sizeX];
        for (int i = 0; i < SIZE_Y; i++)
        {

            for (int j = 0; j < SIZE_X; j++)
            {
                tab[i, j] = grainGrowth.Tab[i, j].Copy();
                tab[i, j].State = 0;
            }
        }

        Random rnd = new Random();

        int rowSpace = SIZE_X / numberInRow;
        int colSpace = SIZE_Y / numberInCol;

        int offsetRow = (SIZE_X - rowSpace) / 2;
        int offsetCol = (SIZE_Y - colSpace) / 2;


        for (int i = 0; i < numberInCol; i++)
        {
            for (int j = 0; j < numberInRow; j++)
            {
                int y = i * colSpace + offsetCol;
                int x = j * rowSpace + offsetRow;
                tab[y, x].State = Colors.RandomColor();
            }
        }


        return tab;
    }

    
    public static void Random(Grid grid, Simulation grainGrowth, int number, Bitmap bitmap)
    {
        Random rnd = new Random();



        List<int> listX = new List<int>();
        List<int> listY = new List<int>();

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                if (grainGrowth.Tab[i, j].State == 0)
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

                grainGrowth.Tab[y, x].State = Colors.RandomColor();
                grainGrowth.Tab[y, x].Display(bitmap);
            }

        }
    }

    public static Grain[,] Random(Grid grid, Simulation grainGrowth, int number)
    {
        Random rnd = new Random();

        int sizeX = SIZE_X;
        int sizeY = SIZE_Y;

        Grain[,] tab = new Grain[sizeY, sizeX];
        for (int i = 0; i < SIZE_Y; i++)
        {

            for (int j = 0; j < SIZE_X; j++)
            {
                tab[i, j] = grainGrowth.Tab[i, j].Copy();
                tab[i, j].State = 0;
            }
        }

        List<int> listX = new List<int>();
        List<int> listY = new List<int>();

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                if (grainGrowth.Tab[i, j].State == 0)
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


                tab[y, x].State = Colors.RandomColor();
            }
        }

        return tab;
    }

    
    public static void Radial(Grid grid, Simulation grainGrowth, int r, int number, Bitmap bitmap, TextBox alertTextBox, GrainGrowth.Form1 form)
    {

        Random rnd = new Random();



        List<int> listX = new List<int>();
        List<int> listY = new List<int>();

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                if (grainGrowth.Tab[i, j].State == 0)
                {
                    listX.Add(j);
                    listY.Add(i);
                }
            }
        }

        int n = 0;

        while(n < number && listX.Count > 0)
        {
            int indexList = rnd.Next(listX.Count);

            int j = listX.ElementAt(indexList);
            int i = listY.ElementAt(indexList);

            listX.RemoveAt(indexList);
            listY.RemoveAt(indexList);

            int maxY = i + r >= SIZE_Y ? SIZE_Y - 1 : i + r;
            int maxX = j + r >= SIZE_X ? SIZE_X - 1 : j + r;
            int minY = i - r < 0 ? 0 : i - r;
            int minX = j - r < 0 ? 0 : j - r;
         
            bool isInRange = false;

            if (grainGrowth.Tab[maxY, j].State != 0)
            {
                isInRange = true;
                continue;
            }

            if (grainGrowth.Tab[minY, j].State != 0)
            {
                isInRange = true;
                continue;
            }

            if (grainGrowth.Tab[i, maxX].State != 0)
            {
                isInRange = true;
                continue;
            }

            if (grainGrowth.Tab[i, minX].State != 0)
            {
                isInRange = true;
                continue;
            }

            maxY = i + r >= SIZE_Y ? SIZE_Y - 1 : i + r - 1;
            maxX = j + r >= SIZE_X ? SIZE_X - 1 : j + r - 1;
            minY = i - r < 0 ? 0 : i - r + 1;
            minX = j - r < 0 ? 0 : j - r + 1;

            for (int x = minX; x <= maxX; ++x)
            {
                for (int y = minY; y <= maxY; ++y)
                {
                    if (!(y == i && x == j) && grainGrowth.Tab[y, x].State != 0)
                    {
                        isInRange = true;
                        break;
                    }
                }

                if (isInRange)
                    break;
            }

            if (isInRange)
                continue;


            grainGrowth.Tab[i, j].State = Colors.RandomColor();
            grainGrowth.Tab[i, j].Display(bitmap);

            n++;
        }

        if(n < number)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                form.AlertTextBoxAction("Not added " + (number - n) + " grain/s", true);
                
                System.Threading.Thread.Sleep(3000);

                form.AlertTextBoxAction("", false);

            });

            backgroundWorker.RunWorkerAsync();

        }


          

       
    }

    public static Grain[,] Radial(Grid grid, Simulation grainGrowth, int r, int number, TextBox alertTextBox, GrainGrowth.Form1 form)
    {
        int sizeX = SIZE_X;
        int sizeY = SIZE_Y;

        Grain[,] tab = new Grain[sizeY, sizeX];
        for (int i = 0; i < SIZE_Y; i++)
        {

            for (int j = 0; j < SIZE_X; j++)
            {
                tab[i, j] = grainGrowth.Tab[i, j].Copy();
                tab[i, j].State = 0;
            }
        }

        Random rnd = new Random();



        List<int> listX = new List<int>();
        List<int> listY = new List<int>();

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                if (grainGrowth.Tab[i, j].State == 0)
                {
                    listX.Add(j);
                    listY.Add(i);
                }
            }
        }

        int n = 0;

        while (n < number && listX.Count > 0)
        {
            int indexList = rnd.Next(listX.Count);

            int j = listX.ElementAt(indexList);
            int i = listY.ElementAt(indexList);

            listX.RemoveAt(indexList);
            listY.RemoveAt(indexList);


            int maxY = i + r >= SIZE_Y ? SIZE_Y - 1 : i + r;
            int maxX = j + r >= SIZE_X ? SIZE_X - 1 : j + r;
            int minY = i - r < 0 ? 0 : i - r;
            int minX = j - r < 0 ? 0 : j - r;

            bool isInRange = false;

            for (int y = i, index = 0, iterator = 0; y <= maxY; y++)
            {
                for (int x = j; x <= maxX + index; x++)
                {
                    if (!(y == i && x == j) && grainGrowth.Tab[y, x].State != 0)
                    {
                        isInRange = true;
                        break;
                    }
                }

                if (j + r - iterator < SIZE_X)
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
                    if (!(y == i && x == j) && grainGrowth.Tab[y, x].State != 0)
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
                    if (!(y == i && x == j) && grainGrowth.Tab[y, x].State != 0)
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
                    if (!(y == i && x == j) && grainGrowth.Tab[y, x].State != 0)
                    {
                        isInRange = true;
                        break;
                    }
                }

                if (j + r - iterator < SIZE_X)
                {
                    index--;
                }

                iterator++;

                if (isInRange)
                    break;
            }


            if (isInRange)
                continue;


            tab[i, j].State = Colors.RandomColor();
            n++;

        }

        if (n < number)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                form.AlertTextBoxAction("Not added " + (number - n) + " grain/s", true);

                System.Threading.Thread.Sleep(3000);

                form.AlertTextBoxAction("", false);


            });

            backgroundWorker.RunWorkerAsync();
            
        }

        return tab;
    }

}

