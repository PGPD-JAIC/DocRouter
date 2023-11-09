using DocRouter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocRouter.Persistence.Configurations
{
    public class SubmissionConfiguration : BaseEntityConfiguration<Submission>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">An instance of <see cref="EntityTypeBuilder{Submission}"/></param>
        public override void Configure(EntityTypeBuilder<Submission> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Title)
                .HasField("_title")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Description)
                .HasField("_description")
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(x => x.FolderUri)
                .HasField("_folderUri")
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(x => x.DriveId)
                .HasField("_driveId")
                .HasMaxLength(100);
            builder.Property(x => x.ItemId)
                .HasField("_itemId")
                .HasMaxLength(100);
            builder.Property(x => x.ListId)
                .HasField("_listId")
                .HasMaxLength(100);

            var nav1 = builder.Metadata.FindNavigation(nameof(Submission.Items));
            nav1.SetPropertyAccessMode(PropertyAccessMode.Field);
            var nav2 = builder.Metadata.FindNavigation(nameof(Submission.Transactions));
            nav2.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}