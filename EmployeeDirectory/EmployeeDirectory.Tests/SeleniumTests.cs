using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace EmployeeDirectory.Tests;

public class SeleniumTests
{
    private IWebDriver driver;
    private string baseUrl = "http://18.61.228.241:5000"; // 🔴 replace

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
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