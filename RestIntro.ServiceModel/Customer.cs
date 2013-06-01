using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;

namespace RestIntro.ServiceModel
{
    [Route("/customers")]
    [Route("/customers/{Id}")]
    public class Customer
    {
        [AutoIncrement] //OrmLite hint
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
    
}