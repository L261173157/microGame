using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// 角色移动控制类
/// 处理玩家的行走、碰撞检测和苹果收集功能
/// </summary>
public partial class CharacterBody2d : CharacterBody2D
{
    /// <summary>
    /// 角色移动速度（像素/秒）
    /// </summary>
    [Export]
    public float Speed = 50.0f;
    
    /// <summary>
    /// 角色初始位置，用于重置
    /// </summary>
    public Vector2 InitialPosition;

    /// <summary>
    /// 迷宫游戏主控制器引用
    /// </summary>
    public MazeMain mazeMain;

    /// <summary>
    /// 苹果收集事件信号
    /// </summary>
    [Signal]
    public delegate void AppleCollectedEventHandler();

    /// <summary>
    /// 初始化方法，在节点进入场景时调用
    /// 保存初始位置并获取迷宫主控制器引用
    /// </summary>
    public override void _Ready()
    {
        base._Ready();
        InitialPosition = Position;
        mazeMain = GetNode<MazeMain>("/root/MazeMain");
    }

    /// <summary>
    /// 物理处理方法，每帧调用用于处理移动和碰撞
    /// </summary>
    /// <param name="delta">帧时间差（秒）</param>
    public override void _PhysicsProcess(double delta)
    {
        // 游戏胜利时停止移动
        if(mazeMain.isWin)
        {
            return;
        }

        Vector2 inputVector = Vector2.Zero;
        
        // 获取输入方向
        if (Input.IsActionPressed("ui_right"))
            inputVector.X += 1;
        if (Input.IsActionPressed("ui_left"))
            inputVector.X -= 1;
        if (Input.IsActionPressed("ui_down"))
            inputVector.Y += 1;
        if (Input.IsActionPressed("ui_up"))
            inputVector.Y -= 1;
        
        // 规范化输入向量，确保对角线移动速度与直线移动相同
        inputVector = inputVector.Normalized();
        
        // 计算速度向量并应用移动
        Velocity = inputVector * Speed;
        MoveAndSlide();

        // 检测碰撞物体
        var collisionInfo = MoveAndCollide(Velocity * (float)delta);
        if (collisionInfo != null)
        {
            var collider = collisionInfo.GetCollider() as Node;
            GD.Print("Collided with: ", collider.Name);
            
            // 判断是否与苹果碰撞，触发信号
            if(collider.Name=="apple")
            {
                EmitSignal(nameof(AppleCollected));
            }
        }
    }

    /// <summary>
    /// 重置角色位置到初始位置
    /// 在游戏重新开始时使用
    /// </summary>
    public void ResetPosition()
    {
        Position = InitialPosition;
    }
}
