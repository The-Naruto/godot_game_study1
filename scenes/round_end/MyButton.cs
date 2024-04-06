using Godot;
using System;

public partial class MyButton : Button
{
	public event EventHandler<string> Clicked;

	public string GroupName;
	public string TypeName;
	public string Value;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ButtonDown += Pressed;

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private new void Pressed()
	{
		Clicked?.Invoke(null, GroupName + "@" + TypeName+"@"+ Value);

    }

}
