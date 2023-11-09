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
        /// <summary>
        /// A <see cref="DbSet{Submission}"/> containing submission entities.
        /// </summary>
        DbSet<Submission> Submissions { get; set; }
        /// <summary>
        /// A <see cref="DbSet{SubmissionItem}"/> containing submission item entities.
        /// </summary>
        DbSet<SubmissionItem> Items { get; set; }
        /// <summary>
        /// A <see cref="DbSet{SubmissionTransaction}"/> containing submission transaction entities.
        /// </summary>
        DbSet<SubmissionTransaction> Transactions { get; set; }
        /// <summary>
        /// Saves changes to the context.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A <see cref="int"/> indicating whether the save operation succeeded.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Saves changes to the context.
        /// </summary>
        /// /// <returns>A <see cref="int"/> indicating whether the save operation succeeded.</returns>
        int SaveChanges();
    }
}
