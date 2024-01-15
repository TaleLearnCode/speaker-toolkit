namespace TaleLearnCode.SpeakerToolkit.Extensions;

public static class ListExtensions
{

	/// <summary>
	/// Adds an item to the List if it not already in the list.
	/// </summary>
	/// <typeparam name="T">Type of the generic list to add the item to.</typeparam>
	/// <param name="list">The generic list to add the item to.</param>
	/// <param name="item">The object to add to the list.</param>
	public static void AddOnlyOnce<T>(this IList<T> list, T? item)
	{
		if (item is not null && !list.Contains(item))
			list.Add(item);
	}

	/// <summary>
	/// Tries to add an object to the end of the <see cref="List{T}"/>.
	/// </summary>
	/// <typeparam name="T">Tye type of items within the generic list.</typeparam>
	/// <param name="list">The list to try to add to.</param>
	/// <param name="item">The object to be added to the end of the <paramref name="item"/>.</param>
	/// <remarks>The method checks whether <paramref name="item"/> is null.  If so, it will not attempt to add the object to the list.</remarks>
	public static void TryAdd<T>(this List<T>? list, T? item) where T : new()
	{
		if (item is not null)
		{
			list ??= [];
			list.Add(item);
		}
	}

}