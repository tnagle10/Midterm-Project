using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_Project
{
    class Transaction
    {
        public static List<float> totalsCalc(List<Product> Cart)
        {
            float subTotal = 0;
            float grandTotal = 0;
            float taxTotal = 0;
            foreach (var product in Cart)
            {
                subTotal += product.Price * product.Quantity;

            }

            taxTotal = subTotal * .06f;
            grandTotal = subTotal + taxTotal;

            List<float> allTotals = new List<float>();
            allTotals.Add(subTotal);
            allTotals.Add(taxTotal);
            allTotals.Add(grandTotal);

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Subtotal:   ${Math.Round(subTotal,2)}");
            Console.WriteLine($"SalesTax:   ${Math.Round(taxTotal, 2)}");
            Console.WriteLine($"Grandtotal: ${Math.Round(grandTotal, 2)}");
            Console.BackgroundColor = ConsoleColor.Black;
            return (allTotals);
        }

        public static void processPayment(List<float> allTotals)
        {
            Console.WriteLine("Please enter your payment method,choose from cash/credit/check ");
            Console.WriteLine("For cash enter 1");
            Console.WriteLine("For check enter 2");
            Console.WriteLine("For credit enter 3");

            //validating and retreiving initial user input
            int choice;
            while (true)
            {
                while (!int.TryParse(Console.ReadLine(), out choice))
                {

                    Console.WriteLine("Please enter a correct value.");
                }

                if (!(choice >= 1 && choice <= 3))

                    Console.WriteLine("Please enter 1, 2, or 3.");

                else if (choice == 1 || choice == 2 || choice == 3)
                {
                    break;
                }

                //validating and retreiving initial user input


            }
            switch (choice)
            {
                case 1:
                    float changeDue = getChange(allTotals);
                    Console.WriteLine($"Your change due is ${Math.Round(changeDue,2)}");
                    break;
                case 2:
                    Console.WriteLine("Please enter the check number");
                    int checkNumber;
                    while (!int.TryParse(Console.ReadLine(), out checkNumber))
                    {

                        Console.WriteLine("Please enter an appropriate value.");
                    }
                    break;
                case 3:
                    DateTime currentDate = new DateTime();
                    currentDate = DateTime.Now;
                    int currentMonth = currentDate.Month;
                    int currentYear= int.Parse(currentDate.Year.ToString().Substring(2));
                    long cardNumber;
                    int CVV;
                    Console.WriteLine("Enter your 16 digit Card Number in the format below:");
                    Console.WriteLine("XXXXXXXXXXXXXXXX");
                    ///////////////////////
                    while (true)
                    {
                        //TODO present credit card format

                        while (!long.TryParse(Console.ReadLine(), out cardNumber))
                        {
                            Console.WriteLine("Please enter an appropriate value.");
                            Console.WriteLine("XXXXXXXXXXXXXXXX");
                        }
                        if (cardNumber.ToString().Length != 16)
                        {
                            Console.WriteLine("Please enter a 16 digit value");
                            Console.WriteLine("XXXXXXXXXXXXXXXX");
                        }
                        else break;
                    }
                        ///////////////////////////////////////
                       
                    while (true)/////
                    {
                        Console.WriteLine("Enter year of expiration (YY) ");
                        int year;
                        //GET YEAR
                        while (true)    
                        {
                            while (!int.TryParse(Console.ReadLine(), out year))
                            {
                                Console.WriteLine("Please enter a valid number for input of year.");
                            }
                            if (year <= 0)
                            {
                                Console.WriteLine("Year cannot be negative, please enter again.");
                            }

                            else break;
                        }
                        //GET MONTH
                        Console.WriteLine("Select month of expiration Enter 1 - 12");
                        int month;
                        while (true)
                        {
                            while (!int.TryParse(Console.ReadLine(), out month))
                            {
                                Console.WriteLine("Please enter a valid number.");
                            }
                            if (month < 1 || month > 12)
                                Console.WriteLine("Please enter a value that corresponds to a month. Please try enter another month.");

                            else break;
                        }

                        //CHECK FOR EXPIRATION

                        if ((year < currentYear) || (year == currentYear && month <= currentMonth))
                        {
                            Console.WriteLine("Your card has expired.");
                        }


                        else if ((year > currentYear + 5) || (year == currentYear + 5 && month > currentMonth))
                        {
                            Console.WriteLine("Expiration date cannot be beyond five years from current year. Please enter again.");
                        }

                        else break;
                    }////



                    Console.WriteLine("Enter CVV");
                    
                    while (true)
                    {
                        while (!int.TryParse(Console.ReadLine(), out CVV))
                        {
                            Console.WriteLine("Please enter an appropriate value.");
                        }
                        if (CVV.ToString().Length != 3)
                            Console.WriteLine("Please enter a 3 digit value");
                        else break;
                    }
                    break;
            }

        }

        //altotals== {subTotal, taxTotal, grandTotal}

        public static float getChange(List<float> allTotals)
        {
            float change = 0;
            float totalPaidCash = 0;
            while (true)
            {
                Console.WriteLine("Please enter your cash amount.");
                float paidCash;
                while (!float.TryParse(Console.ReadLine(), out paidCash))
                {

                    Console.WriteLine("Please enter a correct value.");
                }
                totalPaidCash += paidCash;
                change = totalPaidCash - allTotals[2];

                if (change < 0)
                    Console.WriteLine($"You did not enter sufficient cash. You still owe ${Math.Round(Math.Abs(change), 2)}");
                else break;
            }
            return change;
        }

    }
}//END
