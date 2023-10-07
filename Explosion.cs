using Godot;
using System;

public partial class Explosion : Node2D
{
	private void _Ready()
	{
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(GetNode<Sprite2D>("Sprite2D"), "scale", Vector2.Zero, 1.5f).SetTrans(Tween.TransitionType.Linear);
	}
	private void OnExpTimer()
	{
		QueueFree();
	}
}
