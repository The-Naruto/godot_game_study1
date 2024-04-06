using Godot;

public partial class AnimationBase : Node2D
{


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void RunAnimation(Node2D parent, string aniName, Vector2 position, Vector2 scale = default)
    {
        var aniTemp = Duplicate() as Node2D;
        parent?.AddChild(aniTemp);
        //aniTemp.pl
        aniTemp.Scale = scale == default ? new Vector2(1, 1) : scale;
        aniTemp.Position = position;
        AnimatedSprite2D animatedSprite2D = aniTemp.GetNode<AnimatedSprite2D>("all_animations");
        animatedSprite2D.Play(aniName);
        aniTemp.Show();
    }

    public void OnAllAnimationsAnimationFinished()
    {
        this.QueueFree();
    }
}
