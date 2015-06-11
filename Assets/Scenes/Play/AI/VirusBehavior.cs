using UnityEngine;
using System.Collections;

public class VirusBehavior : MonoBehaviour
{

    //-----------------------------------------
    // Private variables

    private Mover mover = null;                 // The virus move manager
    private CellBehavior targetBehavior = null; // The target behavior script
    private float timer = 0.0f;                 // The infection duration


    //-----------------------------------------
    // Event

    // Awake
    void Awake()
    {
        // Initialize
        this.mover = this.GetComponent<Mover>();
    }

    // Update
    void Update()
    {
        // Animate the timer
        if (this.timer > 0)
        {
            // Subtract the delta time
            this.timer -= Time.deltaTime;

            // Try to infect
            if (timer <= 0)
            {
                // Pass the infection and pass as random number of infection
                this.targetBehavior.Infect(Random.Range(0, Settings.MAX_INFECTION));
                // Start random movement
                this.mover.MoveRandom();
            }
        }
    }

    // When have a collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collider is a Virus
        if (collision.collider.tag == "Cell")
        {
            // Find the behavior script
            this.targetBehavior = collision.collider.gameObject.GetComponent<CellBehavior>();

            // Check if not is already infected
            if (!this.targetBehavior.IsInfected())
            {
                // Copy the cell position
                this.mover.MoveCopy(collision.collider.gameObject);

                // Start timer
                this.timer = Settings.TIME_TRASMISSIONINFECTION;
            }
        }
    }

}
