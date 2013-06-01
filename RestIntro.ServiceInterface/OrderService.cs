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
    public class OrderService : Service
    {
        public object Get(Order request)
        {
            if (request.Id != default(long))
                return Db.GetById<Order>(request.Id);

            return Db.Select<Order>();
        }

        public object Post(Order order)
        {
            Db.Save(order);
            
            order.Id = (int)Db.GetLastInsertId();

            var pathToNewResource = base.RequestContext.AbsoluteUri.CombineWith(order.Id.ToString());
            return HttpResult.Status201Created(order, pathToNewResource);
        }

        public Order Put(Order order)
        {
            Db.Save(order);
            return order;
        }

        public void Delete(Order request)
        {
            Db.DeleteById<Order>(request.Id);
        }
    }
}