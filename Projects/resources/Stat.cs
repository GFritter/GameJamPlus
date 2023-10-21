using Godot;
using System;


public partial class Stat : Resource
{
	[Export]
	public string name;

	[Export]
	public float minValue,currentValue,maxValue;

	[Signal]
	public delegate void OnFullEventHandler();

	[Signal]
	public delegate void OnEmptyEventHandler();

	[Signal]
	public delegate void OnUpdateEventHandler(float value);

	public void Update()
	{
		if(currentValue >= maxValue)
		{
			currentValue = maxValue;
			EmitSignal("OnFull");
		}

		if(currentValue<=minValue)
		{
			currentValue = minValue;
			EmitSignal("OnEmpty");
		}

		EmitSignal("OnUpdate",currentValue);
		GD.Print("MandeiUpdate");

	}

	public Stat()
	{

	}


}
