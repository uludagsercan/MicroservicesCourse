
using BuildingBlocks.Exceptions;

namespace Microservice.Basket.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string userName):base("Basket",userName)
        {

        }
       
    }
}