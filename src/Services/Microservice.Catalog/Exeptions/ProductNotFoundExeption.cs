
namespace Microservice.Catalog.Exeptions
{
    public class ProductNotFoundExeption : Exception
    {
        public ProductNotFoundExeption():base("Product Not Found")
        {
        }
    }
}