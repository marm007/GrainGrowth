using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Config;

public class Recrystallization
{
    private const double maxTime = 0.2;
    private const double A = 86710969050178.5;
    private const double B = 9.41268203527779;

    private const double A0 = 0.000000000257;
    private const double B0 = 80000000000;

    private const double deltaTime = 0.001;
    private const double sigma0 = 0;
    private const double value = 1.9;

    private double time = 0;

    private List<double> ro = null;
    private List<double> sigma = null;

    public Recrystallization()
    {
        ro = new List<double>();
        sigma = new List<double>();

        double _ro = (A / B) + (1 - (A / B)) * (Math.Exp(-B * time));
        double _sigma = sigma0 + value * A0 * B0 * Math.Sqrt(_ro);

        ro.Add(_ro);
        sigma.Add(_sigma);
    }

    public void DislocationsPartition()
    {
        for(double i = 0; i <= maxTime; i+= deltaTime)
        {

            time += deltaTime;

            double _ro = (A / B) + (1 - (A / B)) * (Math.Exp(-B * time));
            double _sigma = sigma0 + value * A0 * B0 * Math.Sqrt(_ro);
          
            ro.Add(_ro);
            sigma.Add(_sigma);
        }
        string strFilePath = @"C:\Users\Marcin\Desktop\xd1.csv";
        string strSeperator = ";";
        StringBuilder sbOutput = new StringBuilder();

        String[] toWrite = new String[3];
        double _time = 0;

        for (int i = 0; i < ro.Count; i++)
        {
            toWrite[0] = _time.ToString();

            toWrite[1] = ro.ElementAt(i).ToString();
            toWrite[2] = sigma.ElementAt(i).ToString();
            _time += deltaTime;
            sbOutput.AppendLine(string.Join(strSeperator, toWrite));
        }

        // Create and write the csv file
        File.WriteAllText(strFilePath, sbOutput.ToString());

    }
}
