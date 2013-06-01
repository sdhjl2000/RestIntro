using RestIntro.ServiceModel;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;

namespace RestIntro.ServiceInterface
{
    /// <summary>
    /// Create your ServiceStack rest-ful web service implementation. 
    /// </summary>
    public class ProductService : Service
    {
        public object Get(Product request)
        {
            if (request.Id != default(long))
                return Db.GetById<Product>(request.Id);

            return Db.Select<Product>();
        }

        public object Post(Product product)
        {
            Db.Save(product);
            product.Id = (int)Db.GetLastInsertId();

            var pathToNewResource = base.RequestContext.AbsoluteUri.CombineWith(product.Id.ToString());
            return HttpResult.Status201Created(product, pathToNewResource);
        }

        public Product Put(Product product)
        {
            Db.Save(product);
            return product;
        }

        public void Delete(Product request)
        {
            Db.DeleteById<Product>(request.Id);
        }
    }
}