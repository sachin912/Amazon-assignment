using AmazonWork.Locators;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Linq;

namespace AmazonWork.Modules
{
   public class Home_Page : Common_Page
    {
        

        public Home_Page() : base()
        {
        }
        public void VerifyContentCanBeSearched(string valueSearch)
        {
            _SeleniumCommonActions.WaitForPageLoad();
            By searchInputBox = By.CssSelector(HomePage_Locators.inputBoxByCss);
            _SeleniumCommonActions.CLearAndTyeKeys(searchInputBox, valueSearch);
            Assertions.IsTrue(_SeleniumCommonActions.GetValue(searchInputBox).Contains(valueSearch), "it contains"+ valueSearch + "in search field" );
            By searchButton = By.CssSelector(HomePage_Locators.searchBoxByCss);
            _SeleniumCommonActions.ClickElement(searchButton);
        }

        public void VerifyPosterPresent()
        {
           By verifyPoster = By.CssSelector(HomePage_Locators.verifyPosterByCss);
            Assertions.IsTrue(_SeleniumCommonActions.IsDisplayed(verifyPoster), "Poster is displayed");
        }
        
        public void VerifySearchResultIsPresent(string value)
        {
           By searchResults = By.CssSelector(HomePage_Locators.searchReasultByCss);
            _SeleniumCommonActions.WaitForSecs(2);
           Assertions.IsTrue(_SeleniumCommonActions.getWebElementsByLocator(searchResults).First().Text.Contains(value), "java search result is verified");
        }

        public void VerifyHomepageElements(string feedbackValue, string updateValue, string emailValue, string menuBarValue)
        {
            By feedBack = By.CssSelector(HomePage_Locators.feedBackByCss);
            Assertions.IsTrue(_SeleniumCommonActions.getWebElementsByLocator(feedBack).First().Text.Contains(feedbackValue), feedbackValue + "is present on homepage");
            By upDates = By.CssSelector(HomePage_Locators.feedBackByCss);
            Assertions.IsTrue(_SeleniumCommonActions.getWebElementsByLocator(upDates).Last().Text.Contains(updateValue), updateValue + "is present on homepage");
            By feedBackEmail = By.CssSelector(HomePage_Locators.feedBackMailByCss);
            Assertions.IsTrue(_SeleniumCommonActions.GetText(feedBackEmail).Contains(emailValue), updateValue + "is present on homepage");
            Assertions.IsTrue(_SeleniumCommonActions.ElementsPresent(HomePage_Locators.homeMenuBarByCss).ElementAt(2).Text.Contains(menuBarValue), menuBarValue + "is present on homepage");
        }


    }
}
