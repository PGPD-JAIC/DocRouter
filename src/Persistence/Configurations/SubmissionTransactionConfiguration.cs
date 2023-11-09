using DocRouter.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocRouter.Persistence.Configurations
{
    public class SubmissionTransactionConfiguration : BaseEntityConfiguration<SubmissionTransaction>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">An instance of <see cref="EntityTypeBuilder{SubmissionTransaction}"/></param>
        public override void Configure(EntityTypeBuilder<SubmissionTransaction> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.SubmissionId)
                .HasField("_submissionId")
                .IsRequired();
            builder.Property(x => x.SubmitDate)
                .HasField("_submitDate")
                .IsRequired();
            builder.Property(x => x.Status)
                .HasField("_status")
                .IsRequired()
                .HasConversion<string>();
            builder.Property(x => x.ResponseDate)
                .HasField("_responseDate");
            builder.Property(x => x.RoutedTo)
                .HasField("_routedTo")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.RoutedFrom)
                .HasField("_routedFrom")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Comments)
                .HasField("_comments")
                .IsRequired()
                .HasMaxLength(5000);
        }
    }
}
