using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public IWebDriver driver;

[SetUp]
public void Setup()
{
    var options = new ChromeOptions();

    // REQUIRED for Jenkins/Linux
    options.AddArgument("--headless=new");
    options.AddArgument("--no-sandbox");
    options.AddArgument("--disable-dev-shm-usage");
    options.AddArgument("--disable-gpu");
    options.AddArgument("--window-size=1920,1080");

    // Optional but safer
    options.AddArgument("--remote-allow-origins=*");

    driver = new ChromeDriver(options);
}

    // ✅ Add Employee
    [Test]
    public void AddEmployeeTest()
    {
        driver.Navigate().GoToUrl(baseUrl);

        driver.FindElement(By.Id("name")).SendKeys("John");
        driver.FindElement(By.ClassName("add-btn")).Click();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElement(By.Id("list")).Text.Contains("John"));

        var list = driver.FindElement(By.Id("list"));
        Assert.IsTrue(list.Text.Contains("John"));
    }

    // ✅ Delete Employee
[Test]
public void DeleteEmployeeTest()
{
    driver.Navigate().GoToUrl(baseUrl);

    // Add employee
    driver.FindElement(By.Id("name")).SendKeys("DeleteUser");
    driver.FindElement(By.ClassName("add-btn")).Click();

    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    wait.Until(d => d.FindElement(By.Id("list")).Text.Contains("DeleteUser"));

    // 🔥 Find correct list item
    var items = driver.FindElements(By.TagName("li"));

    foreach (var item in items)
    {
        if (item.Text.Contains("DeleteUser"))
        {
            item.FindElement(By.ClassName("delete-btn")).Click();
            break;
        }
    }

    // Wait until removed
    wait.Until(d => !d.FindElement(By.Id("list")).Text.Contains("DeleteUser"));

    var list = driver.FindElement(By.Id("list"));
    Assert.IsFalse(list.Text.Contains("DeleteUser"));
}

    // ✅ Page Load
    [Test]
    public void PageLoadTest()
    {
        driver.Navigate().GoToUrl(baseUrl);

        var input = driver.FindElement(By.Id("name"));
        Assert.IsTrue(input.Displayed);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
