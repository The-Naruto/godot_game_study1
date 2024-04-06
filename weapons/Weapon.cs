using Godot;
//using Godot.Collections;
using System;
using System.Collections.Generic;
//using Dictionary = Godot.Collections.Dictionary;
public partial class Weapon : Node2D
{
	AnimatedSprite2D animatedSprite;

	Marker2D shootPos;
	Timer timer;

	float bulletShootTime = 0.5f;
	float bulletSpeed = 2000;
	float bulletHurt = 1;
 
    PackedScene bulletTscn;

    Dictionary<string,string> weaponLevel = new Dictionary<string, string>() {
		{"level_1","#b0c3d9" },
		{"level_2","#4b69ff" },
		{"level_3","#d32ce6" },
		{"level_4","#8847ff" },
		{"level_5","#eb4b4b" },
	
	};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        bulletTscn  =ResourceLoader.Load<PackedScene>("res://bullets/bullet.tscn");
		 
        shootPos = GetNode<Marker2D>("shoot_pos");
        timer = GetNode<Timer>("Timer");

        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		 //可以通过设置避免复制 
        //Material materialTemp = animatedSprite.Material.Duplicate() as Material;
		//animatedSprite.Material = materialTemp;
		Color color = new Color(weaponLevel["level_" + GD.RandRange(1, 5)]);
       // GD.Print("当前设定颜色:"+ color);
        animatedSprite.Material.Set("shader_parameter/color", color);
        Variant variant = animatedSprite.Material.Get("shader_parameter/color");


      //  GD.Print("当前颜色:" + variant.AsColor());
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (enemies.Count > 0)
		{
			this.LookAt(enemies[0].Position);
		}
		else
		{
			RotationDegrees = 0;
		}

	}


	public void OnTimerTimeout()
	{
		if (enemies.Count > 0)
		{
			var newBullet = bulletTscn.Instantiate<Bullet>();
			newBullet.bulletSpeed = bulletSpeed;
			newBullet.bulletHurt = bulletHurt;
			newBullet.Position = shootPos.GlobalPosition;
            //每次攻击前都给敌人排序,攻击距离武器最近的敌人
			SortEnemies();
            newBullet.director = (enemies[0].GlobalPosition - newBullet.Position).Normalized();
			GetTree().Root.AddChild(newBullet);
		}
    }



    #region attack area

	List<Node2D> enemies = new List<Node2D>();

    public void OnArea2dBodyEntered(Node2D body)
	{
		if (body.IsInGroup("enemy") && !enemies.Contains(body))
        {
            enemies.Add(body);
          

        }

    }

    private void SortEnemies()
    {
        enemies.Sort((a, b) =>
        {
            float v1 = a.GlobalPosition.DistanceTo(this.GlobalPosition);
            float v2 = b.GlobalPosition.DistanceTo(this.GlobalPosition);

            if (v1 == v2) return 0;
			//距离小的放前面,这个返回值的排序规则是-1,0,1
            return v1 > v2 ? 1 :-1;
        });
    }

    public void OnArea2dBodyExited(Node2D body)
    {
        if (enemies.Contains(body))
        {

            enemies.Remove(body);
        }
    }

    #endregion




}
