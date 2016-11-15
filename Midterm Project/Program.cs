using System;
using System.Collections;
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

            List<Product> ProductList = Inventory.ReadDataFromFile();
            foreach (var item in ProductList)
            {
                Console.WriteLine($"{item.Category}, {item.Name}, {item.Description}, {item.Price}, {item.Quantity}");
            }

          
            Cartbuilder cart = new Cartbuilder();
            do {

                string category = cart.chooseCategory(ProductList);
                Product chosenWithoutQuantity = cart.chooseProduct(ProductList, category);
                Product chosenWithQuantity = cart.chooseQuantity(ProductList, chosenWithoutQuantity);
                foreach (var item in ProductList)
                {
                    Console.WriteLine($"{item.Category}, {item.Name}, {item.Description}, {item.Price}, {item.Quantity}");
                }

            } while (cart.keepGoing());
            //List<float> Totals = Transaction.totalsCalc(ProductList);
            //Transaction.processPayment(Totals);
        }

        
    }
}