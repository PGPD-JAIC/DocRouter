using AutoMapper;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Submissions;
using DocRouter.Application.Submissions.Queries.GetSubmissionDetail;
using DocRouter.Application.UnitTests.Common;
using DocRouter.Common;
using Microsoft.Extensions.Logging.Abstractions;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DocRouter.Application.UnitTests.Submissions.Queries.GetSubmissionDetail
{
    [Collection("QueryCollection")]
    public class GetSubmissionDetailQueryHandlerTests
    {
        private readonly IDocRouterContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;

        public GetSubmissionDetailQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _dateTime = fixture.DateTime;
        }
        [Fact]
        public async Task Handle_Given_Valid_Id_Returns_SubmissionDetails()
        {
            // Arrange
            var sut = new GetSubmissionDetailQueryHandler(_context, new NullLogger<GetSubmissionDetailQueryHandler>(), _mapper);

            // Act
            var result = await sut.Handle(new GetSubmissionDetailQuery { Id = 3 }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<SubmissionDto>();
            result.Title.ShouldBe("Test Submission #3");
            result.Items.Count.ShouldBe(2);
            result.Transactions.Count.ShouldBe(2);
        }
    }
}
