using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    public class DapperDb
    {
        private readonly string _connectionString;

        public DapperDb(string connectionString)
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
                return conn.Query<Product>("select * from product");
            }
        }

        /// <summary>
        /// Deletes a Product from the database
        /// </summary>
        /// <param name="productId"></param>
        public void DeleteProduct(int productId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("delete from product where ProductID = @id", new {id = productId});
            }
        }

        /// <summary>
        /// Updates the Product in the database
        /// </summary>
        /// <param name="prod"></param>
        public void UpdateProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("update product set name = @name where ProductID = @id",
                    new {id = prod.ProductId, name = prod.Name});
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
                conn.Execute("INSERT into product (Name, ProductNumber, MakeFlag, " +
                             "FinishedGoodsFlag, Color, SafetyStockLevel, ReorderPoint, DaysToManufacture," +
                             "StandardCost, ModifiedDate, SellStartDate, ListPrice, RowGuid) values(@Name, @ProductNumber, @MakeFlag," +
                             "@FinishedGoodsFlag, @Color, @SafetyStockLevel, @ReorderPoint, @DaysToManufacture, @StandardCost," +
                             "@ModifiedDate, @SellStartDate, @ListPrice, @RowGuid)", prod);
            }
        }
    }
}
