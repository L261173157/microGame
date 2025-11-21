using Godot;
using System;

public partial class CandleEffect : Node
{
	//实现candle的功能，可以用来照明，增加人物本身照明范围，持续时间有限

	//蜡烛持续时间
	public float duration;
	//照明范围增加值
	public float lightIncrease;
	//照明亮度增加值
	public float brightnessIncrease;
	//当前范围
	public float currentRange;
	//当前亮度
	public float currentBrightness;
	//玩家节点
	public CharacterBody2d player;

	public CandleEffect(float duration, float lightIncrease, float brightnessIncrease)
	{

		this.duration = duration;
		this.lightIncrease = lightIncrease;
		this.brightnessIncrease = brightnessIncrease;

	}
	public override void _Ready()
	{
		if (GameManager.Player != null)
		{
			player = GameManager.Player as CharacterBody2d;
			currentBrightness = player.GetNode<PointLight2D>("PointLight2D").Energy;
			currentRange = player.GetNode<PointLight2D>("PointLight2D").TextureScale;
		}
	}

	public override void _Process(double delta)
	{
	}

	public void ActivateCandle()
	{
		//增加玩家照明范围
		PointLight2D playerLight = player.GetNode<PointLight2D>("PointLight2D");
		playerLight.TextureScale = currentRange + lightIncrease;
		playerLight.Energy = currentBrightness + brightnessIncrease;
		GD.Print("Candle effect activated: Range increased to " + playerLight.TextureScale + ", Brightness increased to " + playerLight.Energy);
		//启动计时器，持续duration时间后关闭效果
		Timer timer = new Timer();
		timer.WaitTime = duration;
		timer.OneShot = true;
		timer.Timeout += () =>
		{
			//恢复玩家照明范围
			playerLight.TextureScale = currentRange;
			playerLight.Energy = currentBrightness;
			GD.Print("Candle effect ended: Range restored to " + playerLight.TextureScale + ", Brightness restored to " + playerLight.Energy);
			//从场景中移除计时器
			timer.QueueFree();
		};
		AddChild(timer);
		timer.Start();
	}



}
