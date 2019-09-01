using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Configuration;
using Contracts;

namespace ImdbProject
{
    class Program
    {
        static void Main(string[] args)
        {

            IWebDriver driver = new ChromeDriver();
            Program p = new Program();
            TempList tl = new TempList();



            var url = "https://www.imdb.com/list/ls052283250/";
            var nameElement = "itemprop";
            var roleElement = "infobar";
            var dateElement = "#name-born-info > time";
            string actorsSelector1 = "#main > div > div.lister.list.detail.sub-list > div.lister-list > div:nth-child(";
            string actorsSelector2 = ") > div.lister-item-content > h3 > a";
            int counter = 1;
            driver.Url = url;
            tl.tempList = new List<Actor>();
            for (int i = 1; i < 100; i++)
            {
                p.ClickOnLink(actorsSelector1, actorsSelector2, driver, counter, nameElement, roleElement, dateElement);

                counter++;
            }
        }


        StoringValues s = new StoringValues();


        public Actor ClickOnLink(string actorsSelector1, string actorsSelector2, IWebDriver driver, int counter, string nameElement, string roleElement, string dateElement)
        {
            IWebElement element1;

            string filePath = ConfigurationManager.AppSettings["filePath"];

            Actor actor = new Actor();

            var parsedInt = counter.ToString();
            var completeVarName = actorsSelector1 + parsedInt + actorsSelector2;
            element1 = driver.FindElement(By.CssSelector(completeVarName));
            element1.GetAttribute("innerHTML");
            element1.Click();
            FindNames(driver, counter, nameElement, actor);
            FindRoles(driver, counter, roleElement, actor);
            FindBirthDate(driver, counter, dateElement, actor);
            s.SerializeJsonToFile(actor, filePath);
            driver.Navigate().Back();

            return actor;
        }

        public Actor FindNames(IWebDriver driver, int counter, string nameElement, Actor actor)
        {

            IWebElement element1;

            element1 = driver.FindElement(By.ClassName(nameElement));
            var str = element1.GetAttribute("innerHTML");
            actor.name = str;
            actor.id = counter;

            return actor;
        }

        public Actor FindBirthDate(IWebDriver driver, int counter, string dateElement, Actor actor)
        {

            IWebElement element1;

            element1 = driver.FindElement(By.CssSelector(dateElement));
            var str = element1.GetAttribute("datetime");
            actor.birthDate = str;

            return actor;
        }


        public Actor FindRoles(IWebDriver driver, int counter, string roleElement, Actor actor)
        {
            IWebElement element1;
            var role = driver.FindElement(By.Id("name-job-categories")).Text;
            actor.role = role;

            return actor;
        }
    }
}


