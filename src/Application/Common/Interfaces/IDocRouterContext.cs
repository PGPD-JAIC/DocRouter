using DocRouter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Common.Interfaces
{
    /// <summary>
    /// Interface that defines the contract for the DocRouterContext used in the Application Layer
    /// </summary>
    public interface IDocRouterContext
    {
        DbSet<Submission> Submissions { get; set; }
        DbSet<SubmissionItem> Items { get; set; }
        DbSet<SubmissionTransaction> Transactions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}
