using System;
using System.Collections.Generic;

namespace RefactorThis.Models.Repository
{
    public class ProductsRepo
    {
        readonly Products products = new();

        public Products Get(string name)
        {
            string where = null;

            if(name != null)
            {
                where = $"where lower(name) like '%{name.ToLower()}%'";
            }

            products.Items = new List<Product>();
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"select id from Products {where}";

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr.GetString(0));
                products.Items.Add(new ProductRepo().Get(id));
            }

            return products;
        }

    }
}
