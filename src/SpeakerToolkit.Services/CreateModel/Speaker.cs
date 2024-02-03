namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void Speaker(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Speaker>(entity =>
		{
			entity.HasKey(e => e.SpeakerId).HasName("pkcSpeaker");

			entity.ToTable("Speaker", tb => tb.HasComment("Details about a speaker."));

			entity.Property(e => e.SpeakerId).HasComment("The identifier of the speaker.");
			entity.Property(e => e.CountryCode)
							.IsRequired()
							.HasMaxLength(2)
							.IsUnicode(false)
							.IsFixedLength()
							.HasComment("Idenfiier of the country where the speaker is located.");
			entity.Property(e => e.CountryDivisionCode)
							.HasMaxLength(3)
							.IsUnicode(false)
							.IsFixedLength()
							.HasComment("Identifier of the country division where the speaker is located.");
			entity.Property(e => e.EnablePublicProfile).HasComment("Flag indicating whether the speaker profile is displayed publicly.");
			entity.Property(e => e.FirstName)
							.IsRequired()
							.HasMaxLength(50)
							.HasComment("The first name of the speaker.");
			entity.Property(e => e.LastName)
							.IsRequired()
							.HasMaxLength(50)
							.HasComment("The last name of the speaker.");
			entity.Property(e => e.PublicProfileUrl)
							.IsRequired()
							.HasMaxLength(100)
							.IsUnicode(false);
			entity.HasOne(d => d.Country).WithMany(p => p.Speakers)
							.HasForeignKey(d => d.CountryCode)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkSpeaker_Country");
			entity.HasOne(d => d.CountryDivision).WithMany(p => p.Speakers)
							.HasForeignKey(d => new { d.CountryCode, d.CountryDivisionCode })
							.HasConstraintName("fkSpeaker_CountryDivision");
			entity.HasOne(d => d.DefaultLanguage).WithMany(p => p.Speakers)
				.HasForeignKey(d => d.DefaultLanguageCode)
				.HasConstraintName("fkSpeaker_Language");
		});
	}
}