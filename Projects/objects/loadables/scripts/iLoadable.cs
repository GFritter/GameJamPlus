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

	[Export] public Attribute attribute;
	[Export] public Attribute welfare;


	[Export] protected GoalResource managedResource;

	[Export]
	public AudioStreamPlayer2D playOnProcess;
	[Export]
	public AudioStreamPlayer2D playOnEnd;
	[Export] public PackedScene[] particlesOnProcess;
	[Export] public PackedScene[] particlesOnEnd;

    public virtual void Spawn(PackedScene[] particlesToSpawn)
    {
        if(particlesToSpawn != null){
			foreach(PackedScene particle in particlesToSpawn){
				ParticleSpawner newParticle = (ParticleSpawner)particle.Instantiate();
				GetTree().CurrentScene.AddChild(newParticle);
				newParticle.GlobalPosition = GlobalPosition;
			}
		}
    }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Reset();
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
		
		currentValue +=(((attribute.flatRate)*attribute.modifier)*welfare.modifier)*value;
		
		if(currentValue>=maxValue)
		{
			OnComplete();
		}
		if(currentValue % (maxValue / 10) <= 0.05){
			Spawn(particlesOnProcess);
		}
		if(playOnProcess != null && !playOnProcess.Playing){
				playOnProcess.Play();
		}
	}

	protected void AddProgress()
	{
		
		currentValue +=(((attribute.flatRate)*attribute.modifier)*welfare.modifier);

		if(currentValue>=maxValue)
		{
			OnComplete();
		}
		if(currentValue % (maxValue / 10) <= 0.1){
			Spawn(particlesOnProcess);
		}
	}

	protected void UpdateBar()
	{
		bar.Value = currentValue;
	}

	protected virtual void OnComplete()
	{
		if(playOnEnd != null) playOnEnd.Play();
		Spawn(particlesOnEnd);
		EmitSignal("onComplete");
		Reset();
	}
	protected virtual void OnEmpty(){
		EmitSignal("onEmpty");
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
			if(playOnProcess != null && playOnProcess.Playing){
				playOnProcess.Stop();
			}
		}

	}



}
