using System;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=localhost;Database=adventureworks;Uid=root;Pwd=1234;"; //get connectionString format from connectionstrings.com and change to match your database
            

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var repo = new DapperDb(conn);
                foreach (var prod in repo.GetProducts())
                {
                    Console.WriteLine("Product Name: " + prod.Name + " Time: " + prod.Date.DayOfWeek);
                }

                repo.DeleteProduct(320);

                repo.InsertProduct(new Product
                {
                    Name = "Candace",
                    Color = "Red",
                    Date = new DateTime(1990, 03, 29),
                    ListPrice = 3.00,
                    ProductNumber = "2bornot2b",
                    SellStartDate = new DateTime(1990, 03, 29),
                    SafetyStockLevel = 3,
                    DaysToManufacture = 4,
                    FinishedGoodsFlag = true,
                    ModifiedDate = new DateTime(1990, 03, 29),
                    MakeFlag = false,
                    ReorderPoint = 77,
                    StandardCost = 5,
                });

                repo.UpdateProduct(new Product { ProductId = 1, Name = "Candace" });
                Console.ReadLine();
            }
            
        }

       
    }
}
