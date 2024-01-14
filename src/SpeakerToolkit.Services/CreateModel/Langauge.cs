﻿namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void Language(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Language>(entity =>
		{
			entity.HasKey(e => e.LanguageCode).HasName("pkcLangauge");

			entity.ToTable("Language", tb => tb.HasComment("Represents a spoken/written language."));

			entity.Property(e => e.LanguageCode)
							.HasMaxLength(2)
							.IsUnicode(false)
							.IsFixedLength()
							.HasComment("Identifier of the language.");
			entity.Property(e => e.IsEnabled).HasComment("Flag indicating whether the language is enabled.");
			entity.Property(e => e.LanguageName)
							.IsRequired()
							.HasMaxLength(100)
							.HasComment("Name of the language.");
			entity.Property(e => e.NativeName)
							.IsRequired()
							.HasMaxLength(100)
							.HasComment("Native name of the language.");
		});
	}
}
