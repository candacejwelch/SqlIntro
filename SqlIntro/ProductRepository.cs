using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// Reads all the products from the products table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {

                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select Name, ModifiedDate from product where ModifiedDate > '2000-10-19' and '2017-10-20';"; //TODO:  Write a SELECT statement that gets all products
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product
                    {
                        Name = dr["Name"].ToString(),
                        Date = (DateTime)dr["ModifiedDate"]
                    };
                }
            }
        }

        /// <summary>
        /// Deletes a Product from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "delete from product where ProductID = @id"; 
                cmd.AddParam("@id", id);
                cmd.ExecuteNonQuery();
                
            }
        }
        /// <summary>
        /// Updates the Product in the database
        /// </summary>
        /// <param name="prod"></param>
        public void UpdateProduct(Product prod)
        {
            //This is annoying and unnecessarily tedious for large objects.
            //More on this in the future...  Nothing to do here..
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "update product set name = @name where ProductID = @id";
                cmd.Parameters.AddWithValue("@name", prod.Name);
                cmd.Parameters.AddWithValue("@id", prod.Id);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Inserts a new Product into the database
        /// </summary>
        /// <param name="prod"></param>
        public void InsertProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT into product (name, SellEndDate, ProductNumber, MakeFlag, " +
                                  "FinishedGoodsFlag, Color, SafetyStockLevel, ReorderPoint, DaysToManufacture," +
                                  "StandardCost, ModifiedDate, SellStartDate, ListPrice, RowGuid) values(@name, @SellEndDate, @ProductNumber, @MakeFlag," +
                                  "@FinishedGoodsFlag, @Color, @SafetyStockLevel, @ReorderPoint, @DaysToManufacture, @StandardCost," +
                                  "@ModifiedDate, @SellStartDate, @ListPrice, @RowGuid)";
                cmd.Parameters.AddWithValue("@name", prod.Name);
                cmd.Parameters.AddWithValue("@SellEndDate", prod.Date);
                cmd.Parameters.AddWithValue("@ProductNumber", prod.ProductNumber);
                cmd.Parameters.AddWithValue("@MakeFlag", prod.MakeFlag);
                cmd.Parameters.AddWithValue("@FinishedGoodsFlag", prod.FinishedGoodsFlag);
                cmd.Parameters.AddWithValue("@Color", prod.Color);
                cmd.Parameters.AddWithValue("@SafetyStockLevel", prod.SafetyStockLevel);
                cmd.Parameters.AddWithValue("@ReorderPoint", prod.ReorderPoint);
                cmd.Parameters.AddWithValue("@DaysToManufacture", prod.DaysToManufacture);
                cmd.Parameters.AddWithValue("@StandardCost", prod.StandardCost);
                cmd.Parameters.AddWithValue("@ModifiedDate", prod.ModifiedDate);
                cmd.Parameters.AddWithValue("@SellStartDate", prod.SellStartDate);
                cmd.Parameters.AddWithValue("@ListPrice", prod.ListPrice);
                cmd.Parameters.AddWithValue("@RowGuid", prod.ListPrice);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
