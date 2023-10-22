using Godot;
using System;

public partial class RewardAction : Resource
{
	
	public virtual void GiveReward()
	{
		GD.Print("Sou uma reward e estou sendo dada");
	}

}
