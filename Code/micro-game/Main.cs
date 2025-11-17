using Godot;
using System;

public partial class Main : Node2D
{
    public override void _Ready()
    {
        GD.Print("Main scene is ready.");
    }

    public void _on_LifeGameStartButton_pressed()
    {
        GetTree().ChangeSceneToFile("res://LifeOfGame/LifeMain.tscn");
    }

    public void _on_MazeGameStartButton_pressed()
    {
        GetTree().ChangeSceneToFile("res://MazeGame/MazeMain.tscn");
    }
}
