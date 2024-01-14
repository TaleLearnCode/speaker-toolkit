namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void LearningObjective(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<LearningObjective>(entity =>
		{
			entity.HasKey(e => e.LearningObjectiveId).HasName("pkcLearningObjective");

			entity.ToTable("LearningObjective", tb => tb.HasComment("Represents a learning objective of a presentation."));

			entity.Property(e => e.LearningObjectiveId).HasComment("The identifier of the learning objective record.");
			entity.Property(e => e.LearningObjectiveText)
							.IsRequired()
							.HasMaxLength(300)
							.HasComment("The text of the learning objective.");
			entity.Property(e => e.PresentationTextId).HasComment("The identifier of the associated presentation (text) record.");
			entity.Property(e => e.SortOrder).HasComment("The sorting order of the learning objective.");

			entity.HasOne(d => d.PresentationText).WithMany(p => p.LearningObjectives)
							.HasForeignKey(d => d.PresentationTextId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkLearningObjective_PresentationText");
		});
	}
}