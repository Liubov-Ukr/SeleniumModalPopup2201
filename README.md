# TestModalWindow2201 Repository

This repository contains automated Selenium test cases for practicing window handling and modal pop-up testing using C# and NUnit.

## 📌 Overview
The project includes test cases for:
- Handling modal pop-ups
- Switching between browser windows/tabs
- Verifying correct window interactions and closing modal windows

## ✅ Completed Test Cases

### 1. Modal Window Test
- **File:** `ModalPopupTest.cs`  
- **Description:** Tests handling of modal pop-ups and verifies the ability to switch back to the main window after closing the pop-up.

### 2. Window Switch Test
- **File:** `WindowSwitchTest.cs`  
- **Description:** Tests switching between browser tabs and verifies the title of the new tab.

### 3. Closing Modal Window Test
- **File:** `ClosingModalWindow.cs`  
- **Description:** Tests that a modal pop-up window can be detected, switched to, and closed properly without affecting the main window.  
- **Key features:**  
  - Opens a modal window from a button click.  
  - Waits for the window to appear and verifies the expected URL.  
  - Closes only the correct modal window and switches back to the main window safely.  
  - Uses `ExtentReports` for detailed logging of the test process.  

## 🛠 Installation & Setup
To run the tests, follow these steps:

1. **Clone the repository**  
git clone https://github.com/your-username/TestModalWindow2201.git cd TestModalWindow2201
2. **Install dependencies**  
Open the project in Visual Studio and install the required NuGet packages:
- `Selenium.WebDriver`
- `Selenium.Support`
- `ExtentReports`
- `NUnit`

Alternatively, use the NuGet Package Manager Console:
Install-Package Selenium.WebDriver Install-Package Selenium.Support Install-Package ExtentReports Install-Package NUnit


3. **Ensure ChromeDriver is installed**  
- Download and place it in your system `PATH`, or specify the path in your code.

## 🚀 Running the Tests
You can execute the tests using **Visual Studio** or **Command Line**:

### **Option 1: Run tests in Visual Studio**
- Open **Test Explorer** (`Test > Windows > Test Explorer`)
- Click **Run All** or run individual tests.

### **Option 2: Run tests using the .NET CLI**
dotnet test

## ⚠️ Notes
- Ensure that all dependencies are installed before running the tests.
- Tests require Google Chrome and ChromeDriver.
- The ExtentReports HTML report will be generated after test execution.

## 📄 License
This project is licensed under the MIT License - see the `LICENSE.txt` file for details.
