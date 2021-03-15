using OpenQA.Selenium;
using Protractor;

namespace WebAppTesting.Test.Pages
{
    class CountPage : ApplicationPage
    {
        public CountPage(NgWebDriver driver) : base(driver)
        {
        }

        public void Increment()
        {
            var incrementButton =
                Driver.FindElement(By.XPath("//app-counter-component/button"));
            incrementButton.Click();
            Driver.WaitForAngular();
        }

        public int CurrentCount => int.Parse(
            Driver.FindElement(By.XPath("//app-counter-component"))
                .FindElement(By.ClassName("currentCount"))
                .Text);
    }
}
