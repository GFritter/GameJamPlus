using Godot;
using System;
using System.Linq;

public partial class AttributeAnimation : AnimationPlayer
{
	[Export] public Stat dishStat;

    public override void _Ready()
    {
        dishStat.OnUpdate += UpdateAnimation;
    }

	public void UpdateAnimation(float newValue){
		int animationNumber = (int)newValue;
		if(GetAnimationList().Count() >= animationNumber - 1){
			Play("Level"+animationNumber.ToString());
		}
	}
}
