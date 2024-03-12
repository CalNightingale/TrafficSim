using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

[Tool] // This enables the script to run in the editor
public partial class Road : Node2D
{
    private int _numLanes = 2;
    [Export]
    public int NumLanes
    {
        get => _numLanes;
        set
        {
            if (_numLanes != value)
            {
                _numLanes = value;
                InitializeLanes(); // Initialize or reinitialize lanes when NumLanes changes
                QueueRedraw(); // And we likely need to redraw
            }
        }
    }

    [Export]
    public float LaneWidth = 50.0f; // The width of each lane
    [Export]
    public Vector2 StartPoint = new Vector2(0, 400); // Starting point of the road
    [Export]
    public Vector2 EndPoint = new Vector2(1920, 400); // Ending point of the road, defaulting to a straight line
    [Export]
    public Color RoadColor = new Color(0.2f, 0.2f, 0.2f);
    private List<Lane> lanes = new List<Lane>();
    private PackedScene carScene = GD.Load<PackedScene>("res://car.tscn");


    private struct Lane {
        public Lane(int laneNum, bool direction)
        {
            LaneNum = laneNum;
            Direction = direction;
            Cars = new List<Car>();
        }

        public int LaneNum { get; }

        public bool Direction { get; } // true if start->end, false otherwise

        public List<Car> Cars { get; set; }

        public override string ToString() => $"Lane in direction {Direction}";
    }

    // Call this method to initialize lanes based on the current NumLanes value
    private void InitializeLanes()
    {
        lanes.Clear();
        float laneOffsetY = 0f;

        for (int i = 0; i < NumLanes; i++)
        {
            bool dir = i <= NumLanes / 2;
            var lane = new Lane(i, dir);
            lanes.Add(lane);
            if (!Engine.IsEditorHint()) SpawnCar(lane);
        }
    }

    private void SpawnCar(Lane lane)
    {
        Car carInstance = (Car) carScene.Instantiate();
        AddChild(carInstance); // Add the car instance to the Road node for it to be rendered and processed.
            
        // Calculate the car's initial position. Adjust the laneOffsetY as per your requirement.
        float yPos = StartPoint.Y + (lane.LaneNum + 1) * LaneWidth - (LaneWidth / 2);
        Vector2 carPosition = new Vector2(StartPoint.X, yPos);
        carInstance.GlobalPosition = carPosition;

        lane.Cars.Add(carInstance);

    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        InitializeLanes(); // Ensure lanes are initialized upon instantiation
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
        foreach (Lane lane in lanes)
        {
            Vector2 laneOffset = roadPerpendicular * (2 * (lane.LaneNum+1) / (float)NumLanes - 1);
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