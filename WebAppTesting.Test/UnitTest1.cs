using WebAppTesting.Test.Pages;
using Xunit;

namespace WebAppTesting.Test
{
    public class UnitTest1 : SeleniumTest
    {
        [Fact]
        public void WhenCounterIsIncremented()
        {
            var homePage = new HomePage(NgDriver);
            var countPage = homePage.OpenCountPage();

            countPage.Increment();
            
            Assert.Equal(1, countPage.CurrentCount);
        }
    }
}
