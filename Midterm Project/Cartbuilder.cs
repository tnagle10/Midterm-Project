using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_Project
{
    public class Cartbuilder
    {

        public void buildCart(List<Product> cartlist,Product newItem)
        {

            Console.WriteLine("\nThe following item has been added to the cart");
            Console.WriteLine("Product Name        Description                            Price    Quantity");
            Console.WriteLine("*******************************************************************************");
            Console.WriteLine(newItem.Name.PadRight(20) +newItem.Description.PadRight(38) +newItem.Price.ToString().PadLeft(6) + newItem.Quantity.ToString().PadLeft(10));
            cartlist.Add(newItem);

            Console.WriteLine("\n\nHere is your current cart");
            Console.WriteLine("Product Name        Description                            Price    Quantity");
            Console.WriteLine("*******************************************************************************");

            foreach (Product item in cartlist)
            {
                Console.WriteLine(item.Name.PadRight(20) + item.Description.PadRight(38) + item.Price.ToString().PadLeft(6) + item.Quantity.ToString().PadLeft(10));
            }
           
        }

        public Product chooseQuantity(List<Product> Inventory, Product item)
        {
            /*
               Name: chooseQuantity
               Description: This method displays a list of products, and gets the customer to select a Quantity
               Input: Inventory list of Products and a chosen Product
               Output: A chosen Quantity
            */

            // Initial quantity is 0
            int quantity = 0;
            // Boolean value for valid input
            Boolean valid = false;
            // Create a new Product for the cart separate from Inventory
            Product cart = new Product("","","",0,0);
            
            // Loop until customer chooses quantity            
            while (valid == false)
            {
                Console.WriteLine("How many would you like?");
                if (!(int.TryParse(Console.ReadLine(), out quantity)))
                // Quantity is not a valid integer
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered an invalid number\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;

                }
                // Quantity is valid, update the Inventory, and the quantity of the selected Product
                else if ((quantity >= 0) && (quantity <= item.Quantity))
                {
                    item.Quantity = item.Quantity - quantity;
                    cart.Quantity = quantity;
                    valid = true;

                }
                // Quantity is more than the quantity in Inventory
                else if ((quantity > 0) && (quantity > item.Quantity))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sorry, we don't have enough in stock.\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;
                    valid = false;

                }
                // Generic error of bad number
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered an invalid number\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;
                    valid = false;
                }

            }

            // Set values for the cart Product
            cart.Category = item.Category;
            cart.Description = item.Description;
            cart.Name = item.Name;
            cart.Price = item.Price;
            cart.Quantity = quantity;

            // Return the Product in the cart
            return cart;
            
        }

        public Product chooseProduct(List<Product> Inventory, string category)
        {
            /*
               Name: chooseProduct
               Description: This method displays a list of products, and gets the customer to select a Product
               Input: Inventory list of Products and a chosen category
               Output: A chosen Product
            */
            // New Product type that will hold pick
            Product chosen = new Product("invalid", "invalid", "invalid", 0, 0);

            // Get a list of products that are in the same category
            List<Product> prodList = createProductList(Inventory,category);
            if (prodList.Count == 0)
            {
                chosen = new Product("out", "out", "out", 0, 0);
                return chosen;
            }

            // Create a new list of sorted product names
            List<string> prodName = new List<string>();
            foreach (Product product in prodList)
            {
                prodName.Add(product.Name);
                   
            }
            
            // Sort the list
            prodName.Sort();

           
            // Loop until customer picks an item
            Boolean valid = false;
            int input = 0;
            
            while (valid == false)
            {

                // Create a new product list that will hold sorted products
                List<Product> prodListSorted = new List<Product>();

                // Build the sorted product list
                foreach (string name in prodName)
                {
                    Product found = prodList.Find(x => x.Name == name);
                    prodListSorted.Add(found);
                }

                // Build a product menu list
                Console.WriteLine("\nPlease choose a product:  (Choose -1 to exit)");
                Console.WriteLine("Product Name           Description                            Price    Quantity");
                Console.WriteLine("*******************************************************************************");
                for (int i = 0; i < prodListSorted.Count; i++)
                {
                    Console.WriteLine(i + 1 + ": " + prodListSorted[i].Name.PadRight(20) + prodListSorted[i].Description.PadRight(38)+prodListSorted[i].Price.ToString().PadLeft(6) +prodListSorted[i].Quantity.ToString().PadLeft(10));
                }

                // Read in a number, and check to make sure it is a valid integer
                Console.WriteLine();
                if (!(int.TryParse(Console.ReadLine(), out input)))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered an invalid number\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;

                }

                // Selection number is good
                else if ((input > 0) && (input <= prodList.Count))
                {
                    Console.WriteLine("\nYou chose " + prodListSorted[input - 1].Description + " " + prodListSorted[input - 1].Name + "\n");
                    valid = true;
                    chosen = prodListSorted[input - 1];
                }
                else if (input < 0)
                {
                    chosen = new Product("no choice", "no choice", "no choice", 0, 0);
                    return chosen;
                }
                else 
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered an invalid number\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;
                    valid = false;
                }

            }

            
            return chosen;

        }

        public string chooseCategory(List<Product> inventory)
        {
            /*
             Name: chooseCategory
             Description: This method takes a product list and generates a category list.  It then generates
                          a menu of categories for a customer to pick.  
             Inputs: List of products
             Output: Category chosen by customer.
            */

            // Create a hash table of categories      
            Hashtable categories = createCategoryList(inventory);
            // Boolean value to test for valid input
            Boolean valid = false;
            
            // Category number that user picks
            int input = 0;
            // List of category names.  Put it to list for sorting.
            List<string> catList = new List<string>();
            

            Console.WriteLine("*****************************************************************************");
            Console.WriteLine("*                Welcome to the Grand Circus Grocery Store                  *");
            Console.WriteLine("*                                                                           *");
            Console.WriteLine("*****************************************************************************");
            while (valid == false)
            {
        
                Console.WriteLine("\nPlease choose a product category:");
                // For each entry in the Hash table, create an entry in catNames array
                // This is just a list of categories
                foreach (DictionaryEntry entry in categories)
                {
                   catList.Add(entry.Value.ToString());
                }

                // Sort the categories
                catList.Sort();

                // Print the categories in a list with a number corresponding to each category
                for (int i = 0; i < catList.Count; i++)
                {
                    Console.WriteLine((i + 1).ToString().PadLeft(3) + ": "+ catList[i]);
                }

                // Read in a number, and check to make sure it is a valid integer
                Console.WriteLine();
                if (!(int.TryParse(Console.ReadLine(), out input)))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered an invalid number\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                // Check to make sure number is in the correct range
                else if ((input > 0) && (input <= catList.Count))
                {
                    valid = true;
                }
                // Number is not in the range
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered an invalid number\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;
                    valid = false;
                }
            }
            
            // Output the category chosen
            return catList[input - 1];

        }

        public Hashtable createCategoryList(List<Product> Inventory)
        {
            /*
               Name: createCategoryList
               Description: This method creates a hash table of categories from a product list
               Input: Product list
               Output: Hash 
            */

            // Create a Hashtable that holds categories
            Hashtable categories = new Hashtable();
            foreach (Product item in Inventory)
            {
                // Add each category to Hash table
                try
                {
                    categories.Add(item.Category.GetHashCode(), item.Category);
                }
                catch (Exception)
                {

                }

            }

            return categories;



        }

        public List<Product> createProductList(List<Product> Inventory, string category)
        {
            /* Name: createProductList
               Description:  This method returns a list of products that match the Category
               Input:  The inventory list and a chosen category
               Output: Returns a list of Products that match the category
            */

            // Create a new list that holds products that match category
            List<Product> prodList = new List<Product>();
            // Loop through the inventory and add products to list that match category
            foreach (Product item in Inventory)
            {
                prodList = Inventory.FindAll(x => x.Category == category && x.Quantity > 0);
            }
            return prodList;
        } 

        public bool keepGoing()
        {
            /* Name: keepGoing
               Description:  This method implements a loop to determine if users wants to continue
               Input:  None
               Output: Returns false if user doesn't want to continue.  Otherwise returns true.
                       Outputs values to Console
            */


            // If user enters "q", execute exit procedure
            Console.WriteLine("\nAdd another item to cart? (y/n):");
            string input = Console.ReadLine();

            if (input == "n")
            {

                return false;

            }

            return true;
        }

    }
}
