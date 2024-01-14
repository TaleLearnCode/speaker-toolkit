namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	public static void PresentationText(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<PresentationText>(entity =>
		{
			entity.HasKey(e => e.PresentationTextId).HasName("pkcPresentationText");

			entity.ToTable("PresentationText", tb => tb.HasComment("The text for a presentation details in a specific language."));

			entity.HasIndex(e => new { e.PresentationId, e.LanguageCode }, "unqPresentationText_PresentationId_LanguageCode").IsUnique();

			entity.Property(e => e.PresentationTextId).HasComment("The identifier of the presentation text record.");
			entity.Property(e => e.Abstract)
							.HasMaxLength(3000)
							.HasComment("The full abstract for the presentation.");
			entity.Property(e => e.AdditionalDetails).HasMaxLength(3000);
			entity.Property(e => e.LanguageCode)
							.IsRequired()
							.HasMaxLength(2)
							.IsUnicode(false)
							.IsFixedLength();
			entity.Property(e => e.PresentationId).HasComment("The identifier of the associated presentation.");
			entity.Property(e => e.PresentationShortTitle)
							.HasMaxLength(60)
							.HasComment("The short title of the presentation.");
			entity.Property(e => e.PresentationTitle)
							.IsRequired()
							.HasMaxLength(300)
							.HasComment("The full title of the presentation.");
			entity.Property(e => e.ShortAbstract)
							.HasMaxLength(2000)
							.HasComment("The short abstract for the presentation.");
			entity.Property(e => e.Summary)
							.HasMaxLength(140)
							.HasComment("The summary for the presentation.");

			entity.HasOne(d => d.LanguageCodeNavigation).WithMany(p => p.PresentationTexts)
							.HasForeignKey(d => d.LanguageCode)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkPresentationText_Language");

			entity.HasOne(d => d.Presentation).WithMany(p => p.PresentationTexts)
							.HasForeignKey(d => d.PresentationId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkPresentationText_Presentation");
		});
	}
}