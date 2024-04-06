using Godot;
using Godot.Collections;
using System;

public partial class MapSeceneBase : Node2D
{

    TileMap _tileMap;

    [Export]
    public RoundEnd RoundEnd;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _tileMap = GetNode<TileMap>("TileMap");


        RandomGrass();

        //从场景中绑定回合结束事件
        GameUI.RoundEnd += GameUI_RoundEnd;

    }

    private void GameUI_RoundEnd(object sender, int e)
    {
        GetTree().Paused = true;
        //GD.Print(e+"回合结束");
        RoundEnd.Show();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public void RandomGrass()
    {
        _tileMap.ClearLayer(1);
        Array<Vector2I> vector2Is = _tileMap.GetUsedCells(0);

        foreach (Vector2I cell in vector2Is)
        {
            var rnd = GD.RandRange(0, 100);
            if (rnd < 10)
            {
                Vector2I grassType;
                if (rnd % 2 == 0)
                {
                    grassType = new Vector2I(5,1);
                }
                else
                {
                    grassType = new Vector2I(6,1);
                }

                _tileMap.SetCell(2, cell,0, grassType);
            }

        }


    }
     




}
