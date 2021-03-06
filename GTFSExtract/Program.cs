﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTFSExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                System.Console.WriteLine("GTFSExtract source destination route");
                System.Console.WriteLine("source : source directory with unzip gtfs");
                System.Console.WriteLine("destination : destination directory");
                System.Console.WriteLine("route : list of routes to extract separated by a ;");
            }
            else
            {
                string gtfsInDirectoryPath = args[0];
                string gtfsOutDirectoryPath = args[1];
                string routeList = args[2];

                string [] routeIdToKeep = routeList.Split(';');

                Dictionary<string, bool> tripIdToKeep = new Dictionary<string, bool>();
                Dictionary<string, bool> stopIdToKeep = new Dictionary<string, bool>();
                Dictionary<string, bool> shapeIdToKeep = new Dictionary<string, bool>();

                // routes.txt
                using (StreamReader sr = new StreamReader(String.Format("{0}\\routes.txt", gtfsInDirectoryPath)))
                {
                    using (StreamWriter sw = new StreamWriter(String.Format("{0}\\routes.txt", gtfsOutDirectoryPath)))
                    {
                        string header = sr.ReadLine();
                        sw.WriteLine(header);
                        string[] headers = header.Split(',');
                        Dictionary<string, int> headersDic = new Dictionary<string, int>();

                        for (int i = 0; i < headers.Length; i++)
                        {
                            headersDic.Add(headers[i], i);
                        }

                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            string[] values = line.Split(',');
                            if (routeIdToKeep.Contains(values[headersDic["route_id"]]))
                            {
                                sw.WriteLine(line);
                            }
                        }
                    }
                }

                //trips.txt
                using (StreamReader sr = new StreamReader(String.Format("{0}\\trips.txt", gtfsInDirectoryPath)))
                {
                    using (StreamWriter sw = new StreamWriter(String.Format("{0}\\trips.txt", gtfsOutDirectoryPath)))
                    {
                        string header = sr.ReadLine();
                        sw.WriteLine(header);
                        string[] headers = header.Split(',');
                        Dictionary<string, int> headersDic = new Dictionary<string, int>();

                        for (int i = 0; i < headers.Length; i++)
                        {
                            headersDic.Add(headers[i], i);
                        }

                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            string[] values = line.Split(',');
                            if (routeIdToKeep.Contains(values[headersDic["route_id"]]))
                            {
                                tripIdToKeep[values[headersDic["trip_id"]]] = true;
                                shapeIdToKeep[values[headersDic["shape_id"]]] = true;
                                sw.WriteLine(line);
                            }
                        }
                    }
                }

                //stop_times.txt
                using (StreamReader sr = new StreamReader(String.Format("{0}\\stop_times.txt", gtfsInDirectoryPath)))
                {
                    using (StreamWriter sw = new StreamWriter(String.Format("{0}\\stop_times.txt", gtfsOutDirectoryPath)))
                    {
                        string header = sr.ReadLine();
                        sw.WriteLine(header);
                        string[] headers = header.Split(',');
                        Dictionary<string, int> headersDic = new Dictionary<string, int>();

                        for (int i = 0; i < headers.Length; i++)
                        {
                            headersDic.Add(headers[i], i);
                        }

                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            string[] values = line.Split(',');

                            if (tripIdToKeep.ContainsKey(values[headersDic["trip_id"]]))
                            {
                                stopIdToKeep[values[headersDic["stop_id"]]] = true;
                                sw.WriteLine(line);
                            }
                        }
                    }
                }

                //stops.txt
                using (StreamReader sr = new StreamReader(String.Format("{0}\\stops.txt", gtfsInDirectoryPath)))
                {
                    using (StreamWriter sw = new StreamWriter(String.Format("{0}\\stops.txt", gtfsOutDirectoryPath)))
                    {
                        string header = sr.ReadLine();
                        sw.WriteLine(header);
                        string[] headers = header.Split(',');
                        Dictionary<string, int> headersDic = new Dictionary<string, int>();

                        for (int i = 0; i < headers.Length; i++)
                        {
                            headersDic.Add(headers[i], i);
                        }

                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            string[] values = line.Split(',');

                            if (stopIdToKeep.ContainsKey(values[headersDic["stop_id"]]))
                            {
                                sw.WriteLine(line);
                            }
                        }
                    }
                }

                //shapes.txt
                using (StreamReader sr = new StreamReader(String.Format("{0}\\shapes.txt", gtfsInDirectoryPath)))
                {
                    using (StreamWriter sw = new StreamWriter(String.Format("{0}\\shapes.txt", gtfsOutDirectoryPath)))
                    {
                        string header = sr.ReadLine();
                        sw.WriteLine(header);
                        string[] headers = header.Split(',');
                        Dictionary<string, int> headersDic = new Dictionary<string, int>();

                        for (int i = 0; i < headers.Length; i++)
                        {
                            headersDic.Add(headers[i], i);
                        }

                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            string[] values = line.Split(',');

                            if (shapeIdToKeep.ContainsKey(values[headersDic["shape_id"]]))
                            {
                                sw.WriteLine(line);
                            }
                        }
                    }
                }
            }
        }
    }
}
