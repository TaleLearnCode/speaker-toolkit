﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

/// <summary>
/// Represents the list of time zones as defined by the IANA.
/// </summary>
public partial class TimeZone
{
    /// <summary>
    /// The identifier of the time zone as defined by the IANA.
    /// </summary>
    public string TimeZoneId { get; set; }

    /// <summary>
    /// The letter designation for the UTC time offset.
    /// </summary>
    public string UtcoffsetId { get; set; }

    /// <summary>
    /// The IANA abbreviation for the time zone when in standard time.
    /// </summary>
    public string Stdabbreviation { get; set; }

    /// <summary>
    /// The name of the offset when in standard time.
    /// </summary>
    public string StdoffsetName { get; set; }

    /// <summary>
    /// The number of minutes offset from UTC when in standard time.
    /// </summary>
    public short StdoffsetMinutes { get; set; }

    /// <summary>
    /// The IANA abbreviation for the time zone when in daylight savings time.
    /// </summary>
    public string Dstabbreviation { get; set; }

    /// <summary>
    /// THe name of the offset when in daylight savings time.
    /// </summary>
    public string DstoffsetName { get; set; }

    /// <summary>
    /// The number of minutes offset from UTC when in daylight savings time.
    /// </summary>
    public short? DstoffsetMinutes { get; set; }
}