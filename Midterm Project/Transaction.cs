using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(String.Format("{0,-26} {1,0}  {2,26}", " ", "RECEIPT", " "));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("--------------------------------------------------------------");
            foreach (var item in Cart)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    Console.WriteLine(String.Format("{0,-20} {1,0} {2,0}", item.Name, item.Description.PadRight(30), item.Price.ToString("C").PadLeft(10)));
                }
                
            }

            Console.WriteLine("--------------------------------------------------------------");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(String.Format("{0,-10} {1,51}","SUBTOTAL",subTotal.ToString("C")));
            Console.WriteLine(String.Format("{0,-10} {1,51}","SALESTAX",taxTotal.ToString("C")));
            Console.WriteLine(String.Format("{0,-10} {1,51}","GRANDTOTAL",grandTotal.ToString("C")));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
            return (allTotals);
        }

        public static void processPayment(List<float> allTotals)
        {
            Console.WriteLine("Please enter your payment method,choose from cash/credit/check ");
            Console.WriteLine("For cash enter 1");
            Console.WriteLine("For check enter 2");
            Console.WriteLine("For credit enter 3");

            //for check verification-printing the ending message
            bool checkverify = true;
            //for credit card verification-printing card rejected
            int cardCount = 0;

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

             //


            }
            switch (choice)
            {
                case 1://CASH
                    float changeDue = getChange(allTotals);
                    Console.WriteLine($"Your change due is {changeDue.ToString("C")}");
                    break;
                case 2://CHECK
                    Random r = new Random(DateTime.Now.Second);
                    int Verify = r.Next(1,3);
                    //Console.WriteLine($"Verify = {Verify}");  //<--------------------only using this to verify the variable, Verify
                    Console.WriteLine("Please enter the check number");
                    int checkNumber;
                    while (!int.TryParse(Console.ReadLine(), out checkNumber))
                    {

                        Console.WriteLine("Please enter an appropriate value.");
                    }

                    //So you have a 50/50 chance of the chaeck not being accepted
                    if (Verify == 1)
                    {
                        checkverify = false;
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("CHECK DENIED");                       
                        Console.WriteLine("THE LOCAL POLICE DEPARTMENT HAS BEEN NOTIFIED");

                        List<string> symbols = new List<string>();
                        symbols.Add(".");
                        symbols.Add(".");
                        symbols.Add(".");
                        symbols.Add(".");
                        symbols.Add("have fun in prison");

                        foreach (string item in symbols)
                        {
                            Console.Write(item + " ");
                            Thread.Sleep(600);
                        }
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                    else if (Verify == 2)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Check Accepted");
                        Console.ResetColor();
                    }

                    break;


                case 3://CARD

                    //Initial variables used for card input and validation
                    DateTime currentDate = new DateTime();
                    currentDate = DateTime.Now;
                    int currentMonth = currentDate.Month;
                    int currentYear= int.Parse(currentDate.Year.ToString().Substring(2));
                    long cardNumber;
                    int CVV;
                    bool getCVV = true;

                    Console.WriteLine("Enter your 16 digit Card Number in the format below:");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("XXXXXXXXXXXXXXXX");
                    Console.ResetColor();
                    ///////////////////////
                    while (true)
                    {

                        while (!long.TryParse(Console.ReadLine(), out cardNumber))
                        {
                            Console.WriteLine("Please enter an appropriate value.");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("XXXXXXXXXXXXXXXX");
                            Console.ResetColor();
                        }
                        if (cardNumber.ToString().Length != 16)
                        {
                            Console.WriteLine("Please enter a 16 digit value");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("XXXXXXXXXXXXXXXX");
                            Console.ResetColor();
                        }
                        else break;
                    }                      
                    while (true)/////
                    {
 
                        Console.WriteLine("Enter the year of expiration (YY)");
                        int year;
                                               
                        //GET YEAR
                        while (true)    
                        {
                            while (!int.TryParse(Console.ReadLine(), out year))
                            {
                                Console.WriteLine("Please enter a valid number for input of year.");
                                Console.WriteLine();
                            }
                            if (year.ToString().Length>2 && year >= 0)
                            {
                                Console.WriteLine("Only enter the last two digits");
                                Console.WriteLine();
                            }
                            else if (year.ToString().Length > 2 && year < 0)
                            {
                                Console.WriteLine("Only enter the last two digits and also, the year can't be negative");
                                Console.WriteLine();
                            }
                            else if (year < 0 && year.ToString().Length <= 2)
                            {
                                Console.WriteLine("Year cannot be negative");
                                Console.WriteLine();
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
                        if (cardCount == 3)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("CARD REJECTED");
                            Console.ResetColor();
                            getCVV = false;
                            break;
                        }

                        else if ((year < currentYear) || (year == currentYear && month <= currentMonth))
                        {
                            Console.WriteLine("Your card has expired. Please enter again.");
                            cardCount++;
                        }

                        else if ((year > currentYear + 5) || (year == currentYear + 5 && month > currentMonth))
                        {
                            Console.WriteLine("Expiration date cannot be beyond five years from current year. Please enter again.");
                            cardCount++;
                        }

                        else break;
                    }////
                    //ONLY GET CVV IF CARD HASN'T EXPIRED
                    if (getCVV==true)
                    {
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
                    break;
            }//end of switch (choice)
                     
            //Won't print this friendly message if check isn't accepted
            if (checkverify == true)
            {
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Thank you for shopping at the Grand Circus Grocery Store, come back soon!");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();
            }
            //
        }
       
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
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"You did not enter sufficient cash. You still owe {Math.Abs(change).ToString("C")}");
                    Console.ResetColor();
                }
                else break;
            }
            return change;
        }

    }
}//END
