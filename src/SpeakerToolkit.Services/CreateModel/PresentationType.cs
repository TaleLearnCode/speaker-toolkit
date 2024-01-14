namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void PresentationType(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<PresentationType>(entity =>
		{
			entity.HasKey(e => e.PresentationTypeId).HasName("pkcPresentationType");

			entity.ToTable("PresentationType", tb => tb.HasComment("Represents a type of a presentation."));

			entity.Property(e => e.PresentationTypeId).HasComment("The identifier of the presentation type record.");
			entity.Property(e => e.PresentationTypeName)
							.IsRequired()
							.HasMaxLength(100)
							.HasComment("The name of the presentation type.");
			entity.Property(e => e.SortOrder).HasComment("The sorting order of the presentation type.");
			entity.Property(e => e.TypeDescription)
							.HasMaxLength(500)
							.HasComment("A description of the presentation type.");
		});
	}
}
