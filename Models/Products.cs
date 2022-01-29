using System;
using System.Collections.Generic;

namespace RefactorThis.Models
{
    public class Products
    {
        public List<Product> Items { get; private set; }

        public Products()
        {
            LoadProducts(null);
        }

        public Products(string name)
        {
            LoadProducts($"where lower(name) like '%{name.ToLower()}%'");
        }

        private void LoadProducts(string where)
        {
            string sqlCommand = $"select id from Products";

            if (where != null)
            {
                sqlCommand = $"select id from Products {where}";
            }

            Items = new List<Product>();
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sqlCommand;

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr.GetString(0));
                Items.Add(new Product(id));
            }
        }
    }
}