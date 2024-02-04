using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
	internal class ConsoleTest
	{
		internal static void Test()
		{
			using SpeakerToolkitSQLDatabaseContext context = new();
			Presentation? presentation = context.Presentations
				.Include(x => x.PresentationTexts)
					.ThenInclude(x => x.LearningObjectives)
				.FirstOrDefault(x => x.PresentationId == 1);

			Console.WriteLine(presentation?.PresentationTexts.FirstOrDefault()?.PresentationTitle);
			Console.WriteLine(presentation?.PresentationTexts.FirstOrDefault()?.LearningObjectives.Count);
		}
	}
}
