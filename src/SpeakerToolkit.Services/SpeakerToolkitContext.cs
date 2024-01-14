#nullable disable
namespace TaleLearnCode.SpeakerToolkit;

public partial class SpeakerToolkitContext : DbContext
{
	public SpeakerToolkitContext(DbContextOptions<SpeakerToolkitContext> options)
			: base(options)
	{
	}

	public virtual DbSet<Country> Countries { get; set; }

	public virtual DbSet<CountryDivision> CountryDivisions { get; set; }

	public virtual DbSet<Engagement> Engagements { get; set; }

	public virtual DbSet<EngagementPresentation> EngagementPresentations { get; set; }

	public virtual DbSet<EngagementPresentationDownload> EngagementPresentationDownloads { get; set; }

	public virtual DbSet<EngagementPresentationSpeaker> EngagementPresentationSpeakers { get; set; }

	public virtual DbSet<Models.EngagementStatus> EngagementStatuses { get; set; }

	public virtual DbSet<EngagementTag> EngagementTags { get; set; }

	public virtual DbSet<EngagementType> EngagementTypes { get; set; }

	public virtual DbSet<Language> Languages { get; set; }

	public virtual DbSet<LearningObjective> LearningObjectives { get; set; }

	public virtual DbSet<Presentation> Presentations { get; set; }

	public virtual DbSet<PresentationRelated> RelatedPresentations { get; set; }

	public virtual DbSet<PresentationSpeaker> PresentationSpeakers { get; set; }

	public virtual DbSet<PresentationTag> PresentationTags { get; set; }

	public virtual DbSet<PresentationText> PresentationTexts { get; set; }

	public virtual DbSet<PresentationType> PresentationTypes { get; set; }

	public virtual DbSet<Speaker> Speakers { get; set; }

	public virtual DbSet<SpeakerAward> SpeakerAwards { get; set; }

	public virtual DbSet<SpeakerAwardType> SpeakerAwardTypes { get; set; }

	public virtual DbSet<SpeakerBiography> SpeakerBiographies { get; set; }

	public virtual DbSet<SpeakerLink> SpeakerLinks { get; set; }

	public virtual DbSet<Tag> Tags { get; set; }

	public virtual DbSet<Models.TimeZone> TimeZones { get; set; }

	public virtual DbSet<WorldRegion> WorldRegions { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		CreateModel.Country(modelBuilder);
		CreateModel.CountryDivision(modelBuilder);
		CreateModel.Engagement(modelBuilder);
		CreateModel.EngagementPresentation(modelBuilder);
		CreateModel.EngagementPresentationDownload(modelBuilder);
		CreateModel.EngagementPresentationSpeaker(modelBuilder);
		CreateModel.EngagementStatus(modelBuilder);
		CreateModel.EngagementTag(modelBuilder);
		CreateModel.EngagementType(modelBuilder);
		CreateModel.Language(modelBuilder);
		CreateModel.LearningObjective(modelBuilder);
		CreateModel.Presentation(modelBuilder);
		CreateModel.PresentationRelated(modelBuilder);
		CreateModel.PresentationSpeaker(modelBuilder);
		CreateModel.PresentationTag(modelBuilder);
		CreateModel.PresentationText(modelBuilder);
		CreateModel.PresentationType(modelBuilder);
		CreateModel.Speaker(modelBuilder);
		CreateModel.SpeakerAward(modelBuilder);
		CreateModel.SpeakerAwardType(modelBuilder);
		CreateModel.SpeakerBiography(modelBuilder);
		CreateModel.SpeakerLink(modelBuilder);
		CreateModel.Tag(modelBuilder);
		CreateModel.TimeZone(modelBuilder);
		CreateModel.WorldRegion(modelBuilder);
		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}