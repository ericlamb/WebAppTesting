using Protractor;

namespace WebAppTesting.Test.Pages
{
    internal class WebInput : PageElement
    {
        public WebInput(NgWebElement ngWebElement) : base(ngWebElement)
        {
        }

        public string Value
        {
            get => WebElement.Text;
            set {
                WebElement.Clear();
                WebElement.SendKeys(value);
                WebElement.NgDriver.WaitForAngular();
            }
        }
    }
}
