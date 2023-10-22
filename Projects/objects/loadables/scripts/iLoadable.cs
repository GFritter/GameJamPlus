using Godot;
using System;
using System.Collections.Generic;

public partial class iLoadable : Node2D
{

	//Object Reference
	[Export]
	public Area2D zoneArea;

	[Export]
	public ProgressBar bar;

	[Export]
	public float minValue, currentValue, maxValue,decayPerSecond;

	[Export]
	public bool active;

	[Signal]
	public delegate void onEmptyEventHandler();

	[Signal]
	public delegate void onCompleteEventHandler();
	
	[Signal]
	public delegate void onLevelUpEventHandler();

	[Signal]
	public delegate void onLevelDownEventHandler();

	[Export]
	public Attribute attribute;

	[Export] public Errand errand;
	[Export] public Timer levelDownTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Reset();
		levelDownTimer.Timeout += OnLevelDown;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(active && currentValue<maxValue)
		{
			AddProgress((float)delta);
		}

		if(currentValue>minValue)
		{
			currentValue -= decayPerSecond * (float)delta;
			UpdateBar();

			if(currentValue<=minValue)
			{
				currentValue = minValue;
				OnEmpty();
			}

		}
		
	}

	protected void AddProgress(float value)
	{
		
		currentValue += (attribute.flatRate*attribute.modifier) * value;
		
		if(currentValue>=maxValue)
		{
			OnComplete();
		}
		
	}

	protected void AddProgress()
	{
		
		currentValue+=attribute.flatRate*attribute.modifier;

		if(currentValue>=maxValue)
		{
			OnComplete();
		}
		
	}

	protected void UpdateBar()
	{
		bar.Value = currentValue;
	}

	protected virtual void OnComplete()
	{
		levelDownTimer.Stop();
		errand.currentXP += 1;
		UpdateLevel();
		EmitSignal("onComplete");
		Reset();
	}
	protected virtual void UpdateLevel(){
		if(errand.currentLevel > errand.maxLevel){
			errand.currentLevel = errand.maxLevel;
			errand.currentXP = 0;
			return;
		}
		if(errand.currentXP >= errand.neededXPPerLevel[errand.currentLevel]){
			errand.currentXP = 0;
			errand.currentLevel += 1;
			
			EmitSignal("onLevelUp");
		}
	}
	protected virtual void OnEmpty(){
		EmitSignal("onEmpty");
		if(levelDownTimer.IsStopped() && errand.doesLevelDecay){
			levelDownTimer.Start(errand.levelDownTimePerLevel[errand.currentLevel]);
		}
	}
	protected virtual void OnLevelDown(){
		errand.currentLevel -= 1;
		errand.currentXP = 0;
		if(errand.currentLevel < 0){
			errand.currentLevel = 0;
		}
		EmitSignal("onLevelDown");
	}

	protected void Reset()
	{
		bar.MinValue = minValue;
		bar.MaxValue = maxValue;
		currentValue = minValue;
		bar.Value = currentValue;

	}

	public void TestAreaEnter(Node2D body)
	{
		GD.Print("o corpo" + body.Name.ToString() + "entrou");

		if(body.IsInGroup("Player"))
		{
			GD.Print("O mano parente entrou");
			active=true;
		}


	}

	public void TestAreaExit(Node2D body)
	{
		GD.Print("o corpo" + body.Name.ToString() + "saiu");

		if(body.IsInGroup("Player"))
		{
			GD.Print("O mano saiu");
			active=false;
		}

	}



}
