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
            //foreach (var item in ProductList)
            //{
            //    Console.WriteLine($"{item.Category}, {item.Name}, {item.Description}, {item.Price}, {item.Quantity}");
            //}

            List<Product> cartlist = new List<Product>();
            Cartbuilder cart = new Cartbuilder();
            do {

                string category = cart.chooseCategory(ProductList);
                Product chosenWithoutQuantity = cart.chooseProduct(ProductList, category);
                Product chosenWithQuantity = cart.chooseQuantity(ProductList, chosenWithoutQuantity);
                cart.buildCart(cartlist, chosenWithQuantity);

            } while (cart.keepGoing());
            //List<float> Totals = Transaction.totalsCalc(ProductList);
            //Transaction.processPayment(Totals);
        }

        
    }
}