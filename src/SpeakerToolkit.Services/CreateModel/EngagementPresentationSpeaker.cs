namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void EngagementPresentationSpeaker(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<EngagementPresentationSpeaker>(entity =>
		{
			entity.HasKey(e => e.EngagementPresentationSpeakerId).HasName("pkcEngagementPresentationSpeaker");

			entity.ToTable("EngagementPresentationSpeaker", tb => tb.HasComment("Represents a speaker presenting an engagement presentation."));

			entity.Property(e => e.EngagementPresentationSpeakerId).HasComment("Identifier of the EngagementPresentationSpeaker record.");
			entity.Property(e => e.EngagementPresentationId).HasComment("Identifier of the engagement presentation.");
			entity.Property(e => e.IsPrimarySpeaker).HasComment("Flag indicating whether the speaker is the primary speaker for the engagement presentation.");
			entity.Property(e => e.SpeakerId).HasComment("Identifier of the assigned speaker.");

			entity.HasOne(d => d.EngagementPresentation).WithMany(p => p.EngagementPresentationSpeakers)
							.HasForeignKey(d => d.EngagementPresentationId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkEngagementPresentationSpeaker_EngagementPresentation");

			entity.HasOne(d => d.Speaker).WithMany(p => p.EngagementPresentationSpeakers)
							.HasForeignKey(d => d.SpeakerId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkEngagementPresentationSpeaker_Speaker");
		});
	}
}