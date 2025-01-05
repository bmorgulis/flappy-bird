using Godot;
using System;
using System.Threading.Tasks;

public partial class Login : Control
{
	private Button _submitButton;
	private Button _signUp_button;
	private LineEdit _username;
	private Label _error_lable;
	private LineEdit _password;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_submitButton = GetNode<Button>("Panel/Button");
		_username = GetNode<LineEdit>("Panel/UserName");
		_password = GetNode<LineEdit>("Panel/Password");
		_signUp_button = GetNode<Button>("Panel/Button2");
		_error_lable= GetNode<Label>("Panel/Label4");
		_error_lable.Text= "";
		_signUp_button.Pressed += GoToSignUp;
		_submitButton.Pressed += OnSubmitButtonPressedAsync;
	}
	private async void OnSubmitButtonPressedAsync()
	{
		
		try
		{
			var saveUserTask = Task.Run(() =>
			{
				GD.Print("task 2");
				var userInformationHandler = new UserInformationHandler();
				return userInformationHandler.LoginUser(_username.Text, _password.Text);
			});
			bool[] results = await Task.WhenAll(saveUserTask);
			if (!results[0]) {
				_error_lable.Text = "Invalid Username or Password";
			} else {
				GetTree().ChangeSceneToFile("res://scene.tscn");
			}
			GD.Print("Tasks completed successfully.");
		}
		catch (AggregateException ex)
		{
			foreach (var inner in ex.InnerExceptions)
			{
				GD.PrintErr("Task failed: " + inner.Message);
			}
		}
		catch (Exception ex)
		{
			GD.PrintErr("Unexpected error: " + ex.Message);
		}
		
	}
	private void GoToSignUp() {
		GetTree().ChangeSceneToFile("res://control.tscn");
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
