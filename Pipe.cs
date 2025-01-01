using Godot;
using System;

public partial class Pipe : Node2D
{
	[Export] private float Speed = 200f;
		public override void _Ready()
	{
		var topPipeArea = GetNode<Area2D>("TopPipeArea");
		var bottomPipeArea = GetNode<Area2D>("BottomPipeArea");

		topPipeArea.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		bottomPipeArea.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}
	

	public override void _PhysicsProcess(double delta)
	{
		Position += new Vector2(-Speed * (float)delta, 0);

		if (Position.X < -GetViewportRect().Size.X / 2)
		{
			QueueFree();
		}
	}
	
	  private void OnBodyEntered(Node body)
	{
		if (body is Player)
		{
			GD.Print("Game Over");
			GetTree().Paused = true; // Pause the game
		}
	}
}
