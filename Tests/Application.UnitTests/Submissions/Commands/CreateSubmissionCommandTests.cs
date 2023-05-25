using DocRouter.Application.Common.Models;
using DocRouter.Application.Submissions.Commands.CreateSubmission;
using DocRouter.Application.UnitTests.Common;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DocRouter.Application.UnitTests.Submissions.Commands
{
    public class CreateSubmissionCommandTests : CommandTestBase
    {
        private readonly CreateSubmissionCommandHandler _sut;
        private readonly Mock<IMediator> _mediatorMock;
        public CreateSubmissionCommandTests(): base()
        {
            _mediatorMock = new Mock<IMediator>();
            _sut = new CreateSubmissionCommandHandler(_context, _fileService, _dateTime, new NullLogger<CreateSubmissionCommandHandler>(), _mediatorMock.Object);
        }
        [Fact]
        public async Task Handle_Given_Valid_Request_Should_Raise_SubmissionCreatedNotification()
        {
            // Act
            var result = await _sut.Handle(
                new CreateSubmissionCommand() { 
                    Title = "Test Submission", 
                    Recipient = "test@mail.com",
                    Comments = "These are test comments.",
                    Files = new List<FileSubmissionDto>()
                    //{
                    //    new FileSubmissionDto
                    //    {
                    //        ContentType = "application/pdf",
                    //        FileName = "Test Name"
                    //    }
                    //}
                },
                CancellationToken.None
                );

            // Assert
            _mediatorMock.Verify(m => m.Publish(It.Is<SubmissionCreated>(cc => cc.SubmissionId == 4), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
