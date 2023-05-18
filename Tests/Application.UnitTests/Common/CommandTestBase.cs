using DocRouter.Persistence;
using System;

namespace DocRouter.Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly DocRouterContext _context;
        protected readonly TestFileService _fileService;
        protected readonly DateTimeTestProvider _dateTime;

        public CommandTestBase()
        {
            _context = DocRouterDbContextFactory.Create();
            _fileService = new TestFileService();
            _dateTime = new DateTimeTestProvider();
        }

        public void Dispose()
        {
            DocRouterDbContextFactory.Destroy(_context);
        }
    }
}
