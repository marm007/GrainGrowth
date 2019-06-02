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

    private const double cellPercent = 0.03;
    private const double smallPackagePercent = 0.001;

    private List<double> ro = null;
    private List<double> sigma = null;

    private bool[,] tmpGrains = null;

    public  double MaxTime => maxTime;

    public  double DeltaTime => deltaTime;

    public Recrystallization(Grain[,] grains)
    {
        ro = new List<double>();
        sigma = new List<double>();
        tmpGrains = new bool[SIZE_Y, SIZE_X];

        time = 0;

        double _ro = (A / B) + (1 - (A / B)) * (Math.Exp(-B * time));
        double _sigma = sigma0 + value * A0 * B0 * Math.Sqrt(_ro);

        criticallRo = criticallRo / (SIZE_X * SIZE_Y);

        Console.WriteLine("Critical " + criticallRo);

        ro.Add(_ro);
        sigma.Add(_sigma);


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

    public void DislocationsPartition(Grain[,] grains, Graphics g)
    {
        List<Grain> all = grains.OfType<Grain>().ToList();
        List<Grain> allOnBorder = all.Where(s => s.OnBorder == true).ToList();
        List<Grain> allNotOnBorder = all.Where(s => s.OnBorder == false).ToList();

            
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

        deltaRo -= firstPackage * all.Count;

        double smallPackage = deltaRo * smallPackagePercent ;

        while (deltaRo > 0)
        {


            int probability = random.Next(101);

            if (probability >= 20)
            {
                int index = random.Next(allOnBorder.Count);
                Grain grain = allOnBorder.ElementAt(index);

                if (!grain.Recrystallized)
                {
                    grains[grain.Y, grain.X].Density += smallPackage ;

                    if (grain.Density > criticallRo)
                    {
                        tmpGrains[grain.Y, grain.X] = true;
                        grain.Density = 0;
                        grains[grain.Y, grain.X].Recrystallized = true;
                        grains[grain.Y, grain.X].DisplayRecrystallized(g);
                    }

                    deltaRo -= smallPackage;

                }



            }
            else
            {
                int index = random.Next(allNotOnBorder.Count);
                Grain grain = allNotOnBorder.ElementAt(index);

                if (!grain.Recrystallized)
                {
                    grains[grain.Y, grain.X].Density += smallPackage;
                    deltaRo -= smallPackage;
                }
            }
        }

    }

    public void Simulation(Grain[,] grains, Graphics g)
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
                                Console.WriteLine(neighbour1.Density + " " + grains[y, x].Density);
                                rescrystallize = false;
                                break;
                            }
                        }

                        if (rescrystallize)
                        {
                            Console.WriteLine("RESC");

                            recrystalized[y, x] = true;
                            grains[y, x].Density = 0;
                            grains[y, x].Recrystallized = true;
                            grains[y, x].DisplayRecrystallized(g);
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
    }


    public void SaveToFile()
    {
        string strFilePath = "data.csv";
        string strSeperator = ";";
        StringBuilder sbOutput = new StringBuilder();

        String[] toWrite = new String[3];
        double _time = 0;

        for (int i = 0; i < ro.Count; i++)
        {
            toWrite[0] = _time.ToString();

            toWrite[1] = ro.ElementAt(i).ToString();
            toWrite[2] = sigma.ElementAt(i).ToString();
            _time += DeltaTime;
            sbOutput.AppendLine(string.Join(strSeperator, toWrite));
        }

        File.WriteAllText(strFilePath, sbOutput.ToString());
    }
}