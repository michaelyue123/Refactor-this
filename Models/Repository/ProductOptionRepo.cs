using System;
namespace RefactorThis.Models.Repository
{
    public class ProductOptionRepo
    {
        readonly ProductOption _productOption = new();

        public ProductOption Get(Guid id)
        {
            _productOption.IsNew = true;
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = $"select * from productoptions where id = '{id}' collate nocase";

            var rdr = cmd.ExecuteReader();
            if (!rdr.Read())
                return null;

            _productOption.IsNew = false;
            _productOption.Id = Guid.Parse(rdr["Id"].ToString());
            _productOption.ProductId = Guid.Parse(rdr["ProductId"].ToString());
            _productOption.Name = rdr["Name"].ToString();
            _productOption.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();

            return _productOption;
        }

        public void Save(ProductOption productOption)
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = _productOption.IsNew
                ? $"insert into productoptions (id, productid, name, description) values ('{productOption.Id}', '{productOption.ProductId}', '{productOption.Name}', '{productOption.Description}')"
                : $"update productoptions set name = '{productOption.Name}', description = '{productOption.Description}' where id = '{productOption.Id}' collate nocase";

            cmd.ExecuteNonQuery();
        }

        public void Delete(Guid id)
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"delete from productoptions where id = '{id}' collate nocase";
            cmd.ExecuteReader();
        }
    }
}
