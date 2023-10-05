using Godot;
using System;

public partial class space : Node2D
{
	public PackedScene PlayerScene;
	public PackedScene FireScene;
	public PackedScene AstroidScene;
	public PackedScene ExplosionScene;
	private ship Player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerScene = ResourceLoader.Load<PackedScene>("res://ship.tscn");
		FireScene = ResourceLoader.Load<PackedScene>("res://projectile.tscn");
		AstroidScene = ResourceLoader.Load<PackedScene>("res://astroid.tscn");
		ExplosionScene = ResourceLoader.Load<PackedScene>("res://explosion.tscn");
		GameStart();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void GameStart()
	{
		Player = PlayerScene.Instantiate<ship>();
		Vector2 ShipSpawn = GetNode<Marker2D>("ShipSpawn").Position;
		Player.Position = ShipSpawn;
		AddChild(Player);
		Player.ShipFire += OnShipFire;
		Player.ShipHit += OnShipHit;

	}
	private void OnShipFire(Vector2 location)
	{
		projectile fire = FireScene.Instantiate<projectile>();
		fire.Position = location + new Vector2(8, 0);
		AddChild(fire);
	}
	private void OnShipHit(Vector2 location)
	{
		Explosion boom = ExplosionScene.Instantiate<Explosion>();
		boom.Position = location;
		AddChild(boom);
		GameStart();
	}

	private void OnSpawnTimer()
	{
		Astroid rock = AstroidScene.Instantiate<Astroid>();
		var RockSpawnLocation = GetNode<PathFollow2D>("AstroidZone/AstroidSpawn");
		RockSpawnLocation.ProgressRatio = GD.Randf();
		rock.Position = RockSpawnLocation.Position;
		AddChild(rock);
		rock.AstroidHit += OnAstroidHit;
	}
	private void OnAstroidHit(Vector2 location)
	{
		Explosion boom = ExplosionScene.Instantiate<Explosion>();
		boom.Position = location;
		AddChild(boom);
	}
}
