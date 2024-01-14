namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void Tag(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Tag>(entity =>
		{
			entity.HasKey(e => e.TagId).HasName("pkcTag");

			entity.ToTable("Tag", tb => tb.HasComment("Represents a label attached to a presentation."));

			entity.Property(e => e.TagId).HasComment("The identifier of the tag record.");
			entity.Property(e => e.TagName)
							.IsRequired()
							.HasMaxLength(100)
							.HasComment("The name of the tag.");
		});
	}
}