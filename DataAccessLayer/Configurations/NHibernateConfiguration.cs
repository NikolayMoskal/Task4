using System.Configuration;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using NLog;
using Configuration = NHibernate.Cfg.Configuration;

namespace DataAccessLayer.Configurations
{
    public static class NHibernateConfiguration
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly ISessionFactory SessionFactory = ConfigureSessionFactory();
        
        private static string GetConnectionString()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count > 0)
                {
                    return appSettings["connectionString"];
                }
                
                Logger.Warn("App settings is empty");
                return null;
            }
            catch (ConfigurationErrorsException e)
            {
                Logger.Error($"Error reading app settings: {e.StackTrace}");
                return null;
            }
        }

        private static ISessionFactory ConfigureSessionFactory()
        {
            var cfg = new Configuration()
                .DataBaseIntegration(db =>
                {
                    db.ConnectionString = GetConnectionString();
                    db.Driver<MySqlDataDriver>();
                    db.Dialect<MySQLDialect>();
                });
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mapping);
            new SchemaUpdate(cfg).Execute(true, true);
            return cfg.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory?.OpenSession();
        }
    }
}