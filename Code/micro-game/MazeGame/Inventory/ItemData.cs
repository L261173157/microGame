using Godot;
using System;

/// <summary>
/// 物品数据类
/// 定义游戏中物品的基本属性和信息
/// 继承自 Godot 的 Resource 类，可用作可序列化资源
/// </summary>
public partial class ItemData : Resource
{
    /// <summary>
    /// 物品唯一标识符
    /// 用于区分不同的物品类型
    /// </summary>
    [Export] public  string Id { get; set; }
    
    /// <summary>
    /// 物品名称
    /// 显示在用户界面中
    /// </summary>
    [Export] public string Name { get; set; }
    
    /// <summary>
    /// 物品图标纹理
    /// 在背包或菜单中显示物品的视觉表示
    /// </summary>
    [Export] public Texture2D Icon { get; set; }
    
    /// <summary>
    /// 物品是否可堆叠
    /// true 表示该物品可以多个叠放在一个槽位中
    /// false 表示每个槽位只能放一个
    /// </summary>
    [Export] public bool Stackable { get; set; } = true;
    
    /// <summary>
    /// 物品最大堆叠数量
    /// 仅在 Stackable 为 true 时有效
    /// 默认值为 99
    /// </summary>
    [Export] public int MaxStack { get; set; } = 99;
}
