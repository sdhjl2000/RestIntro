using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;

namespace RestIntro.ServiceModel
{
    [Route("/orders")]
    [Route("/orders/{Id}")]
    public class Order
    {
        [AutoIncrement] 
        public int Id { get; set; }
        [References(typeof(Customer))] 
        public int CustomerId { get; set; }
        [References(typeof(Product))] 
        public int ProductId { get; set; }
    }
    
}