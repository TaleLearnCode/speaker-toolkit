namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void SpeakerAward(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<SpeakerAward>(entity =>
		{
			entity.HasKey(e => e.SpeakerAwardId).HasName("pkcSpeakerAward");

			entity.ToTable("SpeakerAward", tb => tb.HasComment("Represents an award bestowed to a speaker."));

			entity.HasIndex(e => new { e.SpeakerId, e.SpeakerAwardTypeId, e.AwardCategory, e.AwardYear }, "uqcSpeakerAward_SpeakerId_SpeakerAwardTypeId_AwardCategory_AwardYear").IsUnique();

			entity.Property(e => e.SpeakerAwardId).HasComment("The identifier of the speaker award record.");
			entity.Property(e => e.AwardCategory)
							.HasMaxLength(100)
							.HasComment("The identifier of the speaker award record.");
			entity.Property(e => e.AwardProfileUrl)
							.IsRequired()
							.HasMaxLength(200)
							.IsUnicode(false);
			entity.Property(e => e.AwardYear).HasComment("The identifier of the speaker award record.");
			entity.Property(e => e.SpeakerAwardTypeId).HasComment("The identifier of the speaker award record.");
			entity.Property(e => e.SpeakerId).HasComment("The identifier of the speaker award record.");

			entity.HasOne(d => d.SpeakerAwardType).WithMany(p => p.SpeakerAwards)
							.HasForeignKey(d => d.SpeakerAwardTypeId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkcSpeakerAward_SpeakerAwardTypeId");

			entity.HasOne(d => d.Speaker).WithMany(p => p.SpeakerAwards)
							.HasForeignKey(d => d.SpeakerId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkcSpeakerAward_SpeakerId");
		});
	}
}