using Godot;
using System;

public partial class BarActivator : TextureProgressBar
{
    public override void _Ready()
    {
        ValueChanged += Update;
		Modulate = new Color(0, 0, 0, 0);
    }
    public void Update(double value){
		if(value < 0.1f){
			Modulate = new Color(0, 0, 0, 0);
		}
		else{
			Modulate = new Color(1, 1, 1, 1);
		}
	}
}
