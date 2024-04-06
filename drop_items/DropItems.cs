using Godot;

public partial class DropItems : CharacterBody2D
{

    public bool IsCanMove = false;

    float speed = 1500;


    CharacterBase player;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.SetCollisionLayerValue(5, false);
        this.Visible = false;
        player = GetTree().GetFirstNodeInGroup("player") as CharacterBase;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (IsCanMove)
        {
            var dir = (player.Position - Position).Normalized();
            Velocity = dir * speed;
            MoveAndSlide();
        }

    }

    public void GenDropItems(Node2D parent, string aniName, Vector2 position, Vector2 scale = default)
    {
        var aniTemp = Duplicate() as CharacterBody2D;
        //1 在同一个碰撞信号里对场景修改多次会报debug错
        //1 parent.AddChild(aniTemp);
      //1 aniTemp.Show();
      //1 aniTemp.SetCollisionLayerValue(5, true);
        parent.CallDeferred(Node.MethodName.AddChild, aniTemp);
        aniTemp.CallDeferred(CollisionObject2D.MethodName.SetCollisionLayerValue, 5, true);
        aniTemp.CallDeferred(CollisionObject2D.MethodName.Show);

        aniTemp.Visible =true;
        aniTemp.Position = position;
        aniTemp.Scale = scale == default ? new Vector2(1, 1) : scale;
        AnimatedSprite2D animatedSprite2D = aniTemp.GetNode<AnimatedSprite2D>("all_animations");
        animatedSprite2D.Play(aniName);
     //   GetWindow().AddChild(aniTemp);

    }


}
