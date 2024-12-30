using Godot;

public partial class Player : CharacterBody2D
{
    [Export] private int Speed = 5; // Movement speed
    [Export] private float Gravity = 500f; // Gravity strength
    [Export] private float JumpForce = -300f; // Jump strength
    [Export] private float MaxFallSpeed = 800f; // Maximum speed while falling

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        

        // Apply gravity
        if (!IsOnFloor())
        {
            velocity.Y += Gravity * (float)delta;
            velocity.Y = Mathf.Min(velocity.Y, MaxFallSpeed); // Cap fall speed
        
            // velocity.X += Speed;
        }

        // // Horizontal movement
        // Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        // velocity.X = inputDirection.X * Speed;

        // Jumping
        if (Input.IsActionJustPressed("ui_accept") )
        {
            velocity.Y = JumpForce;
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}