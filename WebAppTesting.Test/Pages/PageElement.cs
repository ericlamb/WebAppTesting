using Protractor;

namespace WebAppTesting.Test.Pages
{
    internal abstract class PageElement
    {
        protected PageElement(NgWebElement ngWebElement) => WebElement = ngWebElement;

        public NgWebElement WebElement { get; }
    }
}
