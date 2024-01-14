namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void EngagementTag(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<EngagementTag>(entity =>
		{
			entity.HasKey(e => e.EngagementTagId).HasName("pkcEngagementTag");

			entity.ToTable("EngagementTag", tb => tb.HasComment("Represents the association between a engagement and a tag."));

			entity.HasIndex(e => new { e.EngagementId, e.TagId }, "unqEngagementTag_EngagementId_TagId").IsUnique();

			entity.Property(e => e.EngagementTagId).HasComment("The identifier of the engagement/tag record.");
			entity.Property(e => e.EngagementId).HasComment("Identifier of the associated engagement.");
			entity.Property(e => e.TagId).HasComment("Identifier of the associated tag.");

			entity.HasOne(d => d.Engagement).WithMany(p => p.EngagementTags)
							.HasForeignKey(d => d.EngagementId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkEngagementTag_Engagement");

			entity.HasOne(d => d.Tag).WithMany(p => p.EngagementTags)
							.HasForeignKey(d => d.TagId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkEngagementTag_Tag");
		});
	}
}