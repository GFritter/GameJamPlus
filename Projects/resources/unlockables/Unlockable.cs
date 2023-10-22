using Godot;
using System;

public partial class Unlockable : Node
{

	[Export] public unlockableResource uResource;
	[Export] public Sprite2D sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(uResource.unlocked)
		{
			sprite = uResource.normal;
		}

		else
		{
			sprite = uResource.fuzzy;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void onUnlock()
	{
		//change attributes
		for(int i=0;i<uResource.attributesAffected.Count;++i)
		{
			uResource.attributesAffected[i].flatRate += uResource.affectedAmount[i];


		}

		//change sprite
		sprite = uResource.normal;

		uResource.unlocked = true;

	}
}
