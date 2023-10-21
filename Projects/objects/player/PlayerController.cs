using Godot;
using Godot.Collections;
using System;

public partial class PlayerController : CharacterBody2D
{
	[Export]
	public const float Speed = 300.0f;
	
	[Export]
	Array<Stat> stats;

	//maybe have those as stats and each thing selects what it increments with (opens possibility for more than one etc)
	[Export]
	public float workIncrement,plantIncrement,cookIncrement,dishIncrement;

	[Export]
	public int eatingBrainRecovery,eatingBrawnRecovery,eatingHungerRecovery;


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
