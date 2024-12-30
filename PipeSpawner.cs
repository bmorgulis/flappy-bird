using Godot;
using System;

public partial class PipeSpawner : Node2D
{
	[Export] private PackedScene PipePrefab;
	[Export] private float SpawnInterval = 2f;
	private Random _random = new Random(); // random number for randomizing the pipe vertical position

	public override void _Ready()
	{
		SetProcess(true);
		Timer timer = new Timer();
		timer.WaitTime = SpawnInterval;
		timer.OneShot = false;
		timer.Connect("timeout", new Callable(this, nameof(OnTimerTimeout)));
		AddChild(timer);
		timer.Start();
	}

	private void OnTimerTimeout()
{
	GD.Print("Spawning pipe");

	
	var pipeInstance = (Node2D)PipePrefab.Instantiate(); //makes a new instance of a scene
	float yPos = _random.Next(-200, 200); // get random vertical position between the 2 numbers for the pipe
	float xPos = GetViewportRect().Size.X; // gets the width of the viewport and put the pipe on right edge of the screen
	pipeInstance.Position = new Vector2(xPos, yPos);  //asigns position of x and y of the spawned pipe
	AddChild(pipeInstance);  // add new spawned pipe instance as a child
}
}
