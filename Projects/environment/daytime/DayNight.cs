using Godot;
using System;

public partial class DayNight : Control
{
    [Export] float minutesPerDay = 7f;
    [Export] Timer clockTimer;
    [Export] Label clockLabel;
    [Export] Label dayLabel;
    int currentDay = 1, currentHour = 9, currentMinute = 0, currentSecond;
    public override void _Ready()
    {
        clockTimer.Start(1 / CalculateMinutesPerSecond());
        clockTimer.Timeout += HandleDayTime;
        dayLabel.Text = "Day " + currentDay;
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
                currentHour = 0;
                currentDay+=1;
                dayLabel.Text = "Day " + currentDay;
            }
        }
        clockLabel.Text = currentHour.ToString("D2")+":"+currentMinute.ToString("D2");
    }
}
