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
    }
    //持续时间，单位秒
    public int Duration { get; set; } = 60;
    //照明亮度
    public int Brightness { get; set; } = 100;
    //照明范围
    public int IlluminateRange { get; set; } = 5;

    public void Use()
    {
        // Implement the use functionality for the candle item
    }
    
}
