using Godot;
using System;

public partial class MazeMain : Node2D
{
    //游戏胜利标志
    public bool isWin = false;

    public CharacterBody2d Player;
    public WinLabel WinLabel;

    public override void _Ready()
    {
        Player = GetNode<CharacterBody2d>("Player");
        WinLabel = GetNode<WinLabel>("CanvasLayer/Control/winLabel");
        Player.AppleCollected += _on_Apple_Collected;
    }

    //重新开始游戏
    public void RestartGame()
    {
        Player.ResetPosition();
        isWin = false;
    }

    private void _on_Apple_Collected()
    {
        isWin = true;
        GD.Print("You win!");
    }
    //每帧动画更新
    public override void _Process(double delta)
    {
        if (isWin)
        {
            WinLabel.SetVisible(true);
        }
        else
        {
            WinLabel.SetVisible(false);
        }
    }
}