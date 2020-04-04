using System;
using System.IO;

namespace CandidateTesting.LuizHenriqueCorreaGoncalves.ConvertLogAgora
{
    public class RegisterLog
    {
        private static string pathExe = string.Empty;

        public static bool Log(string strMsg, string strFileName = "FileLog")
        {
            try
            {
                pathExe = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Log\\";
                string pathFile = Path.Combine(pathExe, strFileName);
                if (!File.Exists(pathFile))
                {
                    FileStream arquivo = File.Create(pathFile);
                    arquivo.Close();
                }
                using (StreamWriter w = File.AppendText(pathFile))
                {
                    AppendLog(strMsg, w);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void AppendLog(string logMsg, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine($"{logMsg}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}