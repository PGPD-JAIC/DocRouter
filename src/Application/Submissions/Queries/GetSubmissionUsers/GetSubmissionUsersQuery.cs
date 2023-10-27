using DocRouter.Application.Common.Models;
using MediatR;
using System.Collections.Generic;

namespace DocRouter.Application.Submissions.Queries.GetSubmissionUsers
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> that retrieves users associated with a <see cref="Domain.Entities.Submission"/>
    /// </summary>
    public class GetSubmissionUsersQuery : IRequest<List<DirectoryUser>>
    {
        /// <summary>
        /// The Id of the desired submission.
        /// </summary>
        public int TransactionId { get; set; }
    }
}
