using Godot;
using System;

public partial class Pipe : Node2D
{
    [Export] private float Speed = 200f;
    private Button newGameButton;

    public override void _Ready()
    {
        var topPipeArea = GetNode<Area2D>("TopPipeArea"); //get the top pipe area
        var bottomPipeArea = GetNode<Area2D>("BottomPipeArea"); //get the bottom pipe area

        //connect the pipes to the body entered signal
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
            GD.Print("Game Over"); //if body is player, print "Game Over"
            GetTree().Paused = true; // Pause the game

            newGameButton = new Button();
            newGameButton.Text = "New Game";
            newGameButton.AddThemeColorOverride("font_color", new Color(0, 1, 0)); // green text

            var viewportSize = GetViewportRect().Size; // Get the size of the visible area of game window
            var buttonSize = new Vector2(200, 50);
            newGameButton.Size = buttonSize;
            newGameButton.Position = (viewportSize - buttonSize) / 2; // Center the button on screen

            newGameButton.ProcessMode = ProcessModeEnum.Always; // Button always processed even if game is paused
            newGameButton.Show();

            newGameButton.Pressed += OnNewGameButtonPressed; // Connect the button's pressed signal to the OnNewGameButtonPressed method
            // newGameButton.Connect("pressed", Callable.From(OnNewGameButtonPressed));  // Connect the button's pressed signal to the OnNewGameButtonPressed method

            var canvasLayer = new CanvasLayer(); // Create a new CanvasLayer which will be used to display the button
            if (!canvasLayer.IsInsideTree())
            {
                GetTree().Root.AddChild(canvasLayer);
            }
            canvasLayer.AddChild(newGameButton); // Add the button to the CanvasLayer
        }
    }

    public void OnNewGameButtonPressed()
    {
        GD.Print("Pressed New Game Button"); //logging to console

        // Check if the button exists and is in the scene tree. If yes then remove it before reloading the scene
        if (newGameButton != null && newGameButton.IsInsideTree())
        {
            newGameButton.QueueFree(); // Remove the button from the scene tree
        }
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }
}