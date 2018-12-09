using System.Configuration;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using NLog;
using Configuration = NHibernate.Cfg.Configuration;

namespace DataAccessLayer.Configurations
{
    public static class NHibernateConfiguration
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static ISession OpenSession()
        {
            var cfg = new Configuration()
                .DataBaseIntegration(db =>
                {
                    db.ConnectionString = GetConnectionString();
                    db.Dialect<MySQLDialect>();
                });
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mapping);
            new SchemaUpdate(cfg).Execute(true, true);
            var sessionFactory = cfg.BuildSessionFactory();
            
            return sessionFactory.OpenSession();
        }

        private static string GetConnectionString()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count > 0)
                {
                    return appSettings["connectionString"] ?? "";
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
    }
}