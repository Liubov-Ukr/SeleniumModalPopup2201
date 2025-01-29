
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace TestModalWindow2201
{
    //сlass for testing the correct opening and closing of a modal window
    public class ModalPopupTest
    {
      private IWebDriver driver = null!;
      private WebDriverWait wait = null!;
      private ExtentReports extent = null!;
      private ExtentSparkReporter sparkReporter = null!;

        [SetUp]
        public void Setup()
        {
            sparkReporter = new ExtentSparkReporter("TestReportModalPopup.html");
            extent = new ExtentReports(); 
            extent.AttachReporter(sparkReporter);

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/window-popup-modal-demo");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        [Test]
        public void Test1()
        {
            string facebookUrl = "https://www.facebook.com/lambdatest/";
            string twitterUrl = "https://x.com/intent/follow?screen_name=Lambdatesting&mx=2";
            bool foundTargetWindow = false;
            var test = extent.CreateTest("Test1 - Form Submission");
            test.Log(Status.Info, "Open modal window");
            IWebElement openTwitterFBPopup = driver.FindElement(By.XPath("//*[@id=\"followboth\"]"));
            openTwitterFBPopup.Click();
            string currentUrl;
            var windowsHandles = driver.WindowHandles;
            foreach (var window in windowsHandles)
            {
                driver.SwitchTo().Window(window); // Switch current Window
                currentUrl = driver.Url;
                if (currentUrl == facebookUrl || currentUrl == twitterUrl)
                {
                    foundTargetWindow = true;
                    break;
                }
            }
            string currentURL = driver.Url;
            test.Log(Status.Info, $"CurrentURL - {currentURL}");
           Assert.That(foundTargetWindow, Is.True,  "Incorrect popup window");

           
            test.Log(Status.Pass, "Success");
           
        }
        [TearDown]
        public void TearDown()
        {
            extent.Flush();
            driver.Quit();
            driver.Dispose();
        }
    }
}
