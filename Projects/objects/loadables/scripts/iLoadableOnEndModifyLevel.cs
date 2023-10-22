using Godot;
using System;

public partial class iLoadableOnEndModifyLevel : iLoadableOnEnd
{
	
	[Export] public Errand[] errandChanged;
	[Export] public Errand[] errandDelta;
    protected override void OnComplete()
    {
        base.OnComplete();
		for (int i = 0; i < errandChanged.Length; i++)
		{
			errandChanged[i].currentLevel += errandDelta[i].currentLevel;
			errandChanged[i].currentXP += errandDelta[i].currentXP;
			UpdateLevel();
		}
    }
}
