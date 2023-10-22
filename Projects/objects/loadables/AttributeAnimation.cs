using Godot;
using System;
using System.Linq;

public partial class AttributeAnimation : AnimationPlayer
{
	iLoadable parent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(GetParent() is iLoadable){
			parent = (iLoadable)GetParent();
			parent.onLevelDown += OnLevelChanged;
			parent.onLevelUp += OnLevelChanged;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public virtual void OnLevelChanged(){
		CurrentAnimation = "Level"+parent.errand.currentLevel.ToString();
	}
}
