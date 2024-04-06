using Godot;

public partial class Enemy : CharacterBody2D
{
    float speed = 200f;
    Vector2 dir;

    private double hp = 3f;
    [Export]
    public float Hp { get; set; }

    public bool Isdispose = false;

    CharacterBase target;
    AnimatedSprite2D thisAnimated;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        target = GetTree().GetFirstNodeInGroup("player") as CharacterBase;
        thisAnimated = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (target != null)
        {
            dir = (target.GlobalPosition - GlobalPosition).Normalized();

            Velocity = dir * speed;

            MoveAndSlide();
        }
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        Isdispose = true;
    }

    internal void Hurt(int v)
    {
        hp -= (v+target.PlayerData.BasicHurt)*target.PlayerData.BasicHurtMultiple;


        if (hp <= 0)
        {
            GameMain.AnimationBase.RunAnimation(GameMain.DuplicateNode, "enemies_dead", GlobalPosition, new Vector2(0.7f, 0.7f));
            GameMain.DropItems.GenDropItems(GameMain.DuplicateNode, "gold_item", GlobalPosition, new Vector2(1f, 1f));
            this.QueueFree();
        }
        else
        {
            EnemyFlash();
            GameMain.AnimationBase.RunAnimation(this, "enemies_hurt", new Vector2(1.1f, 1.1f));
        }
        //  }


    }

    private async void EnemyFlash()
    {
        thisAnimated.Material.Set("shader_parameter/flash_opacity", 1);

        // await Task.Delay(100);
        await ToSignal(GetTree().CreateTimer(0.1), "timeout");
        // GetTree().CreateTimer(0.1).Timeout += Enemy_Timeout;
        // if ( !Isdispose)
        // {
        thisAnimated.Material.Set("shader_parameter/flash_opacity", 0);
    }
}
