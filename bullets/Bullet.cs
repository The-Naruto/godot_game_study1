using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
    public Vector2 director = Vector2.Zero;
    public float bulletSpeed = 2000;
    public float bulletHurt = 1;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Velocity = director * bulletSpeed;

		MoveAndSlide();
    }


    public void OnArea2dBodyShapeEntered(Rid rid, Node2D body, int shapeIdx, int localShapeIdx)
    {
        if (body.IsInGroup("enemy")) {
            Enemy enemy =  body as Enemy;
            enemy.Hurt(1);
            
            this.QueueFree();
        }
       
        if (body is TileMap)
        {
            TileMap tileMap = (TileMap)body;
            //获取碰撞到的墙的坐标
            Vector2I coords = tileMap.GetCoordsForBodyRid(rid);

            TileData tileData = tileMap.GetCellTileData(2, coords);

            if (tileData.GetCustomData("IsWall").AsBool())
            {
                this.QueueFree();
            }

        }


    }


}
