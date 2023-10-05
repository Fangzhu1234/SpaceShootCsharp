using Godot;
using System;

public partial class Astroid : Area2D
{
	[Export]
	public int Speed = 250;
	[Signal]
	public delegate void AstroidHitEventHandler(Vector2 location);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2(-Speed, 0) * (float)delta;
	}

	private void OnOffScreen()
	{
		QueueFree();
	}
	private void OnHit(Area2D obj)
	{
		if (obj.IsInGroup("projectiles"))
		{
			EmitSignal(SignalName.AstroidHit, Position);
		}
		QueueFree();
	}
}
