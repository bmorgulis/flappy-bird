using Godot;
using System;

public partial class Pipe : Node2D
{
	[Export] private float Speed = 200f;

	public override void _PhysicsProcess(double delta)
	{
		Position += new Vector2(-Speed * (float)delta, 0);

		if (Position.X < -GetViewportRect().Size.X / 2)
		{
			QueueFree();
		}
	}
}
