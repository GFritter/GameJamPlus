using Godot;
using System;

public partial class GoalResource : Resource
{
	
	[Export] public string name;
	[Export] public Sprite2D sprite;
	[Export] public int value;

	[Signal] public delegate void OnValueChangeEventHandler(int value,string name);

	public void AddAmount(int v)
	{
		value+=v;
		EmitSignal("OnValueChange",value,name);
	}

}
