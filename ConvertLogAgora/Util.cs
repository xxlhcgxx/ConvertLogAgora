using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.LuizHenriqueCorreaGoncalves.ConvertLogAgora
{
    public class Util
    {
        public static string RemoverAspas(string text)
        {
            try
            {
                return text.Replace("\"", " ");
            }
            catch (Exception ex)
            {
                return ex.InnerException.ToString();
            }
        }

        public static bool ValidateSource(string text)
        {
            try
            {
                if (!text.Contains("http")) { RegisterLog.Log("Invalid url."); return false; } else { return true; }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidateAction(string text)
        {
            try
            {
                if (text != "convert") { RegisterLog.Log("Action invalid."); return false; } else { return true; }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidateTarget(string text)
        {
            try
            {
                if (!text.ToUpper().Contains("./OUTPUT/")) { RegisterLog.Log("Targert Path invalid."); return false; } else { return true; }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
