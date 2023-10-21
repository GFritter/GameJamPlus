using Godot;
using System;

public partial class UIManager : Control
{

	[Export]
	public ProgressBar hungerBar,brainBar,brawnBar;

	[Export]
	Stat hungerStat,brainStat,brawnStat;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetupBars();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupBars()
	{
		
		hungerBar.MinValue = hungerStat.minValue;
		hungerBar.MaxValue = hungerStat.maxValue;
		hungerBar.Value= hungerStat.currentValue;
		hungerStat.OnUpdate+=UpdateHungerBar;

		brainBar.MinValue = brainStat.minValue;
		brainBar.MaxValue=brainStat.maxValue;
		brainBar.Value=brainStat.currentValue;
		brainStat.OnUpdate+=UpdateBrainBar;

		brawnBar.MinValue = brawnStat.minValue;
		brawnBar.MaxValue =brawnStat.maxValue;
		brawnStat.OnUpdate+=UpdateBrawnBar;

	}


	public void UpdateHungerBar(float value)
	{
		hungerBar.Value = value;
	}

	public void UpdateBrainBar(float value)
	{
		brainBar.Value=value;
	}

	public void UpdateBrawnBar(float value)
	{
		brawnBar.Value = value;
	}


}
