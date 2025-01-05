using Godot;
using System;

public partial class GameManager : Node2D
{
    private Button newGameButton;
    private CanvasLayer canvasLayer; // CanvasLayer for UI elements
    private static GameManager _instance;

    // public static GameManager Instance
    // {
    // 	get
    // 	{
    // 		return _instance;
    // 	}
    // }
    public static GameManager Instance => _instance;

    public override void _Ready()
    {
        if (GameManager.Instance == null)
            if (_instance == null)
            {
                _instance = this;
                SetProcess(true); //makes sure that the GameManager is not disposed 
            }
            else
            {
                QueueFree(); // If a diff GameManager exists, remove this one
                return;
            }

        canvasLayer = new CanvasLayer();  
        AddChild(canvasLayer);
    }

    public void GameOver()
    {
        if (_instance == null || !IsInstanceValid(_instance))
        {
            GD.PrintErr("GameManager instance is invalid or disposed.");  // for debugging
            return;
        }

        GD.Print("Game Over"); //if body is player(from pipe class), print "Game Over"
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

        newGameButton.Pressed +=
            OnNewGameButtonPressed; // Connect the button's pressed signal to the OnNewGameButtonPressed method
        // newGameButton.Connect("pressed", Callable.From(OnNewGameButtonPressed));  // Connect the button's pressed signal to the OnNewGameButtonPressed method

        canvasLayer.AddChild(newGameButton); // Add the button to the CanvasLayer
    }

    private void OnNewGameButtonPressed()
    {
        GD.Print("Pressed New Game Button"); //logging to console

        // Check if the button exists and is in the scene tree. If yes then remove it before reloading the scene
        if (newGameButton != null && newGameButton.IsInsideTree())
        {
            newGameButton.QueueFree(); // Remove the button from the scene tree
        }

        _instance = null; // Reset the instance before reloading the scene otherwise we will have no instances of GameManager and will get errors
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }

    public override void _Process(double delta)
    {
    }
}