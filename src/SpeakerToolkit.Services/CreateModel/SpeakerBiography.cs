namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void SpeakerBiography(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<SpeakerBiography>(entity =>
		{
			entity.HasKey(e => e.SpeakerBiographyId).HasName("pkcSpeakerBiography");

			entity.ToTable("SpeakerBiography", tb => tb.HasComment("Contains the title and bio of a speaker in a specified langauge."));

			entity.HasIndex(e => new { e.SpeakerId, e.LanguageCode }, "ucSpeakerBiography_Speaker_Language").IsUnique();

			entity.Property(e => e.SpeakerBiographyId).HasComment("The identifier of the speaker bio record.");
			entity.Property(e => e.Biography)
							.IsRequired()
							.HasMaxLength(4000)
							.HasComment("The biography for the speaker.");
			entity.Property(e => e.LanguageCode)
							.IsRequired()
							.HasMaxLength(2)
							.IsUnicode(false)
							.IsFixedLength()
							.HasComment("Code of the associated language.");
			entity.Property(e => e.SpeakerId).HasComment("The identifier of the speaker.");
			entity.Property(e => e.Title)
							.IsRequired()
							.HasMaxLength(160)
							.HasComment("The title for the speaker.");

			entity.HasOne(d => d.Language).WithMany(p => p.SpeakerBiographies)
							.HasForeignKey(d => d.LanguageCode)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkSpeakerBiography_Language");

			entity.HasOne(d => d.Speaker).WithMany(p => p.SpeakerBiographies)
							.HasForeignKey(d => d.SpeakerId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkSpeakerBiography_Speaker");
		});
	}
}