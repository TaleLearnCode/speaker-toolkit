namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void EngagementPresentation(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<EngagementPresentation>(entity =>
		{
			entity.HasKey(e => e.EngagementPresentationId).HasName("pkcEngagementPresentation");

			entity.ToTable("EngagementPresentation", tb => tb.HasComment("Represents the speaker''s presentations."));

			entity.Property(e => e.EngagementPresentationId).HasComment("Identifier of the EngagementPresentation record.");
			entity.Property(e => e.EndDateTime).HasComment("The ending date and time for the presentation.");
			entity.Property(e => e.EngagementId).HasComment("Identifier of the associated engagement.");
			entity.Property(e => e.PresentationId).HasComment("Identifier of the associated presentation.");
			entity.Property(e => e.Room)
							.HasMaxLength(50)
							.HasComment("The room where the presentation is being presented.");
			entity.Property(e => e.StartDateTime).HasComment("The starting date and time for the presentation.");
			entity.Property(e => e.TimeZone)
							.HasMaxLength(10)
							.IsUnicode(false);

			entity.HasOne(d => d.Engagement).WithMany(p => p.EngagementPresentations)
							.HasForeignKey(d => d.EngagementId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkEngagementPresentation_Engagement");

			entity.HasOne(d => d.Presentation).WithMany(p => p.EngagementPresentations)
							.HasForeignKey(d => d.PresentationId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkEngagementPresentation_Presentation");

			entity.HasOne(d => d.Status).WithMany(p => p.EngagementPresentations)
							.HasForeignKey(d => d.StatusId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkEngagementPresentation_EngagementStatus");

		});

	}
}