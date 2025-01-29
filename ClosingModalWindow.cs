using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace TestModalWindow2201
{
    internal class ClosingModalWindow
    {
        // WebDriver for controlling the browser
        private IWebDriver driver = null!;

        // WebDriverWait for handling elements on the page
        private WebDriverWait wait = null!;

        // ExtentReports instance for logging test results
        private ExtentReports extent = null!;

        // Configures the SparkReporter to generate an HTML report
        private ExtentSparkReporter sparkReporter = null!;

        // Setup method executed before running the test
        [SetUp]
        public void SetUp()
        {
            sparkReporter = new ExtentSparkReporter("ClosingModalWindowTest.html");
            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/window-popup-modal-demo");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test()
        {
            var testModalWindow = extent.CreateTest("Window closed test");

            // Step 1: Click the link to open a new tab
            testModalWindow.Log(Status.Info, "Clicking on the link to open a new window");
            var followOnTwitterButton = driver.FindElement(By.XPath("//*[@id=\"__next\"]/section[3]/div/div/div[1]/div/div[1]/a"));
            followOnTwitterButton.Click();

            // Step 2: Wait until a new window is opened
            wait.Until(d => d.WindowHandles.Count > 1);

            // Step 3: Store the main window handle and get all open window handles
            var mainWindow = driver.CurrentWindowHandle;
            var allWindows = driver.WindowHandles;
            string targetWindowUrl = "https://x.com/Lambdatesting";
            bool targetWindowClosed = false;

            // Step 4: Iterate through all windows and find the target window
            foreach (var window in allWindows)
            {
                if (window != mainWindow)
                {
                    driver.SwitchTo().Window(window);
                    testModalWindow.Log(Status.Info, $"Switched to window: {driver.Url}");

                    // Check if the window URL matches the target
                    if (driver.Url.Contains(targetWindowUrl))
                    {
                        testModalWindow.Log(Status.Info, "Target window detected");
                        targetWindowClosed = true;

                        // Close the target window
                        driver.Close();
                        testModalWindow.Log(Status.Info, "Closed the target modal window");
                        break;
                    }
                }
            }

            // Step 5: Ensure switching back to the main window
            if (driver.WindowHandles.Contains(mainWindow))
            {
                driver.SwitchTo().Window(mainWindow);
                testModalWindow.Log(Status.Info, $"Switched back to main window: {driver.Url}");
            }
            else
            {
                testModalWindow.Log(Status.Error, "Main window was unexpectedly closed");
            }

            // Step 6: Assert that the target window was found and closed
            Assert.That(targetWindowClosed, Is.True, "Target window was not found or closed");

            testModalWindow.Log(Status.Pass, "Test passed successfully");
        }

        // TearDown method executed after each test to close the browser and finalize the report
        [TearDown]
        public void TearDown()
        {
            extent.Flush();
            driver.Quit();
            driver.Dispose();
        }
    }
}
