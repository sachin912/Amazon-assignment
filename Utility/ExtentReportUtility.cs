using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using System;
using System.IO;

namespace AmazonWork.Utility
{
   public class ExtentReportUtility
    {
        protected static ExtentReports report;
        protected static ExtentTest Test;
        public static ExtentReports extent;

        public static ExtentV3HtmlReporter htmlreporter;
        public static string TestResultsPath = null;

        public static ExtentReports ClassInitialize(string documentTitle, string appName)
        {
            if (report == null)
                report = StartReport(documentTitle, appName);

            return report;
        }

        public static void ClassClean()
        {
            report.Flush();
        }

        public static ExtentTest GetTest()
        {
            return Test;
        }

        public static void SetTest(ExtentTest test)
        {
            Test = test;
        }

        public static ExtentReports StartReport(string documentTitle, string appName)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            TestResultsPath = path + "\\" + documentTitle + "\\TestResults\\";
            String reportName = "ExtentReport_" + DateTime.Now.ToString("MMddyyyyhmm") + ".html";
            htmlreporter = new ExtentV3HtmlReporter(TestResultsPath + "\\" + reportName);
            htmlreporter.Config.ReportName = documentTitle;
            htmlreporter.Config.DocumentTitle = "JavaTPoint Report";
            htmlreporter.Config.Theme = Theme.Standard;
            extent = new ExtentReports();  //responsible to drive all your execution 
            extent.AttachReporter(htmlreporter);  //attach report you'va created to main class
            extent.AddSystemInfo("Operating System", Environment.OSVersion.VersionString);
            extent.AddSystemInfo("Application Name", appName);

            return extent;
        }

        public static MediaEntityModelProvider TakeScreenShotAndSave(IWebDriver driver)
        {

            Screenshot _Screenshot;

            string screenshotFolderPath = TestResultsPath + "Screenshot\\";
            if (!Directory.Exists(screenshotFolderPath))
                Directory.CreateDirectory(screenshotFolderPath);
            string screenshotPath = null;
            String screenshotName = "Screenshot_" + DateTime.Now.ToString("MMddyyyyhmm") + ".png";
            screenshotPath = screenshotFolderPath + screenshotName;
            _Screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            _Screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
            var mediaEntityModelProvider = MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build();
            return mediaEntityModelProvider;
        }
    }
}

