using Godot;
using System;

public partial class GoalUiItem : Control
{
	[Export] public Goal goal;
	[Export] public TextureRect image;
	[Export] public Label name,current,target;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupUIElement()
	{
		name.Text = goal.goalName;
		current.Text = goal.currentValue.ToString();
		target.Text = "/"+goal.targetValue.ToString();

		goal.OnGoalProgress+=UpdateUIElements;
		goal.OnGoalCompleted+=CompleteTask;
	}

	public void SetupUIElement(Goal g)
	{
		goal=g;

		name.Text = goal.goalName;
		current.Text = goal.currentValue.ToString();
		target.Text = "/"+goal.targetValue.ToString();

		goal.OnGoalProgress+=UpdateUIElements;
		goal.OnGoalCompleted+=CompleteTask;
	}

	public void UpdateUIElements()
	{
		current.Text = goal.currentValue.ToString();
		target.Text = "/"+goal.targetValue.ToString();
	}

	public void CompleteTask()
	{
		GD.Print("completei a minha taskt");
		Modulate = new Color(0,1,0);
	}
}
