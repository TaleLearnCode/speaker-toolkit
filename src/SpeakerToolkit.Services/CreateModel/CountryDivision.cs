﻿namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void CountryDivision(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CountryDivision>(entity =>
		{
			entity.HasKey(e => new { e.CountryCode, e.CountryDivisionCode }).HasName("pkcCountryDivision");

			entity.ToTable("CountryDivision", tb => tb.HasComment("Lookup table representing the world regions as defined by the ISO 3166-2 standard."));

			entity.Property(e => e.CountryCode)
							.HasMaxLength(2)
							.IsUnicode(false)
							.IsFixedLength();
			entity.Property(e => e.CountryDivisionCode)
							.HasMaxLength(3)
							.IsUnicode(false)
							.IsFixedLength()
							.HasComment("Identifier of the country division using the ISO 3166-2 Alpha-2 code.");
			entity.Property(e => e.CategoryName)
							.IsRequired()
							.HasMaxLength(100)
							.IsUnicode(false)
							.HasComment("The category name of the country division.");
			entity.Property(e => e.CountryDivisionName)
							.IsRequired()
							.HasMaxLength(100)
							.IsUnicode(false)
							.HasComment("Name of the country using the ISO 3166-2 Subdivision Name.");
			entity.Property(e => e.IsEnabled).HasComment("Flag indicating whether the country division record is enabled.");

			entity.HasOne(d => d.Country).WithMany(p => p.CountryDivisions)
							.HasForeignKey(d => d.CountryCode)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fkCountryDivision_Country");
		});
	}
}