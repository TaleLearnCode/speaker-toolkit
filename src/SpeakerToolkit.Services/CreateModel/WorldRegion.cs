namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void WorldRegion(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<WorldRegion>(entity =>
		{
			entity.HasKey(e => e.WorldRegionCode).HasName("pkcWorldRegion");

			entity.ToTable("WorldRegion", tb => tb.HasComment("Lookup table representing the world regions as defined by the UN M49 specification."));

			entity.HasIndex(e => e.ParentId, "idxWorldRegion_ParentId");

			entity.Property(e => e.WorldRegionCode)
							.HasMaxLength(3)
							.IsUnicode(false)
							.IsFixedLength()
							.HasComment("Identifier of the world region.");
			entity.Property(e => e.IsEnabled).HasComment("Flag indicating whether the world region is enabled.");
			entity.Property(e => e.ParentId)
							.HasMaxLength(3)
							.IsUnicode(false)
							.IsFixedLength()
							.HasComment("Identifier of the world region parent (for subregions).");
			entity.Property(e => e.WorldRegionName)
							.IsRequired()
							.HasMaxLength(100)
							.IsUnicode(false)
							.HasComment("Name of the world region.");

			entity.HasOne(d => d.Parent).WithMany(p => p.Subregions)
							.HasForeignKey(d => d.ParentId)
							.HasConstraintName("fkWorldRegion_WorldRegion");
		});
	}
}