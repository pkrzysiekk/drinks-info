using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://plany.ubb.edu.pl/left_menu.php");

        try
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // Czekaj maksymalnie 10 sekund

            // Poczekaj na załadowanie głównego drzewa
            wait.Until(d => d.FindElement(By.CssSelector("ul.main_tree.treeview")).Displayed);
            Console.WriteLine("Strona załadowana.");

            // Znajdź wszystkie elementy <li> zawierające wydziały
            var facultyItems = driver.FindElements(By.CssSelector("ul.main_tree.treeview li"));
            Console.WriteLine($"Znaleziono {facultyItems.Count} wydziałów.");

            // Rekurencyjnie przejdź po wydziałach i znajdź ostatni link
            foreach (var item in facultyItems)
            {
                ExploreFaculty(item, wait);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Naciśnij dowolny klawisz, aby zamknąć program...");
            Console.ReadKey();
        }
    }

    static void ExploreFaculty(IWebElement item, WebDriverWait wait)
    {
        try
        {
            // Sprawdź, czy dany element ma link
            var links = item.FindElements(By.XPath(".//a[contains(@href, 'plan.php')]"));
            if (links.Count > 0)
            {
                foreach (var link in links)
                {
                    // Kliknij każdy link, który prowadzi do planu zajęć
                    Console.WriteLine("Klikam link: " + link.GetAttribute("href"));
                    link.Click();
                    Thread.Sleep(2000); // Poczekaj chwilę na załadowanie strony

                    // Sprawdź, czy pojawiła się zawartość po kliknięciu
                    try
                    {
                        wait.Until(d => d.FindElement(By.CssSelector("div#plan-content")).Displayed);
                        Console.WriteLine("Zawartość planu zajęć została załadowana.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Błąd: " + ex.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Brak linków w tym elemencie.");
            }

            // Sprawdź, czy element zawiera przycisk 'hide' (onclick="hide(6901);")
            var expandButton = item.FindElements(By.XPath(".//a[contains(@onclick, 'hide')]")).FirstOrDefault();
            if (expandButton != null)
            {
                Console.WriteLine("Klikam przycisk rozwijania...");
                // Poczekaj, aż przycisk stanie się widoczny (jeśli to potrzebne)
                wait.Until(d => expandButton.Displayed);

                // Kliknij na przycisk rozwijania
                expandButton.Click();
                Thread.Sleep(2000); // Poczekaj chwilę na rozwinięcie

                // Po kliknięciu, sprawdź, czy pojawiły się nowe elementy
                var expandedItems = item.FindElements(By.CssSelector("ul.treeview li"));
                foreach (var expandedItem in expandedItems)
                {
                    ExploreFaculty(expandedItem, wait); // Rekurencyjnie eksploruj nowe elementy
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas eksploracji tego elementu: {ex.Message}");
        }
    }
}
