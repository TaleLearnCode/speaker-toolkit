namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void SpeakerAwardType(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<SpeakerAwardType>(entity =>
		{
			entity.HasKey(e => e.SpeakerAwardTypeId).HasName("pkcSpeakerAwardType");

			entity.ToTable("SpeakerAwardType", tb => tb.HasComment("Represents a type of a speaker award."));

			entity.Property(e => e.SpeakerAwardTypeId).HasComment("The identifier of the speaker award type record.");
			entity.Property(e => e.HasAwardYears).HasComment("Flag indicating whether the speaker award has award year.");
			entity.Property(e => e.HasCategories).HasComment("Flag indicating whether the speaker award has categories.");
			entity.Property(e => e.SpeakerAwardTypeName)
							.IsRequired()
							.HasMaxLength(100)
							.HasComment("The name of the speaker award type name.");
		});
	}
}