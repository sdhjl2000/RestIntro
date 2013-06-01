using System;
using System.Collections.Generic;
using NUnit.Framework;
using RestIntro.ServiceModel;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;

namespace RestIntro.IntegrationTests
{
	[TestFixture]
	public class IntegrationTests
	{
	    [Test]
	    public void Test_CRUD_REST_methods()
	    {
	        var restClient = (IRestClient) new JsonServiceClient("http://localhost:5000");
            var newCustomer = restClient.Post<Customer>("/customers", new Customer { Name = "test", Age = 1, Email = "as@gmail.com" });
	        var newproduct = restClient.Post<Product>("/products",new Product { Name = "test",Place = "China",Year = DateTime.UtcNow});
            var neworder=new Order(){ CustomerId = newCustomer.Id, ProductId= newproduct.Id};
            var saveorder = restClient.Post<Customer>("/orders", neworder);

            
	        Assert.That(newCustomer.Id, Is.AtLeast(1));

	    }

	    [Test]
	    public void Test_Query_REST_methods()
	    {

	    }

	    [Test]
		public void Test_all_REST_methods()
		{
			var restClient = (IRestClient)new JsonServiceClient("http://localhost:5000");
			var allCustomers = restClient.Get<List<Customer>>("/customers");
			Assert.That(allCustomers.Count, Is.EqualTo(0));

			var newCustomer = restClient.Post<Customer>("/customers",
				new Customer { Name = "test", Age = 1, Email = "as@if.com" });

			Assert.That(newCustomer.Id, Is.EqualTo(1));
			Assert.That(newCustomer.Name, Is.EqualTo("test"));

			allCustomers = restClient.Get<List<Customer>>("/customers");
			Assert.That(allCustomers.Count, Is.EqualTo(1));


			var singleCustomer = restClient.Get<Customer>("/customers/1");
			Assert.That(singleCustomer.Name, Is.EqualTo("test"));

			singleCustomer.Name = "Update Name";
			restClient.Put<Customer>("/customers/1", singleCustomer);

			singleCustomer = restClient.Get<Customer>("/customers/1");
			Assert.That(singleCustomer.Name, Is.EqualTo("Update Name"));


			restClient.Delete<Customer>("/customers/1");

			allCustomers = restClient.Get<List<Customer>>("/customers");
			Assert.That(allCustomers.Count, Is.EqualTo(0));
		}
	}

}

