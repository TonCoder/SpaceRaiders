using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class MinMaxSliderAttribute : PropertyAttribute
{
	public readonly float min;
	public readonly float max;

	public MinMaxSliderAttribute() : this(0, 1) {}

	public MinMaxSliderAttribute(float min, float max)
	{
		this.min = min;
		this.max = max;
	}
}
