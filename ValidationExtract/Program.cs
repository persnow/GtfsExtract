using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            string validationInFilePath = @"C:\Users\x666333\Desktop\4_VAL_00_20180112.csv";
            string validationOutFilePath = @"C:\Users\x666333\Desktop\4_VAL_01_20180112.csv";

            string routeIdToKeep = "42800"; // N2800
            int routeColumn = 7;

            // routes.txt
            using (StreamReader sr = new StreamReader(validationInFilePath))
            {
                using (StreamWriter sw = new StreamWriter(validationOutFilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] values = line.Split(',');
                        if (values[routeColumn].Contains(routeIdToKeep))
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}
