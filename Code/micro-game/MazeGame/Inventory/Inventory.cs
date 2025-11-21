using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory
{
    // 1号格子
    public List<ItemData> Items1 { get; set; } = new List<ItemData>();
    // 2号格子
    public List<ItemData> Items2 { get; set; } = new List<ItemData>();
    // 3号格子
    public List<ItemData> Items3 { get; set; } = new List<ItemData>();

    public Inventory()
    {

    }

    //背包添加物品
    public void AddItem(ItemData item)
    {
        //判断哪个格子空，如果是空的添加；如果不是空的，是否和已有的是一个类型的物品；如果是同一类型物品，是否已经达到存储上限
        if (Items1.Count == 0)
        {
            Items1.Add(item);
        }
        else if (Items1.Count != 0)
        {
            if (Items1[0].Id == item.Id)
            {
                if (Items1.Count < item.MaxStack)
                {
                    Items1.Add(item);
                }
                else
                {
                    return;
                }
            }
        }
        else if (Items2.Count == 0)
        {
            Items2.Add(item);
        }
        else if (Items2.Count != 0)
        {
            if (Items2[0].Id == item.Id)
            {
                if (Items2.Count < item.MaxStack)
                {
                    Items2.Add(item);
                }
                else
                {
                    return;
                }
            }
        }
        else if (Items3.Count == 0)
        {
            Items3.Add(item);
        }
        else if (Items3.Count != 0)
        {
            if (Items3[0].Id == item.Id)
            {
                if (Items3.Count < item.MaxStack)
                {
                    Items3.Add(item);
                }
                else
                {
                    return;
                }
            }
        }
    }

    //背包使用物品
    public void UseItem(int slot)
    {
        if (slot < 1 || slot > 3)
            return;
        switch (slot)
        {
            case 1:
                if (Items1.Count > 0)
                {
                    Items1.RemoveAt(Items1.Count - 1);
                }
                break;
            case 2:
                if (Items2.Count > 0)
                {
                    Items2.RemoveAt(Items2.Count - 1);
                }
                break;
            case 3:
                if (Items3.Count > 0)
                {
                    Items3.RemoveAt(Items3.Count - 1);
                }
                break;
        }

    }
}
