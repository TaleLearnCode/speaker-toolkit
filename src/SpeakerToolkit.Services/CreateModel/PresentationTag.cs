namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void PresentationTag(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<PresentationTag>(entity =>
		{
			entity.HasKey(e => e.PresentationTagId).HasName("pkcPresentationTag");

			entity.ToTable("PresentationTag", tb => tb.HasComment("Represents the association between a presentation and a tag."));

			entity.HasIndex(e => new { e.PresentationId, e.TagId }, "unqPresentationTag_PresentationId_TagId").IsUnique();

			entity.Property(e => e.PresentationTagId).HasComment("The identifier of the presentation/tag record.");
			entity.Property(e => e.PresentationId).HasComment("Identifier of the associated presentation.");
			entity.Property(e => e.TagId).HasComment("Identifier of the associated tag.");

			entity.HasOne(d => d.Presentation).WithMany(p => p.PresentationTags)
							.HasForeignKey(d => d.PresentationId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkPresentationTag_Presentation");

			entity.HasOne(d => d.Tag).WithMany(p => p.PresentationTags)
							.HasForeignKey(d => d.TagId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkPresentationTag_Tag");
		});
	}
}