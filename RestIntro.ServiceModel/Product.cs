using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;

namespace RestIntro.ServiceModel
{
    [Route("/products")]
    [Route("/products/{Id}")]
    public class Product
    {
        [AutoIncrement] 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime Year { get; set; }
    }
}