﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

/// <summary>
/// Links a speaker to a presentation.
/// </summary>
public partial class PresentationSpeaker
{
    /// <summary>
    /// The identifier of the presentation speaker record.
    /// </summary>
    public int PresentationSpeakerId { get; set; }

    /// <summary>
    /// Identifier of the associated presentation.
    /// </summary>
    public int PresentationId { get; set; }

    /// <summary>
    /// Identifier of the associated speaker.
    /// </summary>
    public int SpeakerId { get; set; }

    /// <summary>
    /// Flag indicaitng whether the speaker is the primary speaker for the presentation.
    /// </summary>
    public bool IsPrimary { get; set; }

    public virtual Presentation Presentation { get; set; }

    public virtual Speaker Speaker { get; set; }
}