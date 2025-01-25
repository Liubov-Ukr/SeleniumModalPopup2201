using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace TestModalWindow2201
{
    //// Class for testing window/tab switching in a web application
    internal class WindowSwitchTest
    {
        //// WebDriver for controlling the browser
        private IWebDriver driver = null!;

        // WebDriverWait for handling elements on the page
        private WebDriverWait wait = null!;

        // ExtentReports instance for logging test results
        private ExtentReports extent = null!;

        //// Configures the Spark reporter to generate an HTML report
        private ExtentSparkReporter sparkReporter = null!;
        /// Flag to track if the target window was found.
        private bool foundTargetWindow = false; 

        [SetUp]
        //// Method for setup before running the tests
        public void Setup() 
        {
            sparkReporter = new ExtentSparkReporter("TestReport.html");
            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }
        //Test to verify switching to a new window/tab and validating its URL
        [Test]
        public void Test() 
        {
            var test = extent.CreateTest("Window switch test");
            
            // Step 1: Click the link to open a new tab
            test.Log(Status.Info, "Click on the link to open a new Window");
            IWebElement linkClickHere = driver.FindElement(By.CssSelector("#content > div > a"));
            linkClickHere.Click();
            
            // Step 2: Wait until a new window is opened.
            wait.Until(driver => driver.WindowHandles.Count > 1);

            // Step 3: Save the main window handle and get all open window handles
            var mainWindow = driver.CurrentWindowHandle;
            var allWindows = driver.WindowHandles;

            // Expected URL of the target window.
            string targetWindowUrl = "https://the-internet.herokuapp.com/windows/new";

            // Step 4: Iterate through all windows and switch to the target window.
            foreach (var window in allWindows)
            {
                if (window != mainWindow)
                {
                    driver.SwitchTo().Window(window);
                    if (driver.Url == targetWindowUrl) 
                    {

                        foundTargetWindow = true;
                        break;
                    }
                }
            }

            // Log the current URL for reporting.
            test.Log(Status.Info, $"CurrentURL = {driver.Url}");

            // Step 5: Assert that the target window was found.
            Assert.That( foundTargetWindow, Is.True, "Incorrect window" );
            test.Log(Status.Pass, "Test passed successfully");
        }

        /// TearDown method executed after each test.
        /// Closes the browser and finalizes the report.
        [TearDown]
        public void TearDown() 
        {
         extent.Flush();
         driver.Quit();
         driver.Dispose();
        }
    }
}
