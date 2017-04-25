using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO.Compression;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace fileDownloader
{
    class Downloader
    {
        static void Main(string[] args)
        {
            //create path strings
            string currentDir = Directory.GetCurrentDirectory();
            string fullDataPath = System.IO.Path.Combine(currentDir, "Monthly Full Data");
            string weeklyUpdatePath = System.IO.Path.Combine(currentDir, "Weekly Update");
            string monthlyDeactivatePath = System.IO.Path.Combine(currentDir, "Monthly Deactivate");

            Console.WriteLine("Looking for new files...");
            //check for monthly full Data set directory then download
            if (Directory.Exists(fullDataPath))
                downLoadFiles(fullDataPath, "Monthly Full Data");
            else
            {
                System.IO.Directory.CreateDirectory(fullDataPath);
                downLoadFiles(fullDataPath, "Monthly Full Data");
            }

            //check for weekly update directory then download
            if (Directory.Exists(weeklyUpdatePath))
                downLoadFiles(weeklyUpdatePath, "Weekly Update");
            else
            {
                System.IO.Directory.CreateDirectory(weeklyUpdatePath);
                downLoadFiles(weeklyUpdatePath, "Weekly Update");
            }

            //check for monthly Deactivate directory then download
            if (Directory.Exists(monthlyDeactivatePath))
                downLoadFiles(monthlyDeactivatePath, "Monthly Deactivate");
            else
            {
                System.IO.Directory.CreateDirectory(monthlyDeactivatePath);
                downLoadFiles(monthlyDeactivatePath, "Monthly Deactivate");
            }
            Console.WriteLine("Done!");

            //print to the console
            using (StreamReader r = File.OpenText("log.txt"))
            {
                DumpLog(r);
            }
        

    }
        //check npes download site for new downloads.Then download and extract.
        public static void downLoadFiles(string path, string type)
        {
            string curMonth = DateTime.Now.ToString("MM");
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load("http://download.cms.gov/nppes/NPI_Files.html");
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                if (!hrefValue.Contains(".html"))
                {
                    string dateString = Regex.Match(hrefValue, @"\d+_?\d+").Value;
                    string downloadType = fileType(dateString);
                    if (downloadType.Equals(type))
                    {
                        if (dateString.Length == 4)
                            dateString = curMonth + dateString;
                        string directoryPath = path + "\\" + dateString;
                        if (!Directory.Exists(directoryPath))
                        {
                            WebClient Client = new WebClient();
                            Console.WriteLine("Downloading " + downloadType + " " + dateString + "...");

                            //printing to the log file 
                            using (StreamWriter w = File.AppendText("log.txt"))
                            {
                                Log("Downloaded " + downloadType + " " + dateString, w);
                            }

                            System.IO.Directory.CreateDirectory(directoryPath);
                            string downloadLink = "http://download.cms.gov/nppes" + (hrefValue.Remove(0, 1));
                            string zipName = dateString + ".zip";
                            string savePath = path + "\\" + dateString + '\\' + zipName;
                            Client.DownloadFile(downloadLink, savePath);
                            ZipFile.ExtractToDirectory(savePath, directoryPath);
                            Console.WriteLine("Extracting...");
                        }
                    }
                }
            }
        }

        //helper funtion for matching download file type
        public static string fileType(string fileName)
        {
            if (fileName.Length == 4)
                return "Monthly Full Data";
            else
                if (fileName.Length == 6)
                return "Monthly Deactivate";
            else
                return "Weekly Update";
        }

        //for printing to a log file
        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.Write("{0} {1} =>", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :{0}", logMessage);
        }

        //print to the console
        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

    }



    }      