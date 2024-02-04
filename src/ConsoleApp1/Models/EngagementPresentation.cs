﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

/// <summary>
/// Represents the speaker&apos;&apos;s presentations.
/// </summary>
public partial class EngagementPresentation
{
    /// <summary>
    /// Identifier of the EngagementPresentation record.
    /// </summary>
    public int EngagementPresentationId { get; set; }

    /// <summary>
    /// Identifier of the associated engagement.
    /// </summary>
    public int EngagementId { get; set; }

    /// <summary>
    /// Identifier of the associated presentation.
    /// </summary>
    public int PresentationId { get; set; }

    /// <summary>
    /// The starting date and time for the presentation.
    /// </summary>
    public DateTime? StartDateTime { get; set; }

    /// <summary>
    /// The ending date and time for the presentation.
    /// </summary>
    public DateTime? EndDateTime { get; set; }

    public string TimeZone { get; set; }

    /// <summary>
    /// The room where the presentation is being presented.
    /// </summary>
    public string Room { get; set; }

    public virtual Engagement Engagement { get; set; }

    public virtual ICollection<EngagementPresentationDownload> EngagementPresentationDownloads { get; set; } = new List<EngagementPresentationDownload>();

    public virtual ICollection<EngagementPresentationSpeaker> EngagementPresentationSpeakers { get; set; } = new List<EngagementPresentationSpeaker>();

    public virtual Presentation Presentation { get; set; }
}