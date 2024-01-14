namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void PresentationRelated(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<PresentationRelated>(entity =>
		{
			entity.HasKey(e => e.PresentationRelatedId).HasName("pkcPresentationRelated");

			entity.ToTable("PresentationRelated", tb => tb.HasComment("Links two related presentations togethers."));

			entity.HasIndex(e => new { e.PrimaryPresentationId, e.RelatedPresentationId }, "unqPresentationRelated_PresentationId_RelatedPresentationId").IsUnique();

			entity.Property(e => e.PresentationRelatedId).HasComment("The identifier of the releated presentation object.");
			entity.Property(e => e.PrimaryPresentationId).HasComment("The identifier of the primary presentation.");
			entity.Property(e => e.RelatedPresentationId).HasComment("The identifier of the related presentation.");
			entity.Property(e => e.SortOrder).HasComment("The sorting order of the related presentation.");

			entity.HasOne(d => d.PrimaryPresentation).WithMany(p => p.PresentationRelatedPrimaryPresentations)
							.HasForeignKey(d => d.PrimaryPresentationId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkPresentationRelated_PresentationId");

			entity.HasOne(d => d.RelatedPresentation).WithMany(p => p.PresentationRelatedRelatedPresentations)
							.HasForeignKey(d => d.RelatedPresentationId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkPresentationRelated_RelatedPresentationId");
		});
	}
}