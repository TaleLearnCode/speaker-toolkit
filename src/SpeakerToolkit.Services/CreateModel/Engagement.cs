namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void Engagement(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Engagement>(entity =>
		{
			entity.HasKey(e => e.EngagementId).HasName("pkcEngagement");

			entity.ToTable("Engagement", tb => tb.HasComment("Represents an event that the speaker participates in."));

			entity.HasIndex(e => e.Permalink, "unqEngagement_Permalink").IsUnique();

			entity.Property(e => e.EngagementId).HasComment("The identifier of the engagement record.");
			entity.Property(e => e.EndDate).HasComment("The end date of the event.");
			entity.Property(e => e.EndingCost)
							.HasMaxLength(20)
							.HasComment("The ending cost for the event.");
			entity.Property(e => e.EngagementDescription)
							.HasMaxLength(2000)
							.HasComment("The full description of the event.");
			entity.Property(e => e.EngagementName)
							.IsRequired()
							.HasMaxLength(200)
							.HasComment("The name of the engagement.");
			entity.Property(e => e.EngagementStatusId).HasComment("Identifier of the associated engagement status.");
			entity.Property(e => e.EngagementSummary)
							.HasMaxLength(140)
							.HasComment("The summary description of the event.");
			entity.Property(e => e.EngagementTypeId).HasComment("Identifier of the associated engagement type.");
			entity.Property(e => e.EngagementUrl).HasMaxLength(200);
			entity.Property(e => e.ListingLocation)
							.IsRequired()
							.HasMaxLength(100)
							.HasComment("The location of the event to show on the event listing.");
			entity.Property(e => e.OverviewLocation)
							.HasMaxLength(300)
							.HasComment("The location of the event to show on the overview.");
			entity.Property(e => e.Permalink)
							.IsRequired()
							.HasMaxLength(200);
			entity.Property(e => e.StartDate).HasComment("The start date of the event.");
			entity.Property(e => e.StartingCost)
							.HasMaxLength(20)
							.HasComment("The starting cost for the event.");

			entity.HasOne(d => d.EngagementStatus).WithMany(p => p.Engagements)
							.HasForeignKey(d => d.EngagementStatusId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkEngagement_EngagementStatus");

			entity.HasOne(d => d.EngagementType).WithMany(p => p.Engagements)
							.HasForeignKey(d => d.EngagementTypeId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkEngagement_EngagementType");
		});

	}
}