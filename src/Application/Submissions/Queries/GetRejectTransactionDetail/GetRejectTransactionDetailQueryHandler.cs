using DocRouter.Application.Submissions.Queries.GetApproveTransactionDetail;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocRouter.Application.Submissions.Queries.GetRejectTransactionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler"/> that handles a request to get a transaction response.
    /// </summary>
    public class GetRejectTransactionDetailQueryHandler : IRequestHandler<GetRejectTransactionDetailQuery, RejectTransactionCommand>
    {
    }
}
