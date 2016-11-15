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
            // Create inventory from file
            List<Product> ProductList = Inventory.ReadDataFromFile();
            // Shopping cart list, keeps track of chosen items
            List<Product> cartList = new List<Product>();
            // Create a new cart
            Cartbuilder cart = new Cartbuilder();

            // Loop through products until customer is done adding to cart.
            do {
                // Get a selected category from customer
                string category = cart.chooseCategory(ProductList);
                // Choose a product
                Product chosenWithoutQuantity = cart.chooseProduct(ProductList, category);
                if (chosenWithoutQuantity.Name != "invalid")
                {

                    // Choose a quantity for product
                    Product chosenWithQuantity = cart.chooseQuantity(ProductList, chosenWithoutQuantity);

                    // Add product to cart
                    cart.buildCart(cartList, chosenWithQuantity);
                }
                else
                {
                    Console.WriteLine($"No products of type category: {category}");
                }
         
            } while (cart.keepGoing());

            // Calculate totals
            List<float> Totals = Transaction.totalsCalc(cartList);
            // Process payment
            Transaction.processPayment(Totals);
            Inventory.WriteDataToFile(ProductList);
        }

        
    }
}