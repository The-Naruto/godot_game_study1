using Godot;
using System;

public partial class NowWeapons : Node2D
{
	int weaponRadius = 230;
	int weaponNum = 230;




	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		var num = GetChildCount();
        
        var  unit = Math.Tau / num;

         

        var weapons = GetChildren(true);

		for ( int i = 0;i<num;i++)
		{
			Node2D weapon = weapons[i] as Node2D;
			float weaponRad = (float)unit * i;
			var endPos = weapon.Position + new Vector2(weaponRadius, 0).Rotated(weaponRad);
			weapon.Position = endPos;
           // GD.Print("start" + i);
        }

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
