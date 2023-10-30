using Godot;
using Godot.Collections;
using System;

public partial class GoalManager : Control
{
	[Export] public Array<GoalList> dailyGoals;
	[Export] public GoalList currentGoals;

    [Export] public PackedScene goalUITemplate;

    [Export] public int dayIndex = -1;

    [Export] public TextureButton openGoalsButton;
    [Export] public TextureButton closeGoalsButton;
    [Export] public Control goalContainer;
    [Export]public VBoxContainer listContainer;

    [Export] public Array<GoalResource> managedResources;


    public override void _Ready()
    {
        currentGoals = new GoalList();
        currentGoals.goals = new Array<Goal>();
        managedResources = new Array<GoalResource>();
        openGoalsButton.Pressed += () => goalContainer.Visible = !goalContainer.Visible;
        closeGoalsButton.Pressed += () => goalContainer.Visible = !goalContainer.Visible;
       //SetupDay();
       //SetupList();
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        if(Input.IsActionJustPressed("open_goals")){
            goalContainer.Visible = !goalContainer.Visible;
        }
    }

    public void SetupDay()
    {
       for(int i=0;i<dailyGoals[dayIndex].goals.Count;++i)
       {
        currentGoals.goals.Add(dailyGoals[dayIndex].goals[i]);
       }

    }

    public void NewDay()
    {
        dayIndex+=1;
        if(dayIndex<dailyGoals.Count)
        {
        SetupDay();
        CleanList();
        SetupList();

        }

        else{
            CleanList();
            SetupList();
        }
        
    }

    public void SetupList()
    {
        for(int i=0;i<currentGoals.goals.Count;++i)
        {
            if(!managedResources.Contains(currentGoals.goals[i].goalResource))
            {
                managedResources.Add(currentGoals.goals[i].goalResource);
                currentGoals.goals[i].goalResource.OnValueChange+=ManageGoalResource;

            }
            
            Node newItem = goalUITemplate.Instantiate();
            GoalUiItem refe = newItem as GoalUiItem;
            refe.SetupUIElement(currentGoals.goals[i]);
            currentGoals.goals[i].GoalSetup();
            listContainer.AddChild(refe);
        }
    }

    public void CleanList()
    {
        Array<Goal> removalList= new Array<Goal>();

        for(int i=0;i<currentGoals.goals.Count;++i)
        {
             if(currentGoals.goals[i].completed==true)
            {
                removalList.Add(currentGoals.goals[i]);
                currentGoals.goals[i].goalResource.OnValueChange-=ManageGoalResource;
            }
        }

        GD.Print("achei" +removalList.Count + "pra remover");

        for(int i=0;i<removalList.Count;++i)
        {
             currentGoals.goals.Remove(removalList[i]);
        }

        foreach(GoalUiItem n in listContainer.GetChildren())
        {
            listContainer.RemoveChild(n);
            n.RemoveUIElements();
        }


    }

    public void ManageGoalResource(int value,string name)
    {
        for(int i=0;i<currentGoals.goals.Count;++i)
        {
            if(currentGoals.goals[i].goalResource.name==name && currentGoals.goals[i].completed==false)
            {
                currentGoals.goals[i].UpdateGoal(value,name);
                return;
            }
        }

    }






}
