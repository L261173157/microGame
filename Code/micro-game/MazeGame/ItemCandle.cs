using Godot;
using System;

public partial class ItemCandle : Area2D
{
	//在地图上的蜡烛物品
	
	//背包UI节点
	public InventoryUI inventoryUI;
	//玩家是否在区域范围内
	public bool isPlayerInArea = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
		inventoryUI = GetParent().GetNode<InventoryUI>("CanvasLayer/Control/InventoryUI");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//玩家在范围内，按下交互键拾取物品
		if(isPlayerInArea && Input.IsActionJustPressed("ui_accept"))
		{
			//创建蜡烛物品数据
			CandleItem candleItem = new CandleItem();
			//向背包添加物品
			inventoryUI.inventory.AddItem(candleItem);
			//从场景中移除物品
			QueueFree();
		}
	}

	public void OnBodyEntered(Node2D body)
	{
		if (body is CharacterBody2d player)
		{
			isPlayerInArea = true;
			GD.Print("Player entered candle area");
		}
	}

	public void OnBodyExited(Node2D body)
	{
		if (body is CharacterBody2d player)
		{
			isPlayerInArea = false;
			GD.Print("Player exited candle area");
		}
	}
	

}
