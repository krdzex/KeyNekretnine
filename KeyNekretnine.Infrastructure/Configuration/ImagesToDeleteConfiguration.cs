using KeyNekretnine.Infrastructure.BackgroundJobs.ImageDeleter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
internal sealed class ImagesToDeleteConfiguration : IEntityTypeConfiguration<ImageToDelete>
{
    public void Configure(EntityTypeBuilder<ImageToDelete> builder)
    {
        builder.ToTable("images_to_delete");

        builder.HasKey(imagesToDelete => imagesToDelete.Id);
    }
}