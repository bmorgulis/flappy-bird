using System.Runtime.CompilerServices;

public class  User {
	public string Email { get; set; }
	public string Password { get; set; }
	public int HighScore { get; set; } = 0;
	public User(string email, string password) {
		Email = email;
		Password = password;
	}
}
