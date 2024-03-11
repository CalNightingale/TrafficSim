using Godot;
using System;

[Tool] // This enables the script to run in the editor
public partial class Road : Node2D
{
    [Export]
    public int NumLanes = 2; // Number of lanes, with 2 as default
    [Export]
    public float LaneWidth = 50.0f; // The width of each lane
    [Export]
    public Vector2 StartPoint = new Vector2(100, 100); // Starting point of the road
    [Export]
    public Vector2 EndPoint = new Vector2(400, 100); // Ending point of the road, defaulting to a straight line
    [Export]
    public Color RoadColor = new Color(0.2f, 0.2f, 0.2f);

    private struct Lane {
        public Lane(double direction)
        {
            Direction = direction;
        }

        public double Direction { get; }

        public override string ToString() => $"Lane in direction {Direction}";
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public override void _Draw()
    {
        // Calculate the total road width based on the number of lanes and lane width
        float roadWidth = NumLanes * LaneWidth;

        // Calculate the road direction and perpendicular
        Vector2 roadDirection = (EndPoint - StartPoint).Normalized();
        Vector2 roadPerpendicular = new Vector2(-roadDirection.Y, roadDirection.X) * roadWidth / 2.0f;

        // Draw the road background
        DrawRect(new Rect2(StartPoint - roadPerpendicular, EndPoint - StartPoint + roadPerpendicular * 2), RoadColor);

        // Draw lanes
        for (int i = 1; i < NumLanes; i++)
        {
            Vector2 laneOffset = roadPerpendicular * (2 * i / (float)NumLanes - 1);
            DrawLine(StartPoint + laneOffset, EndPoint + laneOffset, new Color(1, 1, 1));
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // Only redraw in the editor to visualize changes live
        if (Engine.IsEditorHint())
        {
            QueueRedraw();
        }
    }
}
