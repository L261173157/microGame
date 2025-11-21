using Godot;
using System;

public partial class InventoryUI : GridContainer
{
	/// <summary>
	/// 物品按钮1
	/// </summary>
	public Button itemButton1;
	/// <summary>
	/// 物品按钮2
	/// </summary>
	public Button itemButton2;
	/// <summary>
	/// 物品按钮3
	/// </summary>
	public Button itemButton3;
	
	public Inventory inventory;

	public Texture2D emptyIcon;
	public override void _Ready()
	{
		itemButton1 = GetNode<Button>("Item1Btn");
		itemButton2 = GetNode<Button>("Item2Btn");
		itemButton3 = GetNode<Button>("Item3Btn");
		emptyIcon = GD.Load<Texture2D>("res://MazeGame/asset/noneItem.png");
		inventory = new Inventory();
	}

	
	public override void _Process(double delta)
	{
		if(inventory.Items1.Count > 0)
		{
			itemButton1.Icon = inventory.Items1[0].Icon;
			itemButton1.Text = inventory.Items1.Count.ToString();
		}
		else
		{
			itemButton1.Icon = emptyIcon;
			itemButton1.Text = "";
		}
		if(inventory.Items2.Count > 0)
		{
			itemButton2.Icon = inventory.Items2[0].Icon;
			itemButton2.Text = inventory.Items2.Count.ToString();
		}
		else
		{
			itemButton2.Icon = emptyIcon;
			itemButton2.Text = "";
		}
		if(inventory.Items3.Count > 0)
		{
			itemButton3.Icon = inventory.Items3[0].Icon;
			itemButton3.Text = inventory.Items3.Count.ToString();	
		}
		else
		{
			itemButton3.Icon = emptyIcon;
			itemButton3.Text = "";
		}
	}

	//使用物品
	public void OnItemButtonPressed(int index)
	{
		switch(index)
		{
			case 1:
				if(inventory.Items1.Count > 0)
				{
					if(inventory.Items1[0] is IItemFunction itemFunction)
					{
						itemFunction.Use();
						inventory.Items1.RemoveAt(0);
					}
				}
				break;
			case 2:
				if(inventory.Items2.Count > 0)
				{
					if(inventory.Items2[0] is IItemFunction itemFunction)
					{
						itemFunction.Use();
						inventory.Items2.RemoveAt(0);
					}
				}
				break;
			case 3:
				if(inventory.Items3.Count > 0)
				{
					if(inventory.Items3[0] is IItemFunction itemFunction)
					{
						itemFunction.Use();
						inventory.Items3.RemoveAt(0);
					}
				}
				break;
			default:
				break;
		}
	}
}
