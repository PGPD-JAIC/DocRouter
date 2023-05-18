using AutoMapper;
using Castle.Core.Logging;
using DocRouter.Application.Common.Mappings;
using DocRouter.Common;
using DocRouter.Persistence;
using System;
using Xunit;

namespace DocRouter.Application.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public DocRouterContext Context { get; private set; }
        public IMapper Mapper { get; private set; }
        public IDateTime DateTime { get; private set; }

        public QueryTestFixture() 
        {
            Context = DocRouterDbContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
            DateTime = new DateTimeTestProvider();
        }
        public void Dispose()
        {
            DocRouterDbContextFactory.Destroy(Context);
        }
    }
    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
