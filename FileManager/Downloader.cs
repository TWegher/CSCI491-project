using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO.Compression;
using System.IO;


namespace fileDownloader
{
    class Downloader
    {
        static void Main(string[] args)
        {
            //create path strings
            string currentDir = Directory.GetCurrentDirectory();
            string fullDataPath = System.IO.Path.Combine(currentDir, "Full Data"); 
            string weeklyUpdatePath = System.IO.Path.Combine(currentDir, "Weekly Update"); 
            string monthlyDeactivatePath = System.IO.Path.Combine(currentDir, "Monthly Deactivate");

            //check for monthly full Data set directory then download
            if (Directory.Exists(fullDataPath))
                downloadFullData(fullDataPath);
            else
            {
                System.IO.Directory.CreateDirectory(fullDataPath);
                downloadFullData(fullDataPath);
            }

            //check for weekly update directory then download
            if (Directory.Exists(weeklyUpdatePath))
                downloadWeeklyUpdate(weeklyUpdatePath);
            else
            {
                System.IO.Directory.CreateDirectory(weeklyUpdatePath);
                downloadWeeklyUpdate(weeklyUpdatePath);
            }

            //check for monthly Deactivate directory then download
            if (Directory.Exists(monthlyDeactivatePath))
                downloadMonthlyDeactivate(monthlyDeactivatePath);
            else
            {
                System.IO.Directory.CreateDirectory(monthlyDeactivatePath);
                downloadMonthlyDeactivate(monthlyDeactivatePath);
            }


        }

        //download and unzip weekly update file
        public static void downloadWeeklyUpdate(string path)
        {
            WebClient Client = new WebClient();
            string dateSting = "041017_041617";
            string pathString = System.IO.Path.Combine(path, dateSting);
            if (Directory.Exists(pathString))
                Console.WriteLine("Most Recent Weekly Update File Download Already Exists.");
            else
            {
                System.IO.Directory.CreateDirectory(pathString);
                Client.DownloadFile("http://download.cms.gov/nppes/NPPES_Data_Dissemination_041017_041617_Weekly.zip", @"C:\Users\tim.wegher\Documents\Visual Studio 2015\Projects\fileDownloader\fileDownloader\bin\Debug\Weekly Update\041017_041617\041017_041617.zip");
                string zipPath = System.IO.Path.Combine(pathString, "041017_041617.zip");
                ZipFile.ExtractToDirectory(zipPath, pathString);
            }
        }

        //download and unzip monthly deactivate file
        public static void downloadMonthlyDeactivate(string path)
        {
            WebClient Client = new WebClient();
            string dateSting = "041117";
            string pathString = System.IO.Path.Combine(path, dateSting);
            if (Directory.Exists(pathString))
                Console.WriteLine("Most Recent Mothly Deactivate File Download Already Exists.");
            else
            {
                System.IO.Directory.CreateDirectory(pathString);
                Client.DownloadFile("http://download.cms.gov/nppes/NPPES_Deactivated_NPI_Report_041117.zip", @"C:\Users\tim.wegher\Documents\Visual Studio 2015\Projects\fileDownloader\fileDownloader\bin\Debug\Monthly Deactivate\041117\041117.zip");
                string zipPath = System.IO.Path.Combine(pathString, "041117.zip");
                ZipFile.ExtractToDirectory(zipPath, pathString);
            }
        }

        //download and unzip full data file
        public static void downloadFullData(string path)
        {
            WebClient Client = new WebClient();
            string dateSting = "April_2017";
            string pathString = System.IO.Path.Combine(path, dateSting);
            if (Directory.Exists(pathString))
                Console.WriteLine("Most Recent Mothly Full Data File Download Already Exists.");
            else
            {
                System.IO.Directory.CreateDirectory(pathString);
                Client.DownloadFile("http://download.cms.gov/nppes/NPPES_Data_Dissemination_April_2017.zip", @"C:\Users\tim.wegher\Documents\Visual Studio 2015\Projects\fileDownloader\fileDownloader\bin\Debug\Full Data\April_2017\April_2017.zip");
                string zipPath = System.IO.Path.Combine(pathString, "April_2017.zip");
                ZipFile.ExtractToDirectory(zipPath, pathString);
            }
        }
    }
}
 