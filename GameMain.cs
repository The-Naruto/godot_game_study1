using Godot;

public partial class GameMain : Node
{
    //击杀动效管理
    static PackedScene animation_scene;
    public static AnimationBase AnimationBase;

    //掉落物动效管理
    static PackedScene animation_drops;
    public static DropItems DropItems;

    //做所有复制节点的父节点
    public static Node2D DuplicateNode;
    public static Node2D ItemsParent;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animation_scene = ResourceLoader.Load<PackedScene>("res://Animations/animation.tscn");
        AnimationBase = animation_scene.Instantiate<AnimationBase>();
        AddChild(AnimationBase);

        animation_drops = ResourceLoader.Load<PackedScene>("res://drop_items/drop_items.tscn");
        DropItems = animation_drops.Instantiate<DropItems>();
        AddChild(DropItems);


        var node2d = new Node2D();
        node2d.Name = "duplicate_node";
      //  this.CallDeferred("add_child", GetWindow());
        //延迟加入节点
        GetWindow().CallDeferred(Node.MethodName.AddChild, node2d);
        DuplicateNode = node2d;
        var node2d2 = new Node2D();
        node2d.Name = "duplicate_node2";
        //  this.CallDeferred("add_child", GetWindow());
        //延迟加入节点
        GetWindow().CallDeferred(Node.MethodName.AddChild, node2d2);
        ItemsParent = node2d2;
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
