using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyDB
{
    public class SessionFactory
    {
        private static volatile ISessionFactory iSessionFactory;
        private static object syncRoot = new object();

        public static ISession OpenSession
        {
            get
            {
                if (iSessionFactory == null)
                    lock (syncRoot)
                    {
                        if (iSessionFactory == null)
                        {
                            iSessionFactory = BuildSessionFactory();
                        }
                    }

                return iSessionFactory.OpenSession();
            }
        }

        private static ISessionFactory BuildSessionFactory()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["sampleDatabase"].ConnectionString;

                return Fluently.Configure()
                    .Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString))
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Program>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private static AutoPersistenceModel CreateMappings()
        {
            return AutoMap
                .Assemblies(System.Reflection.Assembly.GetCallingAssembly())
                .Where(x => x.Namespace == "StudyDB.Model");
        }

        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {

        }
    }
}
