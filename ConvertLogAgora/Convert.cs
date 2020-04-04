using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;

namespace CandidateTesting.LuizHenriqueCorreaGoncalves.ConvertLogAgora
{
    public class Convert
    {
        static readonly string cabecalhoLog = "#Version: 1.0" + Environment.NewLine
            + "#Date: " + DateTime.Now.ToString() + Environment.NewLine
            + "#Fields: provider http-method status-code uri-path time-taken response-size cache-status";
        const string provider = "\"MINHA CDN\"";

        public class CDN
        {
            public string HttpMethod { get; set; }
            public string StatusCode { get; set; }
            public string UriPath { get; set; }
            public string TimeTaken { get; set; }
            public string ResponseSize { get; set; }
            public string CacheStatus { get; set; }
        }

        public static string ManipulateLog(string sourceUrl, string targerPath, string type)
        {
            try
            {
                var listInfo = new List<CDN>(); var Log = "";

                if (type == "f") { listInfo = ConvertLogFIle(sourceUrl); }
                else if (type == "u")  { listInfo = ConvertLogUrl(sourceUrl); }
                Log = CreateLog(listInfo, targerPath);
                return Log;
            }
            catch (Exception)
            {
                RegisterLog.Log("Problem creating the file.");
                return "";
            }
        }

        private static List<CDN> ConvertLogFIle(string sourceUrl)
        {
            try
            {
                string line;
                var listaLog = new List<CDN>();

                StreamReader file = new StreamReader(sourceUrl);
                while ((line = file.ReadLine()) != null)
                {
                    listaLog.Add(NewLog(line));
                }
                file.Close();
                return listaLog;
            }
            catch (Exception)
            {
                RegisterLog.Log("Problem converting file.");
                return new List<CDN>();
            }
        }

        private static List<CDN> ConvertLogUrl(string sourceUrl)
        {
            try
            {
                var listaLog = new List<CDN>();
                var client = new RestClient(sourceUrl);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                if (response.Content != null)
                {
                    string[] stringSeparators = new string[] { "\r\n" };
                    string[] linesUrl = response.Content.Split(stringSeparators, StringSplitOptions.None);

                    foreach (var line in linesUrl)
                    {
                        if (line != "")
                        {
                            listaLog.Add(NewLog(line));
                        }
                    }
                }
                return listaLog;
            }
            catch (Exception)
            {
                RegisterLog.Log("Error converting URL.");
                return new List<CDN>();
            }
        }

        private static CDN NewLog(string line)
        {
            try
            {
                var fieldsGroup_1 = line.Split('|');
                var fieldsGroup_2 = fieldsGroup_1[3].Split(' ');
                var fieldsGroup_3 = fieldsGroup_1[4].Split('.');

                return new CDN
                {
                    ResponseSize = fieldsGroup_1[0].ToString(),
                    StatusCode = fieldsGroup_1[1].ToString(),
                    CacheStatus = fieldsGroup_1[2].ToString(),
                    HttpMethod = Util.RemoverAspas(fieldsGroup_2[0].ToString()),
                    UriPath = fieldsGroup_2[1].ToString(),
                    TimeTaken = fieldsGroup_3[0].ToString()
                };
            }
            catch (Exception)
            {
                RegisterLog.Log("Invalid file format.");
                return new CDN();
            }
        }

        private static string CreateLog(List<CDN> listInfo, string targerPath)
        {
            try
            {
                var line = "";

                StreamWriter newFile = File.CreateText(
                    Directory.GetParent(Environment.CurrentDirectory).Parent.FullName 
                    + targerPath.Replace("./", "/").Replace(" ", "").Replace("/", "\\"));

                newFile.WriteLine(cabecalhoLog);
                foreach (var item in listInfo)
                {
                    line = $"{provider}{item.HttpMethod} {item.StatusCode} {item.UriPath} " +
                        $"{item.TimeTaken} {item.ResponseSize} {item.CacheStatus}";

                    newFile.WriteLine(line);
                }
                newFile.Close();

                return targerPath;
            }
            catch (Exception)
            {
                RegisterLog.Log("Error creating file.");
                return "";
            }
        }

        public static void OpenFile(string targerPath)
        {
            try
            {
                System.Diagnostics.Process.Start(targerPath);
            }
            catch (Exception)
            {
                RegisterLog.Log("Error opening file.");
            }
        }
    }
}