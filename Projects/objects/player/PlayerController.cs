using Godot;
using Godot.Collections;
using System;

public partial class PlayerController : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;
	public bool tired;
	public bool starving;
	public bool brainDead;
	
	AnimationPlayer animationPlayer;
	Sprite2D playerSprite;

	[Export] public Stat brainStat;
	[Export] public Stat hungerStat;
	[Export] public Stat brawnStat;
	[Export] public Timer starvationTimer;
	[Export] public Timer brainDeadTimer;
	[Export] public Timer tiredTimer;

	[Export] public Attribute welfare;
	private float lastWelfareModifier;
    public override void _Ready()
    {
        animationPlayer = (AnimationPlayer)FindChild("AnimationPlayer");
		playerSprite = (Sprite2D)FindChild("Sprite");
		starvationTimer.Timeout += Starve;
		brainDeadTimer.Timeout += BrainDead;
		tiredTimer.Timeout += Tired;
    }
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
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

		Velocity = tired? velocity / 2 : velocity;
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

	protected virtual void OnUpdateBrainStats(){
		brainDead = brainStat.currentValue <= brainStat.minValue;
		if(brainDead && brainDeadTimer.IsStopped()){
			brainDeadTimer.Start(1);
			lastWelfareModifier = welfare.modifier;
			welfare.modifier = 0.5f;
		}
		else if(!brainDead){
			brainDeadTimer.Stop();
			welfare.modifier = lastWelfareModifier;
		}
	}
	protected virtual void OnUpdateHungerStats(){
		starving = hungerStat.currentValue <= hungerStat.minValue;
		if(starving && starvationTimer.IsStopped()){
			starvationTimer.Start(1);
		}
		else if(!starving){
			starvationTimer.Stop();
		}
	}
	protected virtual void OnUpdateBrawnStats(){
		tired = brawnStat.currentValue <= brawnStat.minValue;
		if(tired && tiredTimer.IsStopped()){
			tiredTimer.Start(1);
		}
		else if(!tired){
			tiredTimer.Stop();
		}
	}

	void BrainDead(){

	}
	void Tired(){

	}
	void Starve(){
		brawnStat.currentValue -= 1;
		brawnStat.Update();
		brainStat.currentValue -= 1;
		brainStat.Update();
	}
}
