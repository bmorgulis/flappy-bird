using System;
using System.IO;
using System.Text.Json;

namespace flappy_bird;

public class  User {
    public string Email { get; set; }
    public string Password { get; set; }
    public int HighScore { get; set; } = 0;
    public User(string email, string password) {
        Email = email;
        Password = password;
    }
    
    private string UserExists(string email, string filePath) {
        var lines = File.ReadLines(filePath);
        foreach (var line in lines) {
            var user = JsonSerializer.Deserialize<User>(line);
            if (user.Email == email) {
                return user.Password;
            }
        }
        return null;
    }
}