using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.IO.Compression;
using HtmlAgilityPack;
using System.Text.RegularExpressions;


class Downloader
{
    string currentDir;

    public Downloader()
    {
        currentDir = Directory.GetCurrentDirectory();
    }

    public List<string> checkPath(FileType type)
    {
        string path = System.IO.Path.Combine(currentDir, type.ToString());

        Console.WriteLine("Looking for new files...");
        if (!Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        List<string> filePaths = downloadFile(path, type);

        Console.WriteLine("Done!");

        //print to the console
        using (StreamReader r = File.OpenText("log.txt"))
        {
            DumpLog(r);
        }

        return filePaths;
    }

    //check nppes download site for new downloads. Then download and extract.
    private List<string> downloadFile(string path, FileType type)
    {
        string curMonth = DateTime.Now.ToString("MM");
        HtmlWeb hw = new HtmlWeb();
        HtmlDocument doc = hw.Load("http://download.cms.gov/nppes/NPI_Files.html");
        List<string> downloadedFilePaths = new List<string>();
        foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
        {
            string hrefValue = link.GetAttributeValue("href", string.Empty);
            if (!hrefValue.Contains(".html"))
            {
                string dateString = Regex.Match(hrefValue, @"\d+_?\d+").Value;
                FileType downloadType = getFileType(dateString);
                if (downloadType.Equals(type))
                {
                    if (type.Equals(FileType.Full))
                        dateString = $"{curMonth}_{dateString}";
                    string directoryPath = path + "\\" + dateString;
                    
                    //If the selected file has not yet been downloaded, allocate disk location and download the file
                    if (!Directory.Exists(directoryPath))
                    {
                        WebClient Client = new WebClient();
                        Console.WriteLine("Downloading " + downloadType + " " + dateString + " ...");

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

                        string[] extractedFiles = Directory.GetFiles(directoryPath, "*");

                        downloadedFilePaths.Add(extractedFiles[0]);

                        Console.WriteLine("Extracting...");
                    }
                }
            }
        }
        //Returns the path to all of the downloaded files
        return downloadedFilePaths;
    }

    //helper funtion for matching download file type
    private FileType getFileType(string fileName)
    {
        if (fileName.Length == 4)
        {
            return FileType.Full;
        }
        else if (fileName.Length == 6)
        {
            return FileType.Deactivate;
        }
        else
        {
            return FileType.Update;
        }
    }

    //for printing to a log file
    private void Log(string logMessage, TextWriter w)
    {
        w.Write("\r\nLog Entry : ");
        w.Write("{0} {1} =>", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToLongDateString());
        w.WriteLine("  :{0}", logMessage);
    }

    //prints log to the console
    private void DumpLog(StreamReader r)
    {
        string line;
        while ((line = r.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }
}


public enum FileType
{
    Full,
    Update,
    Deactivate
}