using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using AmazonWork.Utility;
using AventStack.ExtentReports;

namespace AmazonWork
{
    public class BaseTest : ExtentReportUtility
    {
        public static IWebDriver Driver;
        public TestContext TestContext { get; set; }
        public static SeleniumCommonActions CommonActions;

        public static void BrowserInitialization(TestContext testContext)
        {
            try
            {
                string driverPath = SeleniumCommonActions.GetFilePath(Constants.WebDriverPath);
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("no-sandbox");
                options.AddArguments("--start-maximized");
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);
                options.AddAdditionalCapability("useAutomationExtension", false);
                options.AddExcludedArgument("enable-automation");
                options.AddArgument("--disable-gpu");
                Driver = new ChromeDriver(driverPath, options, TimeSpan.FromMinutes(2));
                //Driver.Manage().Window.Maximize();
                Driver.Manage().Cookies.DeleteAllCookies();
                Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

                Driver.Url = Constants.AppUrl;
                //Driver.Url = testContext.Properties["webAppUrl"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void OneTimeSetUp(TestContext testContext)
        {
            BrowserInitialization(testContext);
            CommonActions = new SeleniumCommonActions(Driver);
            ExtentReportUtility.ClassInitialize(GetCurrentDirectoryName(), "JavaTPoint");
            Assertions.SetDriverInstance(Driver);

        }

        public static string GetCurrentDirectoryName()
        {
            return System.Reflection.Assembly.GetCallingAssembly().GetName().Name;
        }

        [TestInitialize]
        public void TestInitialize()
        {
                Test = report.CreateTest(TestContext.TestName);
                Test.Log(Status.Info, "Test Started");
                CommonActions.SetTest(Test);
                Driver.Url = Constants.AppUrl;
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Passed)
                Test.Log(Status.Pass, "Test case Passed");
            else if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
                Test.Log(Status.Fail, "Test case Failed");
            Test.Log(Status.Info, "Test End");
        }

        public static void KillBrowser()
        {
            SeleniumCommonActions.BrowserQuit();
        }

        public static void OneTimeTearDown()
        {
            KillBrowser();
            ClassClean();
        }
    }
}
