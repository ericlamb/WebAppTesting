using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Protractor;

namespace WebAppTesting.Test.Pages
{
    internal class FetchDataPage : ApplicationPage
    {
        public FetchDataPage(NgWebDriver driver) : base(driver)
        {
        }

        public IEnumerable<Forecast> Forcasts => Driver.FindElements(By.XPath("//app-fetch-data//tbody//tr")).Select(x => new Forecast(x));

        internal class Forecast : PageElement
        {
            public Forecast(NgWebElement ngWebElement) : base(ngWebElement)
            {
            }

            public DateTime Date => DateTime.Parse(WebElement.FindElement(By.XPath(".//td[1]")).Text);

            public int TempC => int.Parse(WebElement.FindElement(By.XPath(".//td[2]")).Text);

            public int TempF => int.Parse(WebElement.FindElement(By.XPath(".//td[3]")).Text);

            public string Summary => WebElement.FindElement(By.XPath(".//td[4]")).Text;
        }
    }
}
