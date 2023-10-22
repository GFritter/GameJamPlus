using Godot;
using Godot.Collections;
using System;

public partial class PlayerController : CharacterBody2D
{
	[Export]
	public const float Speed = 300.0f;
	
	AnimationPlayer animationPlayer;
	Sprite2D playerSprite;

    public override void _Ready()
    {
        animationPlayer = (AnimationPlayer)FindChild("AnimationPlayer");
		playerSprite = (Sprite2D)FindChild("Sprite");
    }
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
		if(direction == Vector2.Zero){
			animationPlayer.Play("idle");
		}
		else{
			if(direction.X < 0){
				playerSprite.Scale = new Vector2(-1, 1);
			}
			else if(direction.X > 0){
				playerSprite.Scale = new Vector2(1, 1);
			}
			animationPlayer.Play("walk");
		}
		MoveAndSlide();
	}
}
