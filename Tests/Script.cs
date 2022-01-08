using AmazonWork.Modules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmazonWork
{
    [TestClass]
    public class Script : BaseTest
    {
        Home_Page _HomePage;
       
        [ClassInitialize]
        public static void ClassInitialization(TestContext context)
        {
            OneTimeSetUp(context);
        }

        [TestInitialize]
        public void TestInitializtion()
        {
            _HomePage = new Home_Page();
        }

        [TestMethod]
        public void verifysearchTestCase()
        {
            _HomePage.VerifyContentCanBeSearched(Constants.searchValue);
        }

        [TestMethod]
        public void verifyHomePagePosterTestCase()
        {
            _HomePage.VerifyPosterPresent();
        }
        
        [TestMethod]
        public void verifySearchResultTestCase()
        {
            _HomePage.VerifyContentCanBeSearched(Constants.searchValue);
            _HomePage.VerifySearchResultIsPresent(Constants.searchValue);
        }  
        
        [TestMethod]
        public void verifyHomepageElementsTestCase()
        {
           _HomePage.VerifyHomepageElements(Constants.feedback, Constants.updates, Constants.mailFeedback, Constants.searchValue);
        }


        [ClassCleanup()]
        public static void ClassCleanUp()
        {
            OneTimeTearDown();
            //ExtentReportUtility.ClassClean; No need already called above
        }
    }
}
