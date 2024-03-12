using Godot;
using System;

[Tool]
public partial class Car : AnimatedSprite2D
{
	[Export]
	private int speed = 100; // car speed (pixels/sec)
	private float _movementAngle = 0; // angle at which car moves (radians)
	[Export]
	public float MovementAngle
    {
        get => _movementAngle;
        set
        {
			SetMovementAngle(value);
        }
    }

	// Method to change movement angle and update rotation accordingly
    public void SetMovementAngle(float angle)
    {
        _movementAngle = angle;
        Rotation = angle;
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetMovementAngle(_movementAngle);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Calculate the direction vector based on the angle
    	Vector2 direction = new Vector2((float)Math.Cos(_movementAngle), (float)Math.Sin(_movementAngle));

    	// Move the car in the direction of movementAngle by speed * delta
    	Position += direction * speed * (float)delta;
	}
	
}
