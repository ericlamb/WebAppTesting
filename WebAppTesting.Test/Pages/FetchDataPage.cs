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

        public NewForecastForm NewForcast => new NewForecastForm(Driver.FindElement(By.XPath("//app-fetch-data/form")));

        internal class Forecast : PageElement
        {
            public Forecast(NgWebElement ngWebElement) : base(ngWebElement)
            {
            }

            public DateTime Date => DateTime.Parse(WebElement.FindElement(By.XPath(".//td[1]")).Text);

            public int TempC => int.Parse(WebElement.FindElement(By.XPath(".//td[2]")).Text);

            public int TempF => int.Parse(WebElement.FindElement(By.XPath(".//td[3]")).Text);

            public string Summary => WebElement.FindElement(By.XPath(".//td[4]")).Text;

            internal void Delete()
            {
                var deleteButton = WebElement.FindElement(By.XPath(".//button"));
                deleteButton.Click();
                WebElement.NgDriver.WaitForAngular();
            }
        }

        internal class NewForecastForm : PageElement
        {
            public NewForecastForm(NgWebElement ngWebElement)
                : base(ngWebElement) { }

            public DateTime Date
            {
                get => DateTime.Parse(DateInput.Value);
                set => DateInput.Value = value.ToString();
            }

            public int TempC
            {
                get => int.Parse(TempInput.Value);
                set => TempInput.Value = value.ToString();
            }

            public string Summary
            {
                get => SummaryInput.Value;
                set => SummaryInput.Value = value;
            }

            public void Submit()
            {
                var submitButton = WebElement.FindElement(By.XPath(".//button"));
                submitButton.Click();
                WebElement.NgDriver.WaitForAngular();
            }

            private WebInput DateInput => new WebInput(WebElement.FindElement(By.XPath(".//input[@formcontrolname='date']")));

            private WebInput TempInput => new WebInput(WebElement.FindElement(By.XPath(".//input[@formcontrolname='temperatureC']")));

            private WebInput SummaryInput => new WebInput(WebElement.FindElement(By.XPath(".//input[@formcontrolname='summary']")));
        }
    }
}
