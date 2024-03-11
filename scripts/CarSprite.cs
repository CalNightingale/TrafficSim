using Godot;
using System;

public partial class CarSprite : AnimatedSprite2D
{
	[Export]
	private int speed = 100; // Speed at which the player moves

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 velocity = new Vector2(); // Vector to store the movement direction

        // Check for input and adjust the velocity accordingly
        if (Input.IsActionPressed("ui_right")) // For the right arrow key
        {
            velocity.X += 1;
        }
        if (Input.IsActionPressed("ui_left")) // For the left arrow key
        {
            velocity.X -= 1;
        }
        if (Input.IsActionPressed("ui_down")) // For the down arrow key
        {
            velocity.Y += 1;
        }
        if (Input.IsActionPressed("ui_up")) // For the up arrow key
        {
            velocity.Y -= 1;
        }

        // Normalize velocity to ensure consistent movement in all directions
        velocity = velocity.Normalized() * speed;

        // Move the sprite
        Position += velocity * (float) delta;

        // Optional: Change animation based on direction
        //CarSprite sprite = GetNode<CarSprite>("CarSprite"); // Adjust the path if necessary
	}
	
}
