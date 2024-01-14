namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void EngagementStatus(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<EngagementStatus>(entity =>
		{
			entity.HasKey(e => e.EngagementStatusId).HasName("pkcEngagementStatus");

			entity.ToTable("EngagementStatus", tb => tb.HasComment("Represents a status of a engagement."));

			entity.Property(e => e.EngagementStatusId).HasComment("The identifier of the engagement status record.");
			entity.Property(e => e.EngagementStatusName)
							.IsRequired()
							.HasMaxLength(100)
							.HasComment("The name of the engagement status.");
		});
	}
}