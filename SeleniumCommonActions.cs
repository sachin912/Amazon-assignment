using AmazonWork.Utility;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace AmazonWork
{
   public class SeleniumCommonActions
    { 
        public static IWebDriver WebDriver;
        WebDriverWait Wait;
        static ExtentTest Test;

        public SeleniumCommonActions(IWebDriver driver)
        {
            WebDriver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        public static SeleniumCommonActions GetInstance()
        {
            return new SeleniumCommonActions(WebDriver);
        }

        public void SetTest(ExtentTest test)
        {
            Test = test;
        }

        public ExtentTest GetTest()
        {
            return Test;
        }

        public IWebElement WaitForElementTillLocated(By by)
        {
            int retryCount = 0;
            try
            {
                return Wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (SystemException exc)
            {
                retryCount++;

                if (exc.Equals(typeof(StaleElementReferenceException)) || exc.Equals(typeof(NoSuchElementException)))
                {
                    if (retryCount > 2)
                    {
                        //Reports.GetTest().Info("Retrying the element " + by + retryCount + "time");
                        throw exc;
                    }

                    else
                    {
                        WaitForElementTillLocated(by);
                    }
                }
                // Reports.GetTest().Fail("Not found the element " + by);
                throw exc;
            }

        }
        public IReadOnlyCollection<IWebElement> WaitForElementsTillLocated(By by)
        {
            int retryCount = 0;
            try
            {
                return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            }
            catch (SystemException exc)
            {
                retryCount++;

                if (exc.Equals(typeof(StaleElementReferenceException)) || exc.Equals(typeof(NoSuchElementException)))
                {
                    if (retryCount > 2)
                    {
                        ExtentReportUtility.GetTest().Fail("Retrying the element " + by + retryCount + "time");
                        throw exc;
                    }

                    else
                    {
                        WaitForElementTillLocated(by);
                    }
                }
                throw exc;
            }

        }

        public void ClickElement(By by)
        {
            int retryCount = 2;
            var element = WaitForElementTillLocated(by);
            try
            {
                element.Click();
            }
            catch (StaleElementReferenceException exc)
            {
                retryCount++;
                if (exc.Equals(typeof(StaleElementReferenceException)))
                {
                    if (retryCount > 2)
                    {
                      // Reports.GetTest().Fail("Retrying the element " + by + retryCount + "time");
                        throw exc;
                    }

                    else
                    {
                        ClickElement(by);
                    }
                }
                // Reports.GetTest().Fail("Not found the element " + by);
                throw exc;
            }

        }
        public void ClearField(By by)
        {
            var element = WaitForElementTillLocated(by);
            var elementLength = element.Text.Length;
            for (int i = 0; i < elementLength; i++)
            {
                WebDriver.FindElement((By)element).SendKeys(Keys.Backspace);
            }
        }

        public void CLearAndTyeKeys(By by, string input)
        {
            try
            {
                var element = WaitForElementTillLocated(by);
                element.Clear();
                WaitForSecs(2);
                element.SendKeys(input);
                Assert.IsTrue(GetValue(by).Contains(input));
                Test.Pass("Field " + by + " has been set " + " with text: " + input);
            }
            catch (WebDriverException exc)
            {
                WaitForSecs(1);
                var mediaEntityModelProvider = ExtentReportUtility.TakeScreenShotAndSave(WebDriver);
                Test.Log(Status.Fail, "Not able to set the field " + by + "\n" + exc.Message, mediaEntityModelProvider);
                throw exc;
            }
        }

        public IWebElement WaitForElementTillExistenceFound(By by)
        {
            try
            {
                return Wait.Until(ExpectedConditions.ElementExists(by));
            }
            catch (SystemException exc)
            {
                //Reports.GetTest().Fail("Not found the element " + by);
                throw exc;
            }

        }
        public void SelectOptionByText(By by, string option)
        {
            var element = WaitForElementTillExistenceFound(by);
            SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByText(option);
        }

        /**
         * Getters
         */
        public IWebElement GetElement(By by)
        {
            var element = WaitForElementTillLocated(by);
            return element;
        }

        public string GetValue(By by)
        {
            var element = WaitForElementTillExistenceFound(by);
            return element.GetAttribute("value");
        }

        public string GetText(By by)
        {
            WaitForPageLoad();
            var element = WaitForElementTillExistenceFound(by);
            return element.Text;
        }

        public string GetCurrentUrl()
        {
            return WebDriver.Url;
        }

        public string GetCurrentWindowHandle()
        {
            return WebDriver.CurrentWindowHandle;
        }

        public void WaitForSecs(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public void maximiseBrowser()
        {
            WebDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Waits for a certain element for a specified amount of time
        /// </summary>
        /// <param name="aByElement"></param>
        /// <param name="aWaitTimeInMS"></param>
        public void explicitWait(By aByElement, long aWaitTimeInMS = 5000)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromMilliseconds(aWaitTimeInMS));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(aByElement));
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void waitForEle()
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(WebDriver);
            fluentWait.Timeout = TimeSpan.FromSeconds(10);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }



        public void SleepDefaultWait(int aWaitTimeInSec)
        {
            Thread.Sleep(aWaitTimeInSec);
        }

        /// <summary>
        /// Navigates back on the browser
        /// </summary>
        public void browserBackNavigation()
        {
            WebDriver.Navigate().Back();
        }

        /// <summary>
        /// Navigates forward on the browser
        /// </summary>
        public void browserForwardNavigation()
        {
            WebDriver.Navigate().Forward();
        }

        /// <summary>
        /// Gets attribute of a certain element
        /// </summary>
        /// <param name="aByValue"></param>
        /// <param name="aAttribute"></param>
        /// <param name="aWaitTimeInMS"></param>
        /// <returns></returns>
        public String getAttributeOfWebelementByLocator(By aByValue, String aAttribute, int aWaitTimeInMS = 3000)
        {
            IWebElement element = WebDriver.FindElement(aByValue);
            return element.GetAttribute(aAttribute);
        }

        /// <summary>
        /// Finds element by specified locator
        /// </summary>
        /// <param name="aBy"></param>
        /// <returns></returns>
        public IWebElement getWebElementByLocator(By aBy)
        {
            IWebElement webElement = WebDriver.FindElement(aBy);
            return webElement;
        }
        
        public ReadOnlyCollection<IWebElement> getWebElementsByLocator(By aBy)
        {
            ReadOnlyCollection<IWebElement> webElements = WebDriver.FindElements(aBy);
            return webElements;
        }
        
        /// <summary>
        /// Gets the text of the web element
        /// </summary>
        /// <param name="aWebElementID"></param>
        /// <returns></returns>
        public string getTextOfWebElementByLocator(By aWebElementID)
        {
            WaitForPageLoad();

            return getWebElementByLocator(aWebElementID).Text;
        }

        /// <summary>
        /// Click on the element
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="scenarioName"></param>
        /// <returns></returns>
        public void ClickOnElementWhenElementFound(IWebElement webElement)
        {
            try
            {
                webElement.Click();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Double Click on the element
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="scenarioName"></param>
        /// <returns></returns>
        public void DoubleClickOnElementWhenElementFound(IWebElement webElement, string scenarioName)
        {
            try
            {
                Actions actions = new Actions(WebDriver);
                actions.DoubleClick(webElement).Perform();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, scenarioName);
            }
        }


        /// <summary>
        /// Clears the data and enters data into the web element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="aTestData"></param>
        /// <param name="scenarioName"></param>
        /// <returns></returns>
        public void SendKeysForWebElement(IWebElement element, String aTestData, string scenarioName)
        {
            try
            {
                element.Clear();
                element.SendKeys(aTestData);
            }
            catch (ElementNotVisibleException e)
            {
                Console.WriteLine(e.Message, scenarioName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, scenarioName);
            }
        }


        /// <summary>
        /// Desc:Check element is visible or not
        /// </summary>
        /// <param name="webElement"></param>
        /// <returns></returns>
        public bool IsElementVisible(IWebElement webElement)
        {
            bool isDisplayed = false;
            try
            {
                isDisplayed = webElement.Displayed;
                return isDisplayed;
            }
            catch (Exception)
            {
                return isDisplayed;
            }
        }


            public IWebElement getWebElement(By element)
        {
            return WebDriver.FindElement(element);
        }

        public bool waitUntilElementNotVisible(IWebElement aByeValue, int timeOut)
        {
            DateTime dtEnd = DateTime.UtcNow.AddMilliseconds(timeOut);
            while (DateTime.UtcNow < dtEnd)
            {
                if (!(IsElementVisible(aByeValue)))
                    return true;
                Thread.Sleep(100);
            }
            return false;
        }

        /// <summary>
        /// Selects the value from dropdown by text
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public String selectValueFromDropdown(IWebElement webElement, string value)
        {
            try
            {
                SelectElement oSelect = new SelectElement(webElement);
                oSelect.SelectByText(value);
                return value;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }


        public string selectValueFromDropdownByText(By aByValue, string value)
        {
            try
            {
                SelectElement oSelect = new SelectElement(WebDriver.FindElement(aByValue));
                oSelect.SelectByText(value);
                return value;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        /// <summary>
        /// Selects the value from dropdown by index
        /// </summary>
        /// <param name="aByValue"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int selectValueByIndex(By aByValue, int index)
        {
            try
            {
                SelectElement oSelect = new SelectElement(WebDriver.FindElement(aByValue));
                oSelect.SelectByIndex(index);
                return index;
            }
            catch (NoSuchElementException)
            {
                return -1;
            }
        }


        /// <summary>
        /// Desc:Method is used to capture the screenShots
        /// </summary>
        /// <returns></returns>
        public string ScreenShotCapture()
        {
            try
            {
                string screenshotName = DateTime.Now.ToString().Replace("/", "_").Replace("-", "_").Replace(":", "_").Replace(" ", "_") + ".jpeg";
                string screenshotPath = GetScreenshotPath() + screenshotName;
                ITakesScreenshot screenshotDriver = WebDriver as ITakesScreenshot;
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Jpeg);
                return screenshotPath;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Desc:Method is used to get screenshot's path
        /// </summary>
        /// <returns></returns>
        public static string GetScreenshotPath()
        {
            string screenshotPath = FileUtility.GetSourceDirectoryPath();

            return screenshotPath + @"PathNAme";

        }

        /// <summary>
        /// Desc:Method is used to get all the elmenets
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public IList<IWebElement> getSelectedElements(string xPath)
        {
            IList<IWebElement> selects = WebDriver.FindElements(By.XPath(xPath));
            return selects;
        }

        public static string GetFilePath(string filePath)
        {
            string driverPath = FileUtility.GetSourceDirectoryPath();
            driverPath = driverPath + filePath;
            return driverPath;
        }

        public static void BrowserQuit()
        {
            WebDriver.Quit();
        }



        /// <summary>
        /// Checks whether the list of elements are present or not
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public bool areElementsPresent(string locator)
        {
            IList<IWebElement> list = WebDriver.FindElements(By.XPath(locator));
            if (list.Count == 0)
                return false;
            else
                return true;
        }


        public void ClearTextBox(IWebElement element)
        {
            element.Clear();
        }

        public void CloseTheCurrentTab()
        {
            WebDriver.Close();
        }

        public IList<IWebElement> ElementsPresent(string locator)
        {
            IList<IWebElement> list = WebDriver.FindElements(By.CssSelector(locator));
            return list;
        }

        public void HoverOverUsingElement(IWebElement element)
        {
            Actions actions = new Actions(WebDriver);
            actions.MoveToElement(element).Perform();
            WaitForSecs(2);
        }

        public void HoverElementAndClickSubElement(IWebElement parentElement, By childElement)
        {
            Actions actions = new Actions(WebDriver);
            actions.MoveToElement(parentElement).Click(WebDriver.FindElement(childElement)).Build().Perform();
        }

        public void NavigateToPreviousPage()
        {
            WebDriver.Navigate().Back();
        }

        public void WaitForPageLoad()
        {
            Wait.Until(driver1 => ((IJavaScriptExecutor)WebDriver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public bool IsCheckboxChecked(By by)
        {
            var element = WaitForElementTillExistenceFound(by);
            return element.Selected;
        }
        public bool IsDisplayed(By by)
        {
            var element = WaitForElementTillExistenceFound(by);
            //return element.Displayed;

            if (element.Displayed == true)
            {
                //Test.Pass(by + " is displayed ");
                return true;
            }
            else
            {
                //Test.Log(Status.Fail, by + "not displayed");
                return false;
            }
            }
        }
    }


