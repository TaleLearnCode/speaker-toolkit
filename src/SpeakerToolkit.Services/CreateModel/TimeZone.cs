namespace TaleLearnCode.SpeakerToolkit;

internal static partial class CreateModel
{
	internal static void TimeZone(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Models.TimeZone>(entity =>
		{
			entity.HasKey(e => e.TimeZoneId).HasName("pkcTimeZone");

			entity.ToTable("TimeZone", tb => tb.HasComment("Represents the list of time zones as defined by the IANA."));

			entity.Property(e => e.TimeZoneId)
							.HasMaxLength(100)
							.IsUnicode(false)
							.HasComment("The identifier of the time zone as defined by the IANA.");
			entity.Property(e => e.Dstabbreviation)
							.HasMaxLength(4)
							.IsUnicode(false)
							.IsFixedLength()
							.HasComment("The IANA abbreviation for the time zone when in daylight savings time.")
							.HasColumnName("DSTAbbreviation");
			entity.Property(e => e.DstoffsetMinutes)
							.HasComment("The number of minutes offset from UTC when in daylight savings time.")
							.HasColumnName("DSTOffsetMinutes");
			entity.Property(e => e.DstoffsetName)
							.HasMaxLength(6)
							.IsUnicode(false)
							.IsFixedLength()
							.HasComment("THe name of the offset when in daylight savings time.")
							.HasColumnName("DSTOffsetName");
			entity.Property(e => e.Stdabbreviation)
							.IsRequired()
							.HasMaxLength(4)
							.IsUnicode(false)
							.IsFixedLength()
							.HasComment("The IANA abbreviation for the time zone when in standard time.")
							.HasColumnName("STDAbbreviation");
			entity.Property(e => e.StdoffsetMinutes)
							.HasComment("The number of minutes offset from UTC when in standard time.")
							.HasColumnName("STDOffsetMinutes");
			entity.Property(e => e.StdoffsetName)
							.IsRequired()
							.HasMaxLength(6)
							.IsUnicode(false)
							.IsFixedLength()
							.HasComment("The name of the offset when in standard time.")
							.HasColumnName("STDOffsetName");
			entity.Property(e => e.UtcoffsetId)
							.IsRequired()
							.HasMaxLength(1)
							.IsUnicode(false)
							.IsFixedLength()
							.HasComment("The letter designation for the UTC time offset.")
							.HasColumnName("UTCOffsetId");
		});
	}
}