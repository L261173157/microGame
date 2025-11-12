using Godot;
using System;

public partial class BtnLifeGame : Button
{
    //跳转场景
    private void OnBtnLifeGamePressed()
    {
        GetTree().ChangeSceneToFile("res://LifeOfGame/LifeMain.tscn");
    }
}
