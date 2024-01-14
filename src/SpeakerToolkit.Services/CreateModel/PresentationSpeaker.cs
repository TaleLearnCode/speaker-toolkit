namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void PresentationSpeaker(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<PresentationSpeaker>(entity =>
		{
			entity.HasKey(e => e.PresentationSpeakerId).HasName("pkcPresentationSpeaker");

			entity.ToTable("PresentationSpeaker", tb => tb.HasComment("Links a speaker to a presentation."));

			entity.HasIndex(e => new { e.PresentationId, e.SpeakerId }, "unqPresentationSpeaker_PresentationId_SpeakerId").IsUnique();

			entity.Property(e => e.PresentationSpeakerId).HasComment("The identifier of the presentation speaker record.");
			entity.Property(e => e.IsPrimary).HasComment("Flag indicaitng whether the speaker is the primary speaker for the presentation.");
			entity.Property(e => e.PresentationId).HasComment("Identifier of the associated presentation.");
			entity.Property(e => e.SpeakerId).HasComment("Identifier of the associated speaker.");

			entity.HasOne(d => d.Presentation).WithMany(p => p.PresentationSpeakers)
							.HasForeignKey(d => d.PresentationId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkPresentationSpeaker_Presentation");

			entity.HasOne(d => d.Speaker).WithMany(p => p.PresentationSpeakers)
							.HasForeignKey(d => d.SpeakerId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkPresentationSpeaker_Speaker");
		});
	}
}