# Improvements 

## Upgrade Version
Project's .net version is upgraded from 2.2 to version 5.0 as 2.2 is already deprecated and no longer supported. Additionally, the versions of a few
plugins are upgraded as well. 

## Split Giant Model Class
There is one bulky model class (Products), including all api logics. Honestly, it is quite hard to maintain from development perspective. This giant
Products class is split into two smaller model classes: **Product** and **ProductOption**. And then simple model validations added to model classes. Since isNew
attribute is not part of db schema and I am not quite sure why we want to keep it, I decided to remove it at this stage. I am happy for an open
discussion later. :)

## Sqlite Database connection + Create AppDbContext 
I decided to remove helper function and add my own AppDbContext class to establish Sqlite Database connection. Also, I replaced old sql query with linq
query to avoid traditional SQL injection attacks.

## Code Design - Repository Pattern
After a bit of research, I used repository pattern to replace old api logics in Product and ProductOption. By using repo pattern, code is a lot cleaner
and easier to manage as heavy-lifting api logics are managed by different repos instead of model classes, leaving model classes short and clean. Also, each
repo class is extended from its parent interface to make sure api funtions well-structured and organised.

```
interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<IEnumerable<Product>> GetProductsByName(string name);
    Task<Product> GetProduct(Guid id);
    Task<Product> AddProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task<Product> DeleteProduct(Guid id);
}


interface IProductOptionRepository
{
    Task<IEnumerable<ProductOption>> GetProductOptions(Guid productId);
    Task<ProductOption> GetProductOption(Guid productId, Guid optionId);
    Task<ProductOption> AddProductOption(Guid productId, ProductOption option);
    Task<ProductOption> UpdateProductOption(Guid optionId, ProductOption option);
    Task<ProductOption> DeleteProductOption(Guid optionId);
}

ProductRepository:  IProductRepository
ProductOptionRepository : IProductOptionRepository
```

## Split Giant Controller 
It is same as model class. Bulky ProductsController is split into four controllers: **ProductController**, **ProductsController**, **ProductOptionController** and
**ProductOptionsController**. Each controller is responsible for its corresponding api logic. 

## Error Handler for Controllers
ErrorController and HttpResponseException are created to properly handle application errors. In addition, exception handlers are also implemented for each
api function with respect to its corresponding controller. 

## Controller Unit Testing
I wrote unit tests for different controllers (ProductController + ProductOptionController) for its corresponding crud api functions. Although, for some
reasons, I could not pass these unit tests, I still decide to keep it as the evidence.

## Limitation
There is still a lot of space for improvement. I am open to any constructive advice. :) 
