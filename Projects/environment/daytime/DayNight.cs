using Godot;
using System;

public partial class DayNight : Control
{
    [Export] float minutesPerDay = 7f;
    [Export] Timer clockTimer;
    [Export] Label clockLabel;
    [Export] Label dayLabel;
    int currentDay = 1, currentHour = 9, currentMinute = 0, currentSecond;
    [Export] public int maxNumberOfDays = 3;

    [Export] public GoalManager goalManager;

    public bool onPause = false;

    public override void _Ready()
    {
        clockTimer.Timeout += HandleDayTime;
        OnDayBegin();
    }
    private float CalculateMinutesPerSecond(){
        int totalSeconds = (int)minutesPerDay * 60;
        return 1440 / totalSeconds;
    }
    private void HandleDayTime(){
        currentMinute+=1;
        if(currentMinute%60 == 0){
            currentHour+=1;
            currentMinute = 0;
            if(currentHour%24 == 0){
                 OnDayEnd();
            }
        }
        clockLabel.Text = currentHour.ToString("D2")+":"+currentMinute.ToString("D2");
    }

    public void OnDayBegin()
    { 
        goalManager.NewDay();
        clockTimer.Start(1 / CalculateMinutesPerSecond());
        dayLabel.Text = "Day " + currentDay;
    }

    public override void _Process(double delta)
    {
        if(onPause)
        {
            InBetweenDay();
        }
        
    }

    public void OnDayEnd()
    {

        currentHour = 0;
        currentDay+=1;
        //dayLabel.Text = "Day " + currentDay;

        clockTimer.Stop();
        GetTree().Paused=true;
        onPause = true;

       
      


    }

    public void InBetweenDay()
    {
        if(Input.IsActionPressed("Unpause"))
        {
            GetTree().Paused=false;
             OnDayBegin();
             onPause=false;
        }
        
        

    }

}
