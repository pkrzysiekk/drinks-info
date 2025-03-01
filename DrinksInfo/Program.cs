using DrinksInfo.Views;


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
