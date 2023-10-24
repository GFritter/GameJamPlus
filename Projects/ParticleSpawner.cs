using Godot;
using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

public partial class ParticleSpawner : GpuParticles2D
{
    public override void _Ready()
    {
		GD.Print("spawneo");
		this.ProcessMaterial.Set("scale_min",0.3f);
		this.ProcessMaterial.Set("scale_max",0.6f);
        Emitting = true;
    }
    public override void _Process(double delta)
    {
        if(!Emitting){
			QueueFree();
		}
    }
}
