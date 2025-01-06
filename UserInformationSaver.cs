using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using Godot;
using System.Linq;


class UserInformationHandler {
	public static string LoggedInUsername { get; private set; }


	public bool CreateUser(string email, string password) {
		string dbFilePath = "Database.txt";
		var user = new User(email, hashPassword(password));
		string userJson = JsonSerializer.Serialize(user);
		try
		{
			if (UserExists(email, dbFilePath) == null) {
				File.AppendAllText(dbFilePath, userJson + System.Environment.NewLine);
				LoggedInUsername = email;
				return true;
			}
			return false;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error writing to the text file: {ex.Message}");
			return false;
		}
		 
	}
	public bool LoginUser(string email, string password) {
		string dbFilePath = "Database.txt";
		string hashedPassword = UserExists(email, dbFilePath);
		if (hashedPassword != null) {
			if (CheckPasswordsAreEqual(hashedPassword, password)) {
				LoggedInUsername = email;
				return true;
			}
		}
		return false;
	}
	private string UserExists(string email, string filePath) {
		var lines = File.ReadLines(filePath);
		foreach (var line in lines) {
			User user = JsonSerializer.Deserialize<User>(line);
			if (user.Email == email) {
				return user.Password;
			}
		}
		return null;
	}

	private string hashPassword(string password) {
		using (SHA1 sHA1 = SHA1.Create()) {
			byte[] hash = sHA1.ComputeHash(Encoding.ASCII.GetBytes(password));
			StringBuilder stringBuildersb = new StringBuilder();
			foreach (byte b in hash) {
				stringBuildersb.Append(b.ToString("x2"));
			}
			return stringBuildersb.ToString();
		}
	}
	private bool CheckPasswordsAreEqual(string hashedPassword, string password) {
		return hashedPassword.Equals(hashPassword(password));
	}
	public void SaveScore()
	{
		int currentScore = Pipe.GetScore();
		GD.Print("The current score is: " + currentScore);
		string dbFilePath = "Database.txt";
		var users = new List<User>(); // Initialize the users list

		var lines = File.ReadAllLines(dbFilePath);

		foreach (var line in lines)
		{
			var user = JsonSerializer.Deserialize<User>(line);
			if (user.Email == LoggedInUsername)
			{
				// Update the high score if the current score is higher
				if (currentScore > user.HighScore)
				{
					user.HighScore = currentScore;
				}
			}
			users.Add(user);
		}
		// Serialize the updated user list and write it back to the file
		var updatedLines = users.Select(user => JsonSerializer.Serialize(user)).ToArray();
		File.WriteAllLines(dbFilePath, updatedLines);
	}
	
}
