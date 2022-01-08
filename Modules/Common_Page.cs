namespace AmazonWork.Modules
{
   public class Common_Page
    {
        protected static SeleniumCommonActions _SeleniumCommonActions;

        static Common_Page()
        {
            _SeleniumCommonActions = SeleniumCommonActions.GetInstance();
        }
    }
}
