namespace TaleLearnCode.SpeakerToolkit.Services;

public class TagServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<List<TagResponse>> GetTagsAsync()
		=> (await GetDatAsync()).ToResponse();

	public async Task<TagResponse?> GetTagAsync(int id)
		=> (await GetDatAsync(id)).FirstOrDefault()?.ToResponse();

	public async Task<TagResponse?> CreateTagAsync(TagRequest tagRequest)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Tag? tag = await context.Tags.FirstOrDefaultAsync(tag => tag.TagName == tagRequest.Name);
		if (tag is not null)
			throw new ObjectAlreadyExistsException($"Tag '{tagRequest.Name}' already exists.");
		tag = new()
		{
			TagName = tagRequest.Name
		};
		await context.Tags.AddAsync(tag);
		await context.SaveChangesAsync();
		return (await GetDatAsync(tag.TagId)).First().ToResponse();
	}

	public async Task<TagResponse?> UpdateTagAsync(int tagId, TagRequest tagRequest)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Tag? tag = (await GetDataAsync(context, tagId)).FirstOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(tagId), "Tag not found.");
		tag.TagName = tagRequest.Name;
		await context.SaveChangesAsync();
		return tag.ToResponse();
	}

	private async Task<List<Tag>> GetDatAsync(int? id = null)
	{
		using var context = new SpeakerToolkitContext(_configServices);
		return await GetDataAsync(context, id);
	}

	private static async Task<List<Tag>> GetDataAsync(SpeakerToolkitContext context, int? id = null)
	{
		IQueryable<Tag> query = context.Tags;
		if (id.HasValue)
			query = query.Where(tag => tag.TagId == id.Value);
		return await query.ToListAsync();
	}

}