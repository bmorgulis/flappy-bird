using System.Reflection;
using Godot;

namespace flappy_bird;

public class BirdSpeedTestWithReflection
{
	 public static void TestBirdSpeed()
	 {
		  var bird = new Player();

		  FieldInfo birdSpeed =
			   typeof(Player).GetField("Speed",
					BindingFlags.NonPublic | BindingFlags.Instance); // Get the Speed field of the private field

		  if (birdSpeed != null)
		  {
			   int currentSpeed = (int)birdSpeed.GetValue(bird); // Get the current private speed of bird
			   GD.Print("current speed of bird is: " + currentSpeed);
			   
			   //change the speed of bird
			   birdSpeed.SetValue(bird, 100);
			   
			   //check if it changed
			   int newSpeed = (int)birdSpeed.GetValue(bird);
			   GD.Print("new speed of bird is: " + newSpeed);
			   
		  }
	 }
}
