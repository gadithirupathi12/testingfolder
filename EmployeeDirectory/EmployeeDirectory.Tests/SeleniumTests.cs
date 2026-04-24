using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var options = new ChromeOptions();

options.AddArgument("--headless=new");     // modern headless mode
options.AddArgument("--no-sandbox");       // required for Jenkins
options.AddArgument("--disable-dev-shm-usage"); // avoids memory crash
options.AddArgument("--disable-gpu");
options.AddArgument("--window-size=1920,1080");

driver = new ChromeDriver(options);
