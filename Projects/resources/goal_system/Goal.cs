using Godot;
using System;

public partial class Goal : Resource
{
	[Export] public GoalResource goalResource;
	[Export] public int currentValue,targetValue;
	[Export] public bool completed;

	[Signal] public delegate void OnGoalCompletedEventHandler();
	[Signal] public delegate void OnGoalProgressEventHandler();

}
