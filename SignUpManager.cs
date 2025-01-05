using Godot;
using System;
using System.Threading.Tasks;

public partial class SignUpManager : Control
{
	private Button _submitButton;
	private Button _login_button;
	private Label _error_lable;
	private LineEdit _username;
	private LineEdit _password;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_submitButton = GetNode<Button>("Panel/Button");
		_username = GetNode<LineEdit>("Panel/UserName");
		_login_button = GetNode<Button>("Panel/Button2");
		_password = GetNode<LineEdit>("Panel/Password");
		_error_lable= GetNode<Label>("Panel/Label4");
		_error_lable.Text = "";
		_submitButton.Pressed += OnSubmitButtonPressed;
		_login_button.Pressed += GoToLogin;
	}
	private void GoToLogin() {
		GetTree().ChangeSceneToFile("res://Login.tscn");
	}
	private async void OnSubmitButtonPressed()
	{
		GD.Print("Starting tasks...");
		try
		{
			var emailTask = Task.Run(() =>
			{
				GD.Print("task 1");
				var emailHandler = new EmailHandler();
				emailHandler.Send(_username.Text, "Flappy Bird Signup Confirmation", "Thank you for joining Flappy Bird!");
				return true;
			});

			var saveUserTask = Task.Run(() =>
			{
				GD.Print("task 2");
				var userInformationHandler = new UserInformationHandler();
				return userInformationHandler.CreateUser(_username.Text, _password.Text);
			});
			bool[] results = await Task.WhenAll(emailTask, saveUserTask);
			if (!results[1]) {
				_error_lable.Text = "Username Already Exists";
			} else {
				GD.Print("Tasks completed successfully.");
				this.GoToLogin();
			}
			
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
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
