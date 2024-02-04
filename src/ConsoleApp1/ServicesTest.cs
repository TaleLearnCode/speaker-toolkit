using Microsoft.EntityFrameworkCore;
using TaleLearnCode.SpeakerToolkit;
using TaleLearnCode.SpeakerToolkit.Models;

namespace ConsoleApp1
{
	internal class ServicesTest
	{
		internal static void Test()
		{
			using SpeakerToolkitContext context = new();
			Presentation? presentation = context.Presentations
				.Include(x => x.PresentationTexts)
					.ThenInclude(x => x.LearningObjectives)
				.FirstOrDefault(x => x.PresentationId == 1);

			Console.WriteLine(presentation?.PresentationTexts.FirstOrDefault()?.PresentationTitle);
			Console.WriteLine(presentation?.PresentationTexts.FirstOrDefault()?.LearningObjectives.Count);
		}
	}
}
