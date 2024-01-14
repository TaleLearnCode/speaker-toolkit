namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void EngagementType(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<EngagementType>(entity =>
		{
			entity.HasKey(e => e.EngagementTypeId).HasName("pkcEngagementType");

			entity.ToTable("EngagementType", tb => tb.HasComment("Represents a type of engagement."));

			entity.Property(e => e.EngagementTypeId).HasComment("The identifier of the engagement type record.");
			entity.Property(e => e.EngagementTypeName)
							.IsRequired()
							.HasMaxLength(100)
							.HasComment("The name of the engagement type.");
		});
	}
}