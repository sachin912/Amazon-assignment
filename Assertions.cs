using AmazonWork.Utility;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace AmazonWork
{
   public class Assertions
    {
        static IWebDriver Driver;

        public static void SetDriverInstance(IWebDriver driver)
        {
            Driver = driver;
        }
        /*
         * Method verifies the flag value is true or not
         * @exception: throws custom exception when the flag value is false
         */

        public static void IsTrue(bool flag, string message, string stepNumber = null, string defectId = null)
        {
            try
            {
                Assert.IsTrue(flag);
                ExtentReportUtility.GetTest().Pass(String.Format("Verification: '{0}' " + "is done", message));
            }
            catch (Exception exc)
            {
                string failureMessage = null;
                failureMessage = String.Format("Verification: '{0}' " + "is failed", message);
                var mediaEntityModelProvider = ExtentReportUtility.TakeScreenShotAndSave(Driver);
                ExtentReportUtility.GetTest().Log(Status.Fail, failureMessage, mediaEntityModelProvider);
                throw exc;
            }
        }

        /*
         * Method verifies the flag value is false
         * @exception: throws custom exception when the flag value is true
         */
        public static void IsFalse(bool flag, string message, string stepNumber = null, string defectId = null)
        {
            try
            {
                Assert.IsFalse(flag);
                ExtentReportUtility.GetTest().Pass(String.Format("Verification: '{0}' " + "is done", message));
            }
            catch (Exception exc)
            {
                string failureMessage = null;
                failureMessage = String.Format("Verification: '{0}' " + "is failed", message);
                var mediaEntityModelProvider = ExtentReportUtility.TakeScreenShotAndSave(Driver);
                ExtentReportUtility.GetTest().Log(Status.Fail, failureMessage, mediaEntityModelProvider);
                throw exc;
            }
        }

        /*
         * Method to verify both object's value are same
         * @param expectedValue: Value which should be in the app
         * @param actualValue: Value which is in the app at runtime
         * @param actualValue: Message which should get print on success
         * @exception: throws exception if values of both objects are not matching
         */
        public static void AreEqual(Object expectedValue, Object actualValue, string message, string stepNumber = null, string defectId = null)
        {
            try
            {
                Assert.AreEqual(expectedValue, actualValue);
                ExtentReportUtility.GetTest().Pass(String.Format("Verification: '{0}' " + "is done", message));
            }
            catch (Exception exc)
            {
                string failureMessage = null;
                failureMessage = String.Format("Verification: '{0}' " + "is failed - " + "expectedValue is " + expectedValue + " and actual is " + actualValue, message);
                var mediaEntityModelProvider = ExtentReportUtility.TakeScreenShotAndSave(Driver);
                ExtentReportUtility.GetTest().Log(Status.Fail, failureMessage, mediaEntityModelProvider);

                throw exc;
            }
        }

        /*
         * Method fails the test by logging the reason
         * @param: message to get log while failing the test
         * 
         */
        public static void Fail(string message, string stepNumber = null, string defectId = null)
        {
            Assert.Fail();
            ExtentReportUtility.GetTest().Fail(message);
        }

        /*
         * Method to verify both object's value are not same
         * @param expectedValue: Value which should not be in the app
         * @param actualValue: Value which is in the app at runtime
         * @param actualValue: Message which should get print on success
         * @exception: throws exception if values of both objects are not matching
         */

        public static void AreNotEqual(Object expectedValue, Object actualValue, string message, string stepNumber = null, string defectId = null)
        {
            try
            {
                Assert.AreNotEqual(expectedValue, actualValue);
                ExtentReportUtility.GetTest().Pass(String.Format("Verification: '{0}' " + "is done", message));
            }
            catch (Exception exc)
            {
                string failureMessage = null;
                failureMessage = String.Format("Verification: '{0}' " + "is failed", message);
                var mediaEntityModelProvider = ExtentReportUtility.TakeScreenShotAndSave(Driver);
                ExtentReportUtility.GetTest().Log(Status.Fail, failureMessage, mediaEntityModelProvider);

                throw exc;
            }
        }
    }
}

