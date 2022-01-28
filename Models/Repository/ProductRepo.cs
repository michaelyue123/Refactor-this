using System;
using System.Net;
using System.Web.Http;

namespace RefactorThis.Models.Repository
{
    public class ProductRepo
    {
        readonly Product _product = new();
        readonly ProductOptionRepo _productOptionRepo = new();

        public Product Get(Guid id)
        {
            _product.IsNew = true;
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"select * from Products where id = '{id}' collate nocase";

            var rdr = cmd.ExecuteReader();
            if (!rdr.Read())
                return null;

            _product.IsNew = false;
            _product.Id = Guid.Parse(rdr["Id"].ToString());
            _product.Name = rdr["Name"].ToString();
            _product.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();
            _product.Price = decimal.Parse(rdr["Price"].ToString());
            _product.DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString());

            return _product;
        }

        public void Save(Product product)
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = product.IsNew
                ? $"insert into Products (id, name, description, price, deliveryprice) values ('{product.Id}', '{product.Name}', '{product.Description}', {product.Price}, {product.DeliveryPrice})"
                : $"update Products set name = '{product.Name}', description = '{product.Description}', price = {product.Price}, deliveryprice = {product.DeliveryPrice} where id = '{product.Id}' collate nocase";

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(Guid id)
        {
            ProductOption option = _productOptionRepo.Get(id);
            if(option == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // delete associated product option
            _productOptionRepo.Delete(id);

            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = $"delete from Products where id = '{id}' collate nocase";
            cmd.ExecuteNonQuery();
        }
    }
}
