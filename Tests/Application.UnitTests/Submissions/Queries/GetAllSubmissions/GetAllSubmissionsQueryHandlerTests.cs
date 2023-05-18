using AutoMapper;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Submissions.Queries.GetAllSubmissions;
using DocRouter.Application.UnitTests.Common;
using DocRouter.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DocRouter.Application.UnitTests.Submissions.Queries.GetAllSubmissions
{
    [Collection("QueryCollection")]
    public class GetAllSubmissionsQueryHandlerTests
    {
        private readonly IDocRouterContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;

        public GetAllSubmissionsQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _dateTime = fixture.DateTime;
        }
        [Fact]
        public async Task Handle_GetAllSubmissions_With_Default_Parameters_Returns_List()
        {
            // Arrange
            var sut = new GetAllSubmissionsQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetAllSubmissionsQuery(), CancellationToken.None);

            // Assert
            result.ShouldBeOfType<SubmissionListVm>();
            result.Submissions.Count.ShouldBe(3);
            result.RoutedToSearch.ShouldBeEmpty();
            result.SubmittedBySearch.ShouldBeEmpty();
            result.DateSort.ShouldBe("date_asc");
        }
    }
}
