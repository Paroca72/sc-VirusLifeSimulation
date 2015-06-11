using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{

    // -------------------------------------
    // Public variables

    public float Velocity = 0f;


    // -------------------------------------
    // Private variables

    // Define the types of movement
    private enum MovementTypes
    {
        NONE,
        RANDOM,
        COPY,
        LOCAL
    }

    private MovementTypes mode = MovementTypes.NONE;    // Define the current type of movement
    private Rigidbody2D rigidBody = null;               // The rigid body
    private BezierMovement bmv = null;           // Manager for smooth movement
    private GameObject targetToCopy = null;             // The object to copy

    private float xOffset = 0.0f;                       // Position x offset
    private float yOffset = 0.0f;                       // Position y offset


    // -------------------------------------
    // Public methods

    // Start the random movement
    public void MoveRandom()
    {
        // Reset the bounds
        this.bmv.SetBounds(new Bounds());

        // Create the first path
        this.bmv.SetStartPosition(transform.position);
        this.bmv.AddDestination();

        // Reset the mode
        this.mode = MovementTypes.RANDOM;
    }

    // Move the object relative at parent
    public void MoveLocal()
    {
        // Get the renderer
        Renderer renderer = this.GetComponent<Renderer>();

        // Fix the bounds
        Bounds bounds = renderer.bounds;
        bounds.center = Vector2.zero;

        // Assign
        this.bmv.SetBounds(bounds);

        // Create the first path
        this.bmv.SetStartPosition(transform.position);
        this.bmv.AddDestination();
        this.bmv.AddDestination();

        // Reset the mode
        this.mode = MovementTypes.LOCAL;
    }

    // Set the object to copy the position
    public void MoveCopy(GameObject target)
    {
        // Save the target
        this.targetToCopy = target;

        // Save the offset
        this.xOffset = target.transform.position.x - transform.position.x;
        this.yOffset = target.transform.position.y - transform.position.y;

        // Change the mode
        this.mode = MovementTypes.COPY;
    }

    // Stop copy position
    public void Stop()
    {
        // Stop the movement
        this.rigidBody.velocity = Vector2.zero;

        // Reset the mode
        this.mode = MovementTypes.NONE;
    }


    // -------------------------------------
    // Events

    // When the object is create
    void Awake()
    {
        // Create the bezier manager and init it
        this.bmv = new BezierMovement();

        // Hold the rigid body
        this.rigidBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calc velocity by enviroment status and assign to bezier
        float velocity = Enviroment.CalcVelocity(this.Velocity);
        this.bmv.SetVelocity(velocity);

        // Select case
        switch (this.mode)
        {
            case MovementTypes.NONE:
                // Do nothing
                break;

            case MovementTypes.RANDOM:
                // Find the next position
                Vector2 globalPosition = this.bmv.Iterator(true);
                // Move the current object
                this.rigidBody.MovePosition(new Vector2(globalPosition.x, globalPosition.y));
                break;

            case MovementTypes.LOCAL:
                // Find the next position
                Vector2 localPosition = this.bmv.Iterator(true);
                // Move the current object
                this.transform.localPosition = new Vector2(localPosition.x, localPosition.y);
                break;

            case MovementTypes.COPY:
                // Check if object to copy already exists
                if (this.targetToCopy != null)
                {
                    // Copy the position of the target
                    this.rigidBody.MovePosition(
                        new Vector2(
                            this.targetToCopy.transform.position.x - this.xOffset,
                            this.targetToCopy.transform.position.y - this.yOffset
                        )
                    );
                }
                break;
        }
    }

}
