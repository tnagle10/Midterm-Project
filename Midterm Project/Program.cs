using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is our midterm project");
            List<Product> ProductList = Inventory.ReadDataFromFile();
            foreach (var item in ProductList)
            {
                Console.WriteLine($"{item.Category}, {item.Name}, {item.Description}, {item.Price}, {item.Quantity}");
            }
            List<float> Totals = Transaction.totalsCalc(ProductList);


        }
    }
}
