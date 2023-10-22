using Godot;
using System;

public partial class iLoadable : Node2D
{

	//Object Reference
	[Export]
	public Area2D zoneArea;

	[Export]
	public ProgressBar bar;

	[Export]
	public float minValue, currentValue, maxValue,decayRate;

	[Export]
	public float increment;

	[Export]
	public bool active;

	[Signal]
	public delegate void onEmptyEventHandler();

	[Signal]
	public delegate void onCompleteEventHandler();

	[Export]
	public Attribute attribute;


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
			AddProgress();
		}

		if(currentValue>minValue)
		{
			currentValue-=decayRate;
			UpdateBar();

			if(currentValue<=minValue)
			{
				currentValue = minValue;
				EmitSignal("onEmpty");
			}

		}
			

			

	}

	protected void AddProgress(float value)
	{
		
		currentValue +=(attribute.flatRate+value)*attribute.modifier;
		
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
		//GD.Print("Voce ganhou palhaco");
		EmitSignal("onComplete");
		Reset();
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
