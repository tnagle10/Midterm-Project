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

        public Product chooseQuantity(List<Product> Inventory, Product item)
        {
            int quantity = 0;
            Boolean valid = false;
            while (valid == false)
            {
                Console.WriteLine("How many would you like?");
                if (!(int.TryParse(Console.ReadLine(), out quantity)))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered an invalid number\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;

                }

                else if ((quantity > 0) && (quantity <= item.Quantity))
                {
                    int find = Inventory.FindIndex(x => x.Name == item.Name);
                    Inventory[find].Quantity = Inventory[find].Quantity - quantity;
                    valid = true;

                }

                else if ((quantity > 0) && (quantity > item.Quantity))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sorry, we don't have enough in stock.\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;
                    valid = false;

                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered an invalid number\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;
                    valid = false;
                }

            }
            return item;
            
        }

        public Product chooseProduct(List<Product> Inventory, string category)
        {
            // Get a list of products that are in the same category
            List<Product> prodList = createProductList(Inventory,category);
            
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
            Product chosen = new Product("", "", "", 0, 0);
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
                Console.WriteLine("\nPlease choose a product:");
                Console.WriteLine("Product Name                Description                Price               Quantity");
                for (int i = 0; i < prodListSorted.Count; i++)
                {
                    Console.WriteLine(i + 1 + ": " + prodListSorted[i].Name.PadRight(25) + " " + prodListSorted[i].Description.PadRight(25)+ "  $" +prodListSorted[i].Price + "                  "+prodListSorted[i].Quantity);
                }

                // Read in a number, and check to make sure it is a valid integer
                if (!(int.TryParse(Console.ReadLine(), out input)))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered an invalid number\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;

                }

                else if ((input > 0) && (input <= prodList.Count))
                {
                    Console.WriteLine("\nYou chose " + prodListSorted[input - 1].Name + " " + prodListSorted[input - 1].Description + "\n");
                    valid = true;
                    chosen = prodListSorted[input - 1];
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
            // Name: chooseCategory
            // Description: This method takes a product list and generates a category list.  It then generates
            // a menu of categories for a customer to pick.  
            // Inputs: List of products
            // Output: Category chosen by customer.

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
                    Console.WriteLine(i + 1 + ": " + catList[i]);
                }

                // Read in a number, and check to make sure it is a valid integer
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

            // Name: createCategoryList
            // Description: This method creates a hash table of categories from a product list
            // Input: Product list
            // Output: Hash 
            Hashtable categories = new Hashtable();
            foreach (Product item in Inventory)
            {
                //Console.WriteLine(movie.Category1.GetHashCode());
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

        public List<Product> createProductList(List<Product> Inventory,string cat)
        {
            List<Product> prodList = new List<Product>();
            foreach (Product item in Inventory)
            {
                prodList = Inventory.FindAll(x => x.Category == cat);
            }
            
            return prodList;

        }

        public bool keepGoing()
        {
            /* Name: keepGoing
            * Description:  This method implements a loop to determine if users wants to continue
            * Input:  None
            * Output: Returns false if user doesn't want to continue.  Otherwise returns true.
            *         Outputs values to Console
            */


            // If user enters "q", execute exit procedure
            Console.WriteLine("\nContinue? (y/n):");
            string input = Console.ReadLine();

            if (input == "n")
            {

                return false;

            }

            return true;
        }

    }
}
