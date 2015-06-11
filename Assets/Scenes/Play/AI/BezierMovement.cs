using UnityEngine;
using System.Collections;

// Thins class calc the path between two points using the Bezier formula
public class BezierMovement
{

    //---------------------------------
    // Privates variable

    private Vector3 startPosition = Vector3.zero;   // First point
    private Vector3 endPosition = Vector3.zero;     // Last point

    private Vector3 startHandle = Vector3.zero;     // The handle of first point
    private Vector3 endHandle = Vector3.zero;       // The handle of last point

    private Bounds screenBounds = new Bounds();     // The screen bounds

    private float velocity = 0.1f;                  // The velocity of the movement 
    private float currentStep = 0f;                 // Used by itenator


    //---------------------------------
    // Privates methods

    // Find the screen bounds
    private Bounds GetScreenBounds()
    {
        // Get viewport dimensions
        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;

        // Get the bounds
        return new Bounds(new Vector2(width / 2, height / 2), new Vector3(width, height));
    }

    // Calc new position inside the screen
    private Vector3 GenerateRandomPostion()
    {
        // Return random point inside the bounds
        return new Vector3(
            Random.Range(this.screenBounds.min.x, this.screenBounds.max.x),
            Random.Range(this.screenBounds.min.y, this.screenBounds.max.y)
        );
    }


    //---------------------------------
    // Public methods

    // Constructor
    public BezierMovement()
    {
        // Get the screen dimensions
        screenBounds = this.GetScreenBounds();
    }

    // Set the start position
    public void SetStartPosition(Vector3 startPosition)
    {
        this.startPosition = new Vector3(startPosition.x, startPosition.y);
        this.endPosition = Vector3.zero;
    }

    // Add a new random destination point inside the screen
    public void AddDestination()
    {
        // If not the first time
        if (this.endPosition != Vector3.zero)
        {
            // Move the end point to the start
            this.startPosition = new Vector3(this.endPosition.x, this.endPosition.y);
        }

        // Create e new point
        this.endPosition = this.GenerateRandomPostion();

        // Create new handles
        this.startHandle = this.GenerateRandomPostion();
        this.endHandle = this.GenerateRandomPostion();

        // Reset the iterator
        this.currentStep = 0;
    }

    // Get the current position by trasition
    public Vector3 GetPosition(float transition)
    {
        float u = 1.0f - transition;
        float tt = transition * transition;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * transition;

        Vector3 position = uuu * this.startPosition;
        position += 3 * uu * transition * this.startHandle;
        position += 3 * u * tt * this.endHandle;
        position += ttt * this.endPosition;

        return position;
    }

    // Set the step to finish a movement
    public void SetVelocity(float value)
    {
        this.velocity = value;
    }

    // Auto iterator
    public Vector3 Iterator(bool addNewPointWhenFinish)
    {
        // Calculate the distance
        float distance = Vector3.Distance(this.startPosition, this.endPosition);
        distance = Mathf.Abs(distance);

        // Calc the fraction and check the limit
        float fraction = distance > 0 ? this.currentStep / distance : 0;
        if (fraction > 1) fraction = 1;

        // Hold the current position
        Vector3 position = this.GetPosition(fraction);

        // Check if the movement is finish and new point must be add
        if (fraction == 1 && addNewPointWhenFinish)
        {
            // Add new destination
            this.AddDestination();
        }
        else
        {
            // Update the current step
            this.currentStep += this.velocity;
        }

        // Return the position
        return position;
    }

    // Set the custom bounds
    public void SetBounds(Bounds bounds)
    {
        // If empty take the screen bounds
        if (bounds.size.x == 0 || bounds.size.y == 0) {
            bounds = this.GetScreenBounds();
        }

        // Fix the value
        this.screenBounds = bounds;
    }

}
