using Godot;
using Godot.Collections;
using System;

public partial class unlockableResource : Resource
{
	[Export] public Sprite2D fuzzy,normal;
	[Export] public  bool unlocked;
	[Export] public Array<Attribute> attributesAffected;
	[Export] public Array<float> affectedAmount;

	public void Unlock()
	{
		
	}

}
