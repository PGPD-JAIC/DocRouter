using DocRouter.Application.Common.Interfaces;
using DocRouter.Common;
using DocRouter.Domain.Common;
using DocRouter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Persistence
{
    public class DocRouterContext : DbContext, IDocRouterContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        public DocRouterContext(DbContextOptions<DocRouterContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// Creates a new instance of the <see cref="DocRouterContext"></see>
        /// </summary>
        /// <param name="options">An implementation of <see cref="DbContextOptions{DocRouterContext}"/></param>
        /// <param name="currentUserService">An implementation of <see cref="ICurrentUserService"/></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"/></param>
        public DocRouterContext(
            DbContextOptions<DocRouterContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }
        
        public virtual DbSet<Submission> Submissions { get; set; }
        public virtual DbSet<SubmissionItem> Items { get; set; }
        public virtual DbSet<SubmissionTransaction> Transactions { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.UpdateDateCreated(_dateTime.Now);
                        entry.Entity.UpdateCreatedBy(_currentUserService.UserId);
                        entry.Entity.UpdateDateEdited(_dateTime.Now);
                        entry.Entity.UpdateEditedBy(_currentUserService.UserId);
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateDateEdited(_dateTime.Now);
                        entry.Entity.UpdateEditedBy(_currentUserService.UserId);
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.UpdateDateCreated(_dateTime.Now);
                        entry.Entity.UpdateCreatedBy(_currentUserService.UserId);
                        entry.Entity.UpdateDateEdited(_dateTime.Now);
                        entry.Entity.UpdateEditedBy(_currentUserService.UserId);
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateDateEdited(_dateTime.Now);
                        entry.Entity.UpdateEditedBy(_currentUserService.UserId);
                        break;
                }
            }
            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocRouterContext).Assembly);

            // TODO: Seed Data here

            //modelBuilder.Entity<LocationType>().HasData(
            //    new { Id = 59, Name = "Street/Hwy (Non-Residential)", CreatedBy = "Admin", Created = DateTime.MinValue, EditedBy = "Admin", Edited = DateTime.MinValue },
            //    new { Id = 60, Name = "Rideshare (Uber/Lyft, etc)", CreatedBy = "Admin", Created = DateTime.MinValue, EditedBy = "Admin", Edited = DateTime.MinValue }
            //    );
        }
    }
}
