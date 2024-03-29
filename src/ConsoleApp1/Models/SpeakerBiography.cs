﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

/// <summary>
/// Contains the title and bio of a speaker in a specified langauge.
/// </summary>
public partial class SpeakerBiography
{
    /// <summary>
    /// The identifier of the speaker bio record.
    /// </summary>
    public int SpeakerBiographyId { get; set; }

    /// <summary>
    /// The identifier of the speaker.
    /// </summary>
    public int SpeakerId { get; set; }

    /// <summary>
    /// Code of the associated language.
    /// </summary>
    public string LanguageCode { get; set; }

    /// <summary>
    /// The title for the speaker.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The biography for the speaker.
    /// </summary>
    public string Biography { get; set; }

    public virtual Language LanguageCodeNavigation { get; set; }

    public virtual Speaker Speaker { get; set; }
}