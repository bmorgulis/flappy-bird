using System;
using System.IO;
using System.Text.Json;

namespace flappy_bird;

public class HighScorePersist
{
    public HighScorePersist()
    {
        int highScore = 0;
    }
    public static void AppendUserToFile(User user, int highScore)
    {
        string filePath = "\"C:\\Users\\bzber\\userDetails.txt\"";
        var userJson = JsonSerializer.Serialize(user);
        File.AppendAllText(filePath, userJson + Environment.NewLine);
    }
}