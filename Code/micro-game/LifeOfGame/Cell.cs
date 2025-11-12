using Godot;
using System;
using Godot.Collections;



/// <summary>
/// Represents the life state of an entity.
/// </summary>
public enum CellState
{
    Alive,
    Dead
}





public partial class Cell : Node2D
{
    [Export]
    public CellState CurrentState { get; private set; } = CellState.Dead;

    /// <summary>
    /// Holds the states of neighboring cells.
    /// </summary>
    public Array<Cell> neighborStates = new Array<Cell>();

    public Vector2I GridPos; // 逻辑坐标

    /// <summary>
    /// 细胞死亡，改变状态为Dead并发出Died信号
    /// </summary>
    public void Kill()
    {
        if (CurrentState == CellState.Dead)
            return;

        CurrentState = CellState.Dead;
        EmitSignal(SignalName.Died);
    }
    /// <summary>
    /// 细胞复活，改变状态为Alive并发出Revived信号
    /// </summary>
    public void Revive()
    {
        if (CurrentState == CellState.Alive)
            return;

        CurrentState = CellState.Alive;
        EmitSignal(SignalName.Revived);
    }

    /// <summary>
    /// 改变细胞状态
    /// </summary>
    public void ChangeState()
    {
        if (CurrentState == CellState.Dead)
            Revive();
        else
            Kill();
    }

    //接收其他细胞的状态信息
    /// <summary>
    /// 依据其他细胞状态进行更新
    /// </summary>
    /// <param name="newStates">The new surrounding cell states.</param>
    public void UpdateSurroundingCell(Array<Cell> newStates)
    {
        neighborStates = newStates;
        var nextState = DetermineNextState();
        if (nextState != CurrentState)
        {
            ChangeState();
        }
    }

    //判断下一次细胞状态
    public CellState DetermineNextState()
    {
        int aliveCount = 0;

        foreach (var cell in neighborStates)
        {
            if (cell.CurrentState == CellState.Alive)
                aliveCount++;
        }

        if (CurrentState == CellState.Alive)
        {
            if (aliveCount < 2 || aliveCount > 3)
                return CellState.Dead; // Underpopulation or Overpopulation
            else
                return CellState.Alive; // Survival
        }
        else
        {
            if (aliveCount == 3)
                return CellState.Alive; // Reproduction
            else
                return CellState.Dead; // Stays dead
        }
    }

    [Signal]
    public delegate void DiedEventHandler();

    [Signal]
    public delegate void RevivedEventHandler();
    /// <summary>
    /// Button pressed handler to toggle life state.
    /// </summary>
    public void _on_btn_pressed()
    {
        ChangeState();
    }

}
