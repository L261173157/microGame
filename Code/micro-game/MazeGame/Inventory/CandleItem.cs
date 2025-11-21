using Godot;
using System;

public partial class CandleItem : ItemData, IItemFunction
{
    public CandleItem()
    {
        Id = "candle";
        Name = "蜡烛";
        Stackable = true;
        MaxStack = 10;
        Icon = GD.Load<Texture2D>("res://MazeGame/asset/candle.png");
    }
    //持续时间，单位秒
    public int Duration { get; set; } = 60;
    //照明亮度
    public float Brightness { get; set; } = 0.5f;
    //照明范围
    public float IlluminateRange { get; set; } = 1.0f;

    public void Use()
    {
        //使用蜡烛，激活照明效果
        GD.Print("Using candle item");
        CandleEffect candleEffect = new CandleEffect(Duration, IlluminateRange, Brightness);
        if (GameManager.Player != null)
        {
            GameManager.Player.AddChild(candleEffect);
        }
        candleEffect.ActivateCandle();
    }
    
}
