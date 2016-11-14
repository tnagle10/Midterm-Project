using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_Project
{
    class Inventory
    {


        public static List<Product> ReadDataFromFile()
        {
            List<Product> ProductList = new List<Product>();

            string fileLocation = "../../Inventory.txt";

            StreamReader reader = new StreamReader(fileLocation);

            string Data = reader.ReadToEnd().Trim();

            string[] Records = Data.Split('\n');

            foreach (string record in Records)
            {
                string[] rc = record.Split(',');
                ProductList.Add(new Product(rc[0].Trim(), rc[1].Trim(), rc[2].Trim(), float.Parse(rc[3].Trim()), int.Parse(rc[4].Trim())));
            }
            reader.Close();
            return ProductList;
        }





    }
}
