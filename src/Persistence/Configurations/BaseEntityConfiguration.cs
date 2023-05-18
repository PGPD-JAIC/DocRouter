using DocRouter.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocRouter.Persistence.Configurations
{
    /// <summary>
    /// Configuration settings for the <see cref="BaseEntity"/> Entity
    /// </summary>
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">An instance of <see cref="EntityTypeBuilder{BaseEntity}"/></param>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.Id)
                .HasField("_id")
                .IsRequired();
            builder.Property(x => x.Created)
                .HasField("_created")
                .IsRequired();
            builder.Property(x => x.CreatedBy)
                .HasField("_createdBy")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Edited)
                .HasField("_edited");
            builder.Property(x => x.EditedBy)
                .HasField("_editedBy");
        }
    }
}
