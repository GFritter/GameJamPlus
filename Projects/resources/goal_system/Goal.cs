using Godot;
using System;

public partial class Goal : Resource
{
	[Export] public string goalName;
	[Export] public GoalResource goalResource;
	[Export] public int currentValue,targetValue;
	[Export] public bool completed;

	[Signal] public delegate void OnGoalCompletedEventHandler();
	[Signal] public delegate void OnGoalProgressEventHandler();

	public void GoalSetup()
	{
		//goalResource.OnValueChange+=UpdateGoal;

	}

	public void UpdateGoal(int value,string name)
	{
		currentValue = value;
		if(value>=targetValue)
		{
			currentValue=targetValue;
			EmitSignal("OnGoalProgress");
			completed=true;
			EmitSignal("OnGoalCompleted");
		}
		else
		{
			EmitSignal("OnGoalProgress");

		}
	}

}
