using Neo4j.Driver;

namespace Journalist.Crm.Neo4j
{
    public abstract class RepositoryBase
    {
        protected readonly IDriver _driver;

        protected RepositoryBase(IDriver driver)
        {
            _driver = driver;
        }


        protected static void WithDatabase(SessionConfigBuilder sessionConfigBuilder)
            => sessionConfigBuilder.WithDatabase(Constants.DatabaseName);
    }
}
