using System;
using Funq;
using RestIntro.ServiceInterface;
using RestIntro.ServiceModel;
using ServiceStack.Common.Utils;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.OrmLite.Sqlite;
using ServiceStack.WebHost.Endpoints;

namespace RestIntro
{
    /// <summary>
    /// Create your ServiceStack web service application with a singleton AppHost.
    /// </summary> 
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
        /// </summary>
        public AppHost() : base("REST Intro", typeof(CustomerService).Assembly) { }

        /// <summary>
        /// Configure the container with the necessary routes for your ServiceStack application.
        /// </summary>
        /// <param name="container">The built-in IoC used with ServiceStack.</param>
        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(new OrmLiteConnectionFactory(@"Data Source=.\SQLEXPRESS;DataBase=estore;uid=sa;pwd=apsp12345",SqlServerOrmLiteDialectProvider.Instance));
            var conn = container.Resolve<IDbConnectionFactory>();

            conn.Run(dbCmd => dbCmd.DropTable<Order>());
            conn.Run(dbCmd => dbCmd.DropTable<Customer>());
            conn.Run(dbCmd => dbCmd.DropTable<Product>());

            conn.Run(dbCmd => dbCmd.CreateTable<Customer>(true));
            conn.Run(dbCmd => dbCmd.CreateTable<Product>(true));
            conn.Run(dbCmd => dbCmd.CreateTable<Order>(true));

        }
    }

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //Initialize your application
            (new AppHost()).Init();
        }
    }
}