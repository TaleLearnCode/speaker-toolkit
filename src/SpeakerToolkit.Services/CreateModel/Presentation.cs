namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void Presentation(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Presentation>(entity =>
		{
			entity.HasKey(e => e.PresentationId).HasName("pkcPresentation");

			entity.ToTable("Presentation", tb => tb.HasComment("Represents the speaker''s presentations."));

			entity.HasIndex(e => e.Permalink, "unqPresentation_Permalink").IsUnique();

			entity.Property(e => e.PresentationId).HasComment("The identifier of the presentation record.");
			entity.Property(e => e.IncludeInPublicProfile)
							.IsRequired()
							.HasDefaultValueSql("1")
							.HasComment("Flag indicating whether the presentation is to be include in the public profile.");
			entity.Property(e => e.IsArchived).HasComment("Flag indicating whether the presentation has been archived.");
			entity.Property(e => e.Permalink)
							.IsRequired()
							.HasMaxLength(200)
							.IsUnicode(false);
			entity.Property(e => e.PresentationTypeId).HasComment("Identifier of the type of presentation is represented.");
			entity.Property(e => e.RepoLink)
							.HasMaxLength(200)
							.IsUnicode(false);
			entity.HasOne(d => d.PresentationType).WithMany(p => p.Presentations)
							.HasForeignKey(d => d.PresentationTypeId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkPresentation_PresentationType");
			entity.HasOne(d => d.DefaultLanguage).WithMany(p => p.Presentations)
				.HasForeignKey(d => d.DefaultLanguageCode)
				.HasConstraintName("fkPresentation_Language");
		});
	}
}