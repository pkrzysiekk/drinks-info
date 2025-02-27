using DrinksInfo.Controllers;
using DrinksInfo.Model;
using DrinksInfo.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                );
            return choice;
        }
        public async Task ShowAsync()
        {
            var category = await GetUserChoice();
            AnsiConsole.MarkupLine($"Select a drink from the category [bold]{category}[/]");
            var drinks = await _drinksController.GetResponseList<Drink, Drinks>($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={category}");
            var drink = AnsiConsole.Prompt(
                new SelectionPrompt<Drink>()
                    .Title("Select a drink")
                    .AddChoices(drinks)
                    .UseConverter(x => x.Name)   
            );
            AnsiConsole.MarkupLine($"You have selected [bold]{drink.Name}[/]");
            AnsiConsole.Markup(drink.Id);
            drinks = await _drinksController.GetResponseList<Drink,Drinks>($"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drink.Id}");
            drink=drinks.FirstOrDefault();
            AnsiConsole.MarkupLine($"[Blue]{drink.Instructions}[/]");



        }
    }
}
