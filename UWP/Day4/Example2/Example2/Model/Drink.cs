using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2.Model
{
    public class Drink
    {
        public int DrinkId { get; set; }
        public string Title { get; set; }
        public string ImageDrink { get; set; }
    }

    public class DrinkManager
    {
        public static List<Drink> GetDrinks()
        {
            var Drinks = new List<Drink>();
            Drinks.Add(new Drink { DrinkId = 1, Title = "Cooktail 1", ImageDrink = "Assets/1.jpeg" });
            Drinks.Add(new Drink { DrinkId = 2, Title = "Cooktail 2", ImageDrink = "Assets/2.jpeg" });
            Drinks.Add(new Drink { DrinkId = 3, Title = "Cooktail 3", ImageDrink = "Assets/3.jpeg" });
            Drinks.Add(new Drink { DrinkId = 4, Title = "Cooktail 4", ImageDrink = "Assets/4.jpeg" });
            Drinks.Add(new Drink { DrinkId = 5, Title = "Cooktail 5", ImageDrink = "Assets/5.jpeg" });
            Drinks.Add(new Drink { DrinkId = 6, Title = "Cooktail 6", ImageDrink = "Assets/6.jpeg" });
            Drinks.Add(new Drink { DrinkId = 7, Title = "Cooktail 7", ImageDrink = "Assets/7.jpeg" });
            Drinks.Add(new Drink { DrinkId = 8, Title = "Cooktail 8", ImageDrink = "Assets/8.jpeg" });
            Drinks.Add(new Drink { DrinkId = 9, Title = "Cooktail 9", ImageDrink = "Assets/9.jpeg" });
            Drinks.Add(new Drink { DrinkId = 10, Title = "Cooktail 10", ImageDrink = "Assets/10.jpeg" });
            Drinks.Add(new Drink { DrinkId = 11, Title = "Cooktail 11", ImageDrink = "Assets/11.jpeg" });
            Drinks.Add(new Drink { DrinkId = 12, Title = "Cooktail 12", ImageDrink = "Assets/12.jpeg" });
            Drinks.Add(new Drink { DrinkId = 13, Title = "Cooktail 13", ImageDrink = "Assets/13.jpeg" });
            Drinks.Add(new Drink { DrinkId = 14, Title = "Cooktail 14", ImageDrink = "Assets/14.jpeg" });
            Drinks.Add(new Drink { DrinkId = 15, Title = "Cooktail 15", ImageDrink = "Assets/15.jpeg" });
            return Drinks;
        }
    }
}
