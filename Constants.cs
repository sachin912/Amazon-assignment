using System;
using System.Collections.Generic;
using System.Configuration;

namespace AmazonWork
{
  public static class Constants
    {
        public static string WebDriverPath = ConfigurationManager.AppSettings["WebdriverPath"];
        public static string AppUrl = ConfigurationManager.AppSettings["Url"];
        public static string AppName = "JavaTPoint: Web Application";
        public static string searchValue = "Java";
        public static string feedback = "Feedback";
        public static string mailFeedback = "Send your feedback to feedback@javatpoint.com";
        public static string updates = "100+ Latest Updates";

    }
}
