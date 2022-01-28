using System;
using System.Collections.Generic;

namespace RefactorThis.Models.Repository
{
    public class ProductOptionsRepo
    {
        readonly ProductOptions _productOptions = new();
       
        public ProductOptions Get(Guid productId)
        {
            string where = null;
            if (productId != Guid.Empty)
            {
                where = $"where productid = '{productId}' collate nocase";
            }

            _productOptions.Items = new List<ProductOption>();
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = $"select id from productoptions {where}";

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr.GetString(0));
                _productOptions.Items.Add(new ProductOptionRepo().Get(id));
            }

            return _productOptions;
        }
    }
}
