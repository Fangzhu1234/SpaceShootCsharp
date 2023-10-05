using Godot;
using System;

public partial class Explosion : Node2D
{
	private void OnExpTimer()
	{
		QueueFree();
	}
}
