using DrinksInfo.Controllers;
using DrinksInfo.Model;
using DrinksInfo.Models;
using Spectre.Console;


namespace DrinksInfo.Views
{
    public class MainMenu : IViewAsync
    {
       private  DrinksController _drinksController= new();
        public async Task<string> GetUserChoice()
        {
           var list= await _drinksController.GetResponseList<Category,Categories>("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");
           
            var choice= AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Select a category")
                .AddChoices(list.Select(x=>x.CategoryName))
                .AddChoices("Exit")
                );
            return choice;
        }
        public async Task ShowAsync()
        {
            while (true)
            {
               
                var category = await GetUserChoice();
                AnsiConsole.Clear();
                if (category == "Exit")
                {
                    AnsiConsole.MarkupLine("[Green]Goodbye![/]");
                    return;
                    
                }
                AnsiConsole.MarkupLine($"Select a drink from the category [bold]{category}[/]");
                var drinks = await _drinksController.GetResponseList<Drink, Drinks>($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={category}");
                var drink = AnsiConsole.Prompt(
                    new SelectionPrompt<Drink>()
                        .Title("Select a drink")
                        .AddChoices(drinks)
                        .UseConverter(x => x.Name)
                );
                AnsiConsole.MarkupLine($"You have selected [bold]{drink.Name}[/]");
                drinks = await _drinksController.GetResponseList<Drink, Drinks>($"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drink.Id}");
                drink = drinks.FirstOrDefault();
                if (drink is null)
                {
                    AnsiConsole.MarkupLine($"[red]No details found for {drink.Name}[/]");
                    return;
                }
                AnsiConsole.MarkupLine($"[Blue]{drink.Instructions}[/]");
            }

        }
    }
}
