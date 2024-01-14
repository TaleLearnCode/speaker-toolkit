namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void SpeakerLink(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<SpeakerLink>(entity =>
		{
			entity.HasKey(e => e.SpeakerLinkId).HasName("pkcSpeakerLink");

			entity.ToTable("SpeakerLink", tb => tb.HasComment("Represents a link to a website/social media profile."));

			entity.Property(e => e.SpeakerLinkId).HasComment("The identifier of the link record.");
			entity.Property(e => e.LinkType)
							.IsRequired()
							.HasMaxLength(100)
							.HasComment("The type of link being represented.");
			entity.Property(e => e.LinkUrl)
							.IsRequired()
							.HasMaxLength(200)
							.HasComment("The URL of the link.");
			entity.Property(e => e.SpeakerId).HasComment("The identifier of the associated speaker.");

			entity.HasOne(d => d.Speaker).WithMany(p => p.SpeakerLinks)
							.HasForeignKey(d => d.SpeakerId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkcSpeakerLink_SpeakerId");
		});
	}
}