﻿namespace TaleLearnCode.SpeakerToolkit.Responses;

public class PresentationTypeResponse
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string? Description { get; set; }
	public int SortOrder { get; set; }
}