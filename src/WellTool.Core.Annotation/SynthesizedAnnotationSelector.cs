namespace WellTool.Core.Annotation;

using System;

public class SynthesizedAnnotationSelector : ISynthesizedAnnotationSelector
{
	public static readonly SynthesizedAnnotationSelector Instance = new SynthesizedAnnotationSelector();

	public ISynthesizedAnnotation Select(ISynthesizedAnnotation[] synthesizedAnnotations)
	{
		if (synthesizedAnnotations == null || synthesizedAnnotations.Length == 0)
		{
			return null;
		}

		ISynthesizedAnnotation selected = null;
		int minVertical = int.MaxValue;
		int minHorizontal = int.MaxValue;

		foreach (var sa in synthesizedAnnotations)
		{
			if (sa.GetVerticalDistance() < minVertical ||
				(sa.GetVerticalDistance() == minVertical && sa.GetHorizontalDistance() < minHorizontal))
			{
				minVertical = sa.GetVerticalDistance();
				minHorizontal = sa.GetHorizontalDistance();
				selected = sa;
			}
		}

		return selected;
	}
}
