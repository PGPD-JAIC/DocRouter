using DocRouter.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocRouter.Persistence.Configurations
{
    public class SubmissionItemConfiguration : BaseEntityConfiguration<SubmissionItem>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">An instance of <see cref="EntityTypeBuilder{SubmissionItem}"/></param>
        public override void Configure(EntityTypeBuilder<SubmissionItem> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.ItemName)
                .HasField("_itemName")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.SubmissionId)
                .HasField("_submissionId")
                .IsRequired();
            builder.Property(x => x.ItemUri)
                .HasField("_itemUri")
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}
