using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Config;

public class Recrystallization
{
    private static Random random = new Random();

    private const double maxTime = 0.2;
    private const double A = 86710969050178.5;
    private const double B = 9.41268203527779;

    private const double A0 = 0.000000000257;
    private const double B0 = 80000000000;

    private double criticallRo = 4.21584E+12;// 46842668.25; //4.21584E+12;//8.02E+07 ;// 4.21584E+12; // 4.21584E+12;


    private const double deltaTime = 0.001;
    private const double sigma0 = 0;
    private const double value = 1.9;

    private double time = 0;

    private const double cellPercent = 0.3;
    private const double smallPackagePercent = 0.001;

    private List<double> ro = null;
    private List<double> sigma = null;

    private List<double> checkedRo = null;
    private List<double> checkedRoFromGrians = null;

    private bool[,] tmpGrains = null;

    public  double MaxTime => maxTime;

    public  double DeltaTime => deltaTime;


    public Recrystallization(Grain[,] grains)
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                grains[i, j].Density = 0;
                grains[i, j].Recrystallized = false;
            }
        }



        Colors.InitializeRecrystallizationColrs();
        
        ro = new List<double>();
        sigma = new List<double>();
        checkedRo = new List<double>();
        checkedRoFromGrians = new List<double>();

        tmpGrains = new bool[SIZE_Y, SIZE_X];

        time = 0;

        double _ro = (A / B) + (1 - (A / B)) * (Math.Exp(-B * time));
        double _sigma = sigma0 + value * A0 * B0 * Math.Sqrt(_ro);

        criticallRo = criticallRo / (SIZE_X * SIZE_Y);

        Console.WriteLine("Critical " + criticallRo);

        ro.Add(_ro);
        sigma.Add(_sigma);
        checkedRo.Add(0);
        checkedRoFromGrians.Add(_ro);


        for(int i = 0; i < SIZE_Y; i++)
        {
            for(int j = 0; j < SIZE_X; j++)
            {
                tmpGrains[i, j] = false;

                List<int> neighbours = NeighbourhoodFactory.GetNeighboursMonteCarlo(grains, j, i);

                if (neighbours.Where(s => s != grains[i, j].State).ToList().Count > 0)
                {
                    grains[i, j].OnBorder = true;
                }

            }
        }

        
    }

    public Grain[,] DislocationsPartition(Grain[,] grains, Graphics g)
    {
        List<Grain> all = grains.OfType<Grain>().ToList();
        List<Grain> allOnBorder = all.Where(s => s.OnBorder == true).ToList();
        List<Grain> allNotOnBorder = all.Where(s => s.OnBorder == false).ToList();

        if (allOnBorder == null || allNotOnBorder == null || all == null)
            return grains;

            
        time += deltaTime;
        Console.WriteLine(time);

        double _ro = (A / B) + (1 - (A / B)) * (Math.Exp(-B * time));
        double _sigma = sigma0 + value * A0 * B0 * Math.Sqrt(_ro);

        ro.Add(_ro);
        sigma.Add(_sigma);

        double deltaRo = ro.ElementAt(ro.Count - 1) - ro.ElementAt(ro.Count - 2);
        double oneCellRo = deltaRo / (SIZE_X * SIZE_Y);


        double firstPackage = oneCellRo * cellPercent;
        
        
        foreach(Grain grain in grains)
        {
            grain.Density += firstPackage;
        }

        double checkRo = firstPackage * all.Count;

        random = new Random();

        deltaRo -= firstPackage * all.Count;

        double smallPackage = deltaRo  * smallPackagePercent ;

        while (deltaRo  > 0)
        {
            int probability = random.Next(101);

            if (probability >= 20)
            {
                int index = random.Next(allOnBorder.Count);
                Grain grain = allOnBorder.ElementAt(index);

                //if (!grain.Recrystallized)
                //{
                    grains[grain.Y, grain.X].Density += smallPackage ;

                    deltaRo -= smallPackage;
                    checkRo += smallPackage;
                //}
            }
            else
            {
                int index = random.Next(allNotOnBorder.Count);
                Grain grain = allNotOnBorder.ElementAt(index);

                //if (!grain.Recrystallized)
                //{
                    grains[grain.Y, grain.X].Density += smallPackage;
                    deltaRo -= smallPackage;
                    checkRo += smallPackage;

                //}
            }
        }

        

        double _density = 0;
        foreach (Grain grain in grains)
        {
            _density += grain.Density;
        }

        checkedRo.Add(checkRo);
        checkedRoFromGrians.Add(_density);



        grains = Inclusion(grains, g);
        grains = Simulation(grains, g);

        return grains;
    }


    public Grain[,] Inclusion(Grain[,] grains, Graphics g)
    {
        List<Grain> all = grains.OfType<Grain>().ToList();
        List<Grain> allOnBorder = all.Where(s => s.OnBorder == true).ToList();

        foreach (Grain grain in allOnBorder)
        {
            if (grain.Density > criticallRo && !grain.Recrystallized )
            {
                tmpGrains[grain.Y, grain.X] = true;
                grains[grain.Y, grain.X].Density = criticallRo;
                grains[grain.Y, grain.X].Recrystallized = true;
                grains[grain.Y, grain.X].DisplayRecrystallized(g, Colors.GetRecrystallizationColor());
            }
            else if(grain.Density > criticallRo && grain.Recrystallized)
            {
                tmpGrains[grain.Y, grain.X] = true;
                grains[grain.Y, grain.X].Density = criticallRo;
            }
        }

        return grains;
    }


    public Grain[,] Simulation(Grain[,] grains, Graphics g)
    {
       bool[,] recrystalized = new bool[SIZE_Y, SIZE_X];

        for (int y = 0; y < SIZE_Y; y++)
        {
            for (int x = 0; x < SIZE_X; x++)
            {
                recrystalized[y, x] = false;
            }
        }

        for (int y = 0; y < SIZE_Y; y++)
        {
            for (int x = 0; x < SIZE_X; x++)
            {
                List<Grain> neighbours = NeighbourhoodFactory.GetNeighboursGrains(grains, x, y);

                foreach (Grain neighbour in neighbours)
                {
                    if (tmpGrains[neighbour.Y, neighbour.X])
                    {
                        bool rescrystallize = true;

                        foreach (Grain neighbour1 in neighbours)
                        {
                            if (neighbour1.Density >= grains[y, x].Density )
                            {
                                rescrystallize = false;
                                break;
                            }
                        }

                        if (rescrystallize && !grains[y, x].Recrystallized)
                        {
                            recrystalized[y, x] = true;
                            grains[y, x].Density = 0;
                            grains[y, x].Recrystallized = true;
                            grains[y, x].DisplayRecrystallized(g, Colors.GetRecrystallizationColor());
                        }

                        break;
                    }
                }

            }
        }

        for (int y = 0; y < SIZE_Y; y++)
        {
            for (int x = 0; x < SIZE_X; x++)
            {
                tmpGrains[y, x] = recrystalized[y, x];
            }
        }

        return grains;
    }

    public void DisplayDensity(Grain[,] grains, Graphics g)
    {
        SortedList<double, int> densityCount = new SortedList<double, int>();

        for (int y = 0; y < SIZE_Y; y++)
        {
            for (int x = 0; x < SIZE_X; x++)
            {
                double density = grains[y, x].Density;

                if (densityCount.ContainsKey(density))
                {
                    densityCount[density]++;
                }
                else
                {
                    densityCount.Add(density, 1);
                }
            }
        }

    
        /* 
        double highest = densityCount.IndexOfKey(densityCount.Count - 1);
        double lowest = densityCount.IndexOfKey(0);
        double step = densityCount.Count / NUMBER_OF_COLORS_RECRYSTALLIZATION;

        int i = 0;
        int index = 0;
        SortedList<double, int> newDensityCount = new SortedList<double, int>();

        foreach (KeyValuePair<double, int> keyValue in densityCount)
        {
            i += keyValue.Value;
            if(i >= step)
            {
                i = 0;
                index++;
                newDensityCount.Add(keyValue.Key, index);
            }
            else
            {
                newDensityCount.Add(keyValue.Key, index);
            }
        }
        */

        DensityColors.InitializeColors(densityCount.Count);


        for (int y = 0; y < SIZE_Y; y++)
        {
            for (int x = 0; x < SIZE_X; x++)
            {
                int colorIndex = densityCount.IndexOfKey(grains[y,x].Density);
;               System.Drawing.SolidBrush cellBrush = new System.Drawing.SolidBrush(DensityColors.colors[colorIndex]);

                g.FillRectangle(cellBrush, (grains[y, x].X) * CELL_SIZE + (int)GRID_STATE, (grains[y, x].Y) * CELL_SIZE + (int)GRID_STATE,
                CELL_SIZE - (int)GRID_STATE, CELL_SIZE - (int)GRID_STATE);
            }
        }


    }

    public void SaveToFile()
    {
        string text = null;
        try
        {
            text = File.ReadAllText("number.txt");
        }catch(IOException exception) { }

        int number = 0;

        if(text != null)
        {

            number = int.Parse(text);
            number += 1;

            File.WriteAllText("number.txt", number.ToString());

        }
        else
        {
            File.WriteAllText("number.txt", number.ToString());

        }

        string strFilePath = "data" + number + ".csv";
        string strSeperator = ";";
        StringBuilder sbOutput = new StringBuilder();

        String[] toWrite = new String[5];
        double _time = 0;

        for (int i = 0; i < ro.Count; i++)
        {
            toWrite[0] = _time.ToString();

            toWrite[1] = ro.ElementAt(i).ToString();
            toWrite[2] = sigma.ElementAt(i).ToString();
            toWrite[3] = checkedRo.ElementAt(i).ToString();
            toWrite[4] = checkedRoFromGrians.ElementAt(i).ToString();
            _time += DeltaTime;
            sbOutput.AppendLine(string.Join(strSeperator, toWrite));
        }

        File.WriteAllText(strFilePath, sbOutput.ToString());
    }
}