
using Contracts;
using DataManager;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Unity;


namespace Server
{
    class Program
    {

        public static Type Register(string ass1, string ass2)
        {
            // Actor ac = new Actor();
            Type type1 = null;
            Assembly assembly1 = Assembly.Load(ass1);
            Assembly assembly2 = Assembly.Load(ass2);
            Assembly[] assemblies = { assembly1, assembly2 };

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetExportedTypes();

                if (!(assembly.GetExportedTypes().Length > 10))
                {
                    foreach (var type in types)
                    {
                        var s = type.GetConstructors();
                        if (type.BaseType != null)
                        {
                            //HIS IS A TESTttttttttttttt
                            var a = type.BaseType.Name;
                            if (type.BaseType.Name == "ApiController")
                            {
                                type1 = type.BaseType;
                                Console.WriteLine("YES");
                                var x = type.GetConstructors();
                                
                                
                            }
                        }
                    }
                }
            }
            return type1;
        }
        static UnityContainer container = new UnityContainer();
        static void IocContainer() {
            container.RegisterType<ILogic, Logic>();
            
        }
        static void Main(string[] args)
        {
            IocContainer();
            Register("DataManagerServices2","Contracts");

            var config = new HttpSelfHostConfiguration("http://localhost:1234");
                config.Routes.MapHttpRoute("default",
                                            "{controller}/{action}/{id}",
                                            new { controller = "Home", id = RouteParameter.Optional });


                var server = new HttpSelfHostServer(config);
                var task = server.OpenAsync();
                task.Wait();
            
          var s =  config.Services.GetServices(Register("DataManagerServices2", "Contracts"));
            foreach (var t in s) {
                Console.WriteLine(t);
            }
                Console.WriteLine("Web API Server has started at http://localhost:1234");
                Console.ReadLine();
            }
        }
    }


