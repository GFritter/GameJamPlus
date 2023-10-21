using Godot;
using System;
using System.Security;

public partial class UIManager : Control
{

	[Export]
	public ProgressBar hungerBar,brainBar,brawnBar;

	[Export]
	Stat hungerStat,brainStat,brawnStat,moneyStat,foodStat;

	[Export]
	public Label moneyCount,foodCount;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetupBars();
		SetupLabels();
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

	public void SetupLabels()
	{
		moneyCount.Text = moneyStat.currentValue.ToString();
		moneyStat.OnUpdate+=UpdateMoneyCount;

		foodCount.Text = foodStat.currentValue.ToString();
		foodStat.OnUpdate+=UpdateFoodCount;
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

	public void UpdateMoneyCount(float value)
	{
		moneyCount.Text = value.ToString();
	}

	public void UpdateFoodCount(float value)
	{
		foodCount.Text = value.ToString();
	}


}
