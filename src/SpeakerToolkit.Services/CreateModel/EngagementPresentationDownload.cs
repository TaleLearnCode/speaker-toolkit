namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void EngagementPresentationDownload(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<EngagementPresentationDownload>(entity =>
		{
			entity.HasKey(e => e.EngagementPresentationDownloadId).HasName("pkcEngagementPresentationDownload");

			entity.ToTable("EngagementPresentationDownload", tb => tb.HasComment("Represents a download associated with a engagement presentation."));

			entity.Property(e => e.EngagementPresentationDownloadId).HasComment("Identifier of the EngagementPresentationDownload record.");
			entity.Property(e => e.DownloadName)
							.IsRequired()
							.HasMaxLength(50);
			entity.Property(e => e.DownloadUrl)
							.HasMaxLength(500)
							.HasComment("The link to the download.");
			entity.Property(e => e.EngagementPresentationId).HasComment("Identifier of the associated engagement presentation.");

			entity.HasOne(d => d.EngagementPresentation).WithMany(p => p.EngagementPresentationDownloads)
							.HasForeignKey(d => d.EngagementPresentationId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkEngagementPresentationDownload_EngagementPresentation");
		});
	}
}