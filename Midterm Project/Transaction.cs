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
                subTotal += product.Price*product.Quantity;

            }

            taxTotal = subTotal * .06f;
            grandTotal = subTotal + taxTotal;

            List<float> allTotals = new List<float>();
            allTotals.Add(subTotal);
            allTotals.Add(taxTotal);
            allTotals.Add(grandTotal);
            Console.WriteLine($"Subtotal: {subTotal}, SalesTax: {taxTotal}, Grandtotal {grandTotal}");
            return (allTotals);
        }

        public static void processPayment( List<float>allTotals)
        {
            Console.WriteLine("Please enter your payment method");



        }



    }}//END
