using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CandidateTesting.LuizHenriqueCorreaGoncalves.ConvertLogAgora;

namespace CandidateTesting.LuizHenriqueCorreaGoncalves.TestConvertLog
{
    [TestClass]
    public class UnitTest
    {
        string file;

        [TestMethod]
        /* Test with correct data. */
        public void TestOk()
        {
            RegisterLog.Log(String.Format($"{"Log Test created in "} : {DateTime.Now}"), "FileLog");
            RegisterLog.Log("Log Test file start. Test 1");

            var program = "convert";
            var sourceUrl = @"https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
            var targetPath = "./Output/minhaCdn1.txt";

            if (Util.ValidateAction(program) && Util.ValidateSource(sourceUrl) && Util.ValidateTarget(targetPath))
            {
                file = StartConvert.CreateFile(
                sourceUrl,
                targetPath,
                "u");
            } else { RegisterLog.Log("Problems when validating data."); }

            RegisterLog.Log("Log Test file end.");
        }

        [TestMethod]
        /* Test with url without log information. */
        public void TestURLInformationInvalid()
        {
            RegisterLog.Log(String.Format($"{"Log Test created in "} : {DateTime.Now}"), "FileLog");
            RegisterLog.Log("Log Test file start. Test 2");

            var program = "convert";
            var sourceUrl = @"https://s3.amazonaws.com/uux-itaas-static/";
            var targetPath = "./Output/minhaCdn2.txt";

            if (Util.ValidateAction(program) && Util.ValidateSource(sourceUrl) && Util.ValidateTarget(targetPath))
            {
                file = StartConvert.CreateFile(
                sourceUrl,
                targetPath,
                "u");
            }
            else { RegisterLog.Log("Problems when validating data."); }

            RegisterLog.Log("Log Test file end.");
        }

        [TestMethod]
        /* Test with url with physical path. */
        public void TestURLInvalid()
        {
            RegisterLog.Log(String.Format($"{"Log Test created in "} : {DateTime.Now}"), "FileLog");
            RegisterLog.Log("Log Test file start. Test 3");

            var program = "convert";
            var sourceUrl = "C:\\Teste\\Arquivo.txt";
            var targetPath = "./Output/minhaCdn3.txt";

            if (Util.ValidateAction(program) && Util.ValidateSource(sourceUrl) && Util.ValidateTarget(targetPath))
            {
                file = StartConvert.CreateFile(
                sourceUrl,
                targetPath,
                "u");
            }
            else { RegisterLog.Log("Problems when validating data."); }

            RegisterLog.Log("Log Test file end.");
        }

        [TestMethod]
        /* Test with invalid action. */
        public void TestProgramInvalid()
        {
            RegisterLog.Log(String.Format($"{"Log Test created in "} : {DateTime.Now}"), "FileLog");
            RegisterLog.Log("Log Test file start. Test 4");

            var program = "replace";
            var sourceUrl = @"https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
            var targetPath = "./Output/minhaCdn3.txt";

            if (Util.ValidateAction(program) && Util.ValidateSource(sourceUrl) && Util.ValidateTarget(targetPath))
            {
                file = StartConvert.CreateFile(
                sourceUrl,
                targetPath,
                "u");
            }
            else { RegisterLog.Log("Problems when validating data."); }

            RegisterLog.Log("Log Test file end.");
        }

        [TestMethod]
        /* Test with invalid destination. */
        public void TestTargetInvalid()
        {
            RegisterLog.Log(String.Format($"{"Log Test created in "} : {DateTime.Now}"), "FileLog");
            RegisterLog.Log("Log Test file start. Test 5");

            var program = "convert";
            var sourceUrl = @"https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
            var targetPath = "C:\\Test\\File.txt";

            if (Util.ValidateAction(program) && Util.ValidateSource(sourceUrl) && Util.ValidateTarget(targetPath))
            {
                file = StartConvert.CreateFile(
                sourceUrl,
                targetPath,
                "u");
            }
            else { RegisterLog.Log("Problems when validating data."); }

            RegisterLog.Log("Log Test file end.");
        }

        [TestMethod]
        /* Test with invalid url, invalid destination and invalid action. */
        public void TestOff()
        {
            RegisterLog.Log(String.Format($"{"Log Test created in "} : {DateTime.Now}"), "FileLog");
            RegisterLog.Log("Log Test file start. Test 6");

            var program = "replace";
            var sourceUrl = "C:\\Teste\\MinhaCdnl1.txt";
            var targetPath = "C:\\Test\\File.txt";

            if (Util.ValidateAction(program) && Util.ValidateSource(sourceUrl) && Util.ValidateTarget(targetPath))
            {
                file = StartConvert.CreateFile(
                sourceUrl,
                targetPath,
                "u");
            }
            else { RegisterLog.Log("Problems when validating data."); }

            RegisterLog.Log("Log Test file end.");
        }
    }
}
