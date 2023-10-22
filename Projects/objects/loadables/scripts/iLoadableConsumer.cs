using Godot;
using Godot.Collections;
using System;

public partial class ConsumerStat
{
	[Export]
	public Stat sConsumed;
	
	[Export]
	public float rate;
}

public partial class iLoadableConsumer : iLoadable
{

	[Export]
	public Array<Stat> statsConsumed;

	[Export]
	public Array<float>rateConsumed;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(active && currentValue<maxValue && CheckValid((float)delta))
		{
		
			AddProgress((float)delta);
			Consume((float)delta);
		}

		if(currentValue>minValue)
		{
			currentValue-=decayPerSecond * (float)delta;
			UpdateBar();

			if(currentValue<=minValue)
			{
				currentValue = minValue;
				OnEmpty();
			}

		}
	}

	//maybe can become a variable too
	protected virtual bool CheckValid(float delta)
	{
		//GD.Print("vou validar");
		bool valid = true;

		for(int i =0;i<statsConsumed.Count;++i)
		{
			if(statsConsumed[i].currentValue - rateConsumed[i] * delta < statsConsumed[i].minValue)
			{
				valid=false;
			}

		}


		return valid;

	}
	


	protected void Consume(float delta)
	{
		for(int i =0;i<statsConsumed.Count;++i)
		{
			statsConsumed[i].currentValue -= rateConsumed[i] * delta;
			statsConsumed[i].Update();

		}

		//GD.Print("To consumindo");

	}

}
