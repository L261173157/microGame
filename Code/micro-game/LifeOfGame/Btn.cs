using Godot;
using System;

public partial class Btn : Button
{
    public CellState CurrentState { get; private set; }= CellState.Dead;
    private Texture2D iconTexture;

    public override void _Ready()
    {
        base._Ready();
        //加载图标纹理
        iconTexture = GD.Load<Texture2D>("res://LifeOfGame/resource/icon.png");
    }


    public void Killed()
    {
        CurrentState = CellState.Dead;
    }

    public void Revived()
    {
        CurrentState = CellState.Alive;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (CurrentState == CellState.Alive)
        {
            // Text = "Alive";
            Icon = iconTexture;
            
        }
        else
        {
            // Text = "Dead";
            Icon = null;
        }
    }

}
