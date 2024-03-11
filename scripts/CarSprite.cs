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
		Vector2 target = GetGlobalMousePosition(); // Get the mouse position in the game world
        Vector2 direction = (target - GlobalPosition).Normalized(); // Calculate the direction to the mouse cursor

        // Rotate the sprite to face towards the mouse cursor
        float angle = direction.Angle(); // Get the angle in radians towards the mouse cursor
        Rotation = angle; // Set the sprite's rotation

        // Move the sprite towards the mouse cursor
        Position += direction * speed * (float)delta;
	}
	
}
