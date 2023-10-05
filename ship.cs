using Godot;
using System;

public partial class ship : Area2D
{
	[Export]
	public int Speed = 300;
	[Signal]
	public delegate void ShipFireEventHandler(Vector2 location);
	[Signal]
	public delegate void ShipHitEventHandler(Vector2 location);
	private Vector2 ScreenSize = new Vector2();
	private Vector2 ShipSize = new Vector2();
	private bool _reload = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		ShipSize = new Vector2(16,16);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Get input and move ship on the screen
		var velocity = new Vector2(0, 0);
		var InputVector = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		velocity += InputVector * Speed;
		Position += velocity * (float)delta;
		Position = new Vector2(
			Mathf.Clamp(Position.X, ShipSize.X/2, ScreenSize.X-ShipSize.X/2),
			Mathf.Clamp(Position.Y, ShipSize.Y/2, ScreenSize.Y-ShipSize.Y/2));
		if (Input.IsActionPressed("fire")) {
			EmitSignal(SignalName.ShipFire, Position);
			GetNode<Timer>("ReloadTimer").Start();
			_reload = true;
		}
	}
	private void OnReloadTimer()
	{
		_reload = false;
	}
	private void OnHit(Area2D obj)
	{
		if (obj.IsInGroup("astroids"))
		{
			EmitSignal(SignalName.ShipHit, Position);
			QueueFree();
		}
	}
}
