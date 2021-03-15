using OpenQA.Selenium;
using Protractor;

namespace WebAppTesting.Test.Pages
{
    class ApplicationPage
    {
        public ApplicationPage(NgWebDriver driver) =>
            Driver = driver;

        internal NgWebDriver Driver { get; }

        public CountPage OpenCountPage()
        {
            var counterLink = Driver.FindElement(By.XPath("//app-nav-menu")).FindElement(By.LinkText("Counter"));
            counterLink.Click();
            Driver.WaitForAngular();
            return new CountPage(Driver);
        }
    }
}
