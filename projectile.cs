using Godot;
using System;

public partial class projectile : Area2D
{
	[Export]
	public int Speed = 500;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2(Speed, 0) * (float)delta;
	}

	private void OnOffScreen()
	{
		QueueFree();
	}
}
