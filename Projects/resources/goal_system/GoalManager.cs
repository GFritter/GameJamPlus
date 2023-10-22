using Godot;
using Godot.Collections;
using System;

public partial class GoalManager : Node
{
	[Export] public Array<GoalList> dailyGoals;
	[Export] public GoalList currentGoals;

	


    public override void _Ready()
    {
        base._Ready();
    }






}
