using Godot;
using System;
using Godot.Collections;

public partial class LifeMain : Node2D
{
    [Export]
    public PackedScene cellScene; //细胞场景
    //细胞生成概率
    [Export]
    public float cellSpawnProbability = 0.3f;
    [Export]
    public Timer timer;


    private const int CELL_SIZE = 24; //细胞大小
    private const int CELL_xCOUNT = 30; //细胞列数
    private const int CELL_yCOUNT = 30; //细胞行数
    private Label aliveCountLabel;
    //运行次数
    private int generationCount = 0;

    // 存储所有细胞，键为逻辑坐标 (x, y)
    private Dictionary<Vector2I, Cell> cellGrid = new();

    public override void _Ready()
    {
        //创建细胞阵列
        CreateCellArray(CELL_xCOUNT, CELL_yCOUNT);
        aliveCountLabel = GetNode<Label>("AliveCountLabel");
        UpdateAliveCount();
        timer.Timeout += _on_Timer_timeout;
    }
    /// <summary>
    /// 更新活细胞计数显示
    /// </summary>
    public void UpdateAliveCount()
    {
        int aliveCount = 0;

        foreach (var cell in cellGrid.Values)
        {
            if (cell.CurrentState == CellState.Alive)
                aliveCount++;
        }

        aliveCountLabel.Text = $"活数: {aliveCount}"
            + $"\n代数: {generationCount}";
    }
    /// <summary>
    /// 定时器超时处理函数，更新细胞状态
    /// </summary>
    private void _on_Timer_timeout()
    {
        UpdateCellStates();
    }

    //游戏开始
    public void StartGame()
    {
        generationCount = 0;
        RandomInitializeCells();
        timer.Start();
    }
    //游戏停止
    public void StopGame()
    {
        UpdateAliveCount();
        timer.Stop();
    }


    //放置细胞
    public void PlaceCell(int x = 0, int y = 0)
    {
        if (cellScene == null)
        {
            GD.PrintErr("Cell scene is not assigned.");
            return;
        }

        var cell = cellScene.Instantiate<Cell>();
        cell.Position = new Vector2(x * CELL_SIZE, y * CELL_SIZE);
        cell.GridPos = new Vector2I(x, y); // ← 每个细胞知道自己的逻辑位置
        cellGrid[new Vector2I(x, y)] = cell; // 存储细胞到字典中
        AddChild(cell);
    }

    //创造细胞阵列
    public void CreateCellArray(int cols, int rows)
    {
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                PlaceCell(x, y);
            }
        }
    }

    //更新细胞状态
    public void UpdateCellStates()
    {
        foreach (var cell in cellGrid.Values)
        {
            var neighbors = GetNeighborCells(cell.GridPos);
            cell.UpdateSurroundingCell(neighbors);
        }
        generationCount++;
        UpdateAliveCount();
    }

    //获取邻居细胞
    public Array<Cell> GetNeighborCells(Vector2I gridPos)
    {
        Array<Cell> neighbors = [];
        Vector2I[] directions =
        [
            new Vector2I(-1, -1), new Vector2I(0, -1), new Vector2I(1, -1),
            new Vector2I(-1, 0),                     new Vector2I(1, 0),
            new Vector2I(-1, 1), new Vector2I(0, 1), new Vector2I(1, 1)
        ];
        foreach (var dir in directions)
        {
            var neighborPos = gridPos + dir;
            if (cellGrid.TryGetValue(neighborPos, out var neighbor))
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }

    //随机初始化细胞状态
    public void RandomInitializeCells()
    {
        var rand = new Random();
        foreach (var cell in cellGrid.Values)
        {
            if (rand.NextDouble() < cellSpawnProbability)
            {
                cell.Revive();
            }
            else
            {
                cell.Kill();
            }
        }
    }
}
