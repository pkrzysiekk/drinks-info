using DrinksInfo.Controllers;
using DrinksInfo.Model;
using DrinksInfo.Views;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DrinksInfo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
          MainMenu mainMenu = new();
          await mainMenu.ShowAsync();
            

        }
    }
}
