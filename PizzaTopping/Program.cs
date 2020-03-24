using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PizzaTopping
{
    class Program
    {
        static void Main(string[] args)
        {
            var pizzas = JsonConvert.DeserializeObject<List<Pizza>>(File.ReadAllText(@"./pizzas.json"));

            var toppingLists = pizzas.Select(p => string.Join(", ", p.Toppings.OrderBy(t => t)));

            //var distinctToppingCombos = toppingLists.Distinct().OrderBy(d => d);

            var countOfCombos = new Dictionary<string, int>();

            foreach (var combo in toppingLists)
            {
                if (!countOfCombos.ContainsKey(combo))
                {
                    countOfCombos.Add(combo, 1);
                }
                else
                {
                    countOfCombos[combo] += 1;
                }
            }

            var mostOrdered = countOfCombos.OrderByDescending(item => item.Value).Take(20);

            foreach (var (pizza, count) in mostOrdered)
            {
                Console.WriteLine($"The topping combination of {pizza} was ordered {count} times.");
            }

            Console.ReadKey();
        }
    }
}
