using Godot;

public partial class NowEnemies : Node
{
    PackedScene enemyScene;

    TileMap tileMap;


    int maxLegth = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        enemyScene = ResourceLoader.Load<PackedScene>("res://enemy/enemy.tscn");

        tileMap = GetTree().GetFirstNodeInGroup("map") as TileMap;

        maxLegth = tileMap.GetUsedCells(0).Count;


    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }



    public void OnTimerTimeout()
    {
        //因为最大值包含边界,但是数组的索引是从0开始,所以要减1
        int num = GD.RandRange(0, maxLegth-1);
        Vector2 vector2 = tileMap.MapToLocal(tileMap.GetUsedCells(0)[num]);


        var enemy = enemyScene.Instantiate<Enemy>();
        //如果地图缩放,坐标也要缩放,4.2是否优化?
        //enemy.Position = vector2* new Vector2(6,6);
        enemy.Position = vector2;


        GetTree().Root.AddChild(enemy);

    }

}
