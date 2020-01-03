using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using Redirector.Models;

namespace Redirector
{
    public class FactoryManager
    {
        public ISessionFactory Instance { get; }
        public FactoryManager(IConfiguration configuration)
        {
            var connectionString = configuration["CONNSTR_REDIRECTOR"];
            
            Instance = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
                .Mappings(
                    m => m.AutoMappings.Add(
                        AutoMap.AssemblyOf<Redirection>().Where(t => t.Namespace == "Redirector.Models")))
                .BuildSessionFactory();
        }
    }
}