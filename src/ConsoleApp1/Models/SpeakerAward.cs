﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

/// <summary>
/// Represents an award bestowed to a speaker.
/// </summary>
public partial class SpeakerAward
{
    /// <summary>
    /// The identifier of the speaker award record.
    /// </summary>
    public int SpeakerAwardId { get; set; }

    /// <summary>
    /// The identifier of the speaker award record.
    /// </summary>
    public int SpeakerId { get; set; }

    /// <summary>
    /// The identifier of the speaker award record.
    /// </summary>
    public int SpeakerAwardTypeId { get; set; }

    /// <summary>
    /// The identifier of the speaker award record.
    /// </summary>
    public string AwardCategory { get; set; }

    /// <summary>
    /// The identifier of the speaker award record.
    /// </summary>
    public int? AwardYear { get; set; }

    public string AwardProfileUrl { get; set; }

    public virtual Speaker Speaker { get; set; }

    public virtual SpeakerAwardType SpeakerAwardType { get; set; }
}