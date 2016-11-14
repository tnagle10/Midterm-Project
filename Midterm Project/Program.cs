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

            string category = "";
            Cartbuilder cart = new Cartbuilder();
            do {
                Hashtable productCategories = new Hashtable();
                List<Product> productsFound = new List<Product>();

                //productCategories = createCategoryList(ProductList);
                int prodNumber = 0;
                //cart.genList(categories,  out category);
                productCategories = cart.createCategoryList(ProductList);
                Console.Write("\nPlease choose a product category.\n");
                cart.genList(productCategories, out category);
                productsFound = cart.listProducts(ProductList, category);
                cart.genList(productsFound, out prodNumber);
               
                List<float> Totals = Transaction.totalsCalc(ProductList);
            } while (cart.keepGoing());

        }

        
    }
}
