﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TaleLearnCode.SpeakerToolkit.Models;

/// <summary>
/// Details about a speaker.
/// </summary>
public partial class Speaker
{
    /// <summary>
    /// The identifier of the speaker.
    /// </summary>
    public int SpeakerId { get; set; }

    /// <summary>
    /// The first name of the speaker.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// The last name of the speaker.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Flag indicating whether the speaker profile is displayed publicly.
    /// </summary>
    public bool EnablePublicProfile { get; set; }

    public string PublicProfileUrl { get; set; }

    /// <summary>
    /// Idenfiier of the country where the speaker is located.
    /// </summary>
    public string CountryCode { get; set; }

    /// <summary>
    /// Identifier of the country division where the speaker is located.
    /// </summary>
    public string CountryDivisionCode { get; set; }

    public virtual Country CountryCodeNavigation { get; set; }

    public virtual CountryDivision CountryDivision { get; set; }

    public virtual ICollection<EngagementPresentationSpeaker> EngagementPresentationSpeakers { get; set; } = new List<EngagementPresentationSpeaker>();

    public virtual ICollection<PresentationSpeaker> PresentationSpeakers { get; set; } = new List<PresentationSpeaker>();

    public virtual ICollection<SpeakerAward> SpeakerAwards { get; set; } = new List<SpeakerAward>();

    public virtual ICollection<SpeakerBiography> SpeakerBiographies { get; set; } = new List<SpeakerBiography>();

    public virtual ICollection<SpeakerLink> SpeakerLinks { get; set; } = new List<SpeakerLink>();
}