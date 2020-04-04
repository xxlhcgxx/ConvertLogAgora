using System;
using System.IO;

namespace CandidateTesting.LuizHenriqueCorreaGoncalves.ConvertLogAgora
{
    public class StartConvert
    {
        static void Main(string[] args)
        {
            try
            {
                string type; string sourceUrl; string targetPath;

                bool validInf = false; bool validCreate = true;
                string program;

                RegisterLog.Log(String.Format($"{"Log created in "} : {DateTime.Now}"), "FileLog");
                RegisterLog.Log("Log file start");

                do
                {
                    Console.WriteLine("convert <sourceUrl> <targetPath>");
                    var inform = Console.ReadLine();
                    var informFields = inform.Split(' ');

                    program = informFields[0].ToString();
                    sourceUrl = informFields[1].ToString();
                    targetPath = informFields[2].ToString();

                    if (program == "" || sourceUrl == "" || targetPath == "") { validInf = false; } else { validInf = true; }

                } while (validInf == false);

                if (!Util.ValidateAction(program)) { validCreate = false; }
                if (!Util.ValidateSource(sourceUrl)) { validCreate = false; }
                if (!Util.ValidateTarget(targetPath)) { validCreate = false; }

                if (validCreate == false)
                {
                    RegisterLog.Log("Problems when validating data.");
                }
                else
                {
                    //u to url - f to file location
                    CreateFile(sourceUrl, targetPath, "u");
                }

                RegisterLog.Log("End of File.");
                Console.WriteLine("Locate log File..............:" + Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Log\\");
                Console.WriteLine("Locate convert File..........:" + Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Output\\");
                Console.ReadLine();
            }
            catch (Exception)
            {
                RegisterLog.Log("Problem creating the Log file.");
                Console.ReadLine();
            }
        }

        public static string CreateFile(string sourceUrl, string targetPath, string type)
        {
            try
            {
                var newFile = Convert.ManipulateLog(sourceUrl, targetPath, type);

                RegisterLog.Log("File create.");
                Console.WriteLine("");
                Console.WriteLine("File created......");
                Console.WriteLine("");

                return newFile;
            }
            catch (Exception)
            {
                RegisterLog.Log("Error creating file.");
                return "";
            }
        }

        public static void OpenCreatedFile(string file)
        {
            try
            {
                Convert.OpenFile(file);
            }
            catch (Exception)
            {
                RegisterLog.Log("Error opening file.");
            }
        }
    }
}
