using Godot;
using System;
public partial class Errand : Resource
{
    [Export] public int minLevel, currentLevel = 0, maxLevel;
	[Export] public int currentXP = 0;
	[Export] public bool doesLevelDecay;
	[Export] public float[] levelDownTimePerLevel;
	[Export] public int[] neededXPPerLevel;
}
