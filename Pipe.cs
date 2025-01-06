using Godot;
using System;

public partial class Pipe : Node2D
{
	[Export] private float Speed = 200f;
	private static int _score = 0; // Keep track of the score
	private Label _scoreLabel; // Reference to the score label
	public static int savedScore = 0;

	public override void _Ready()
	{
		
		var topPipeArea = GetNode<Area2D>("TopPipeArea"); //get the top pipe area
		var bottomPipeArea = GetNode<Area2D>("BottomPipeArea"); //get the bottom pipe area
		var area = GetNode<Area2D>("Area2D");
		
		//connect the pipes to the body entered signal. If player(body) enters the pipe(collision), the OnBodyEntered method will be called
		topPipeArea.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		bottomPipeArea.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		area.Connect("body_entered", new Callable(this, nameof(OnBirdPassed)));
		
		_scoreLabel = GetNode<Label>("/root/World/ScoreLabel"); // Adjust path as necessary
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
			SetScore(_score);
			UserInformationHandler userInformationHandler = new UserInformationHandler();
			userInformationHandler.SaveScore();
			//Todo Zerach Display high score
			_score = 0; // Reset the score to 0 for the next game
			GameManager.Instance.GameOver();
		}
	}
	
	private void OnBirdPassed(Node body)
	{
		string currentUsername = GetStoredUsername(); // Get the current username for saving the score to the correct user
		GD.Print("User: " + currentUsername);
		
		_score += 1; // Increment the score
		_scoreLabel.Text = $"Score: {_score}"; // Update the score label
		
		GD.Print("Bird passed through the pipe! Score: " + _score);
	}
	private void SetScore(int score)
	{
		savedScore = score;
	}
	public static int GetScore()
	{
		return savedScore;
	}
	private string GetStoredUsername()
	{
		return UserInformationHandler.LoggedInUsername;
	}
}
