using UnityEngine;
using System.Collections;

public class PlayManager : MonoBehaviour {

    //-----------------------------------------
    // Public variables

    public GameObject Virus = null; // Virus prefab
    public GameObject Cell = null;  // Cell prefabs


    //-----------------------------------------
    // Private variables

    private Bounds screenBounds = new Bounds();     // The screen bounds


    //-----------------------------------------
    // Private methods

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

    // Settings the cell parameters
    private void SetCell(GameObject cell)
    {
        // Mover
        Mover mover = cell.GetComponent<Mover>();
        mover.Velocity = Settings.VELOCITY_CELLS;
        mover.MoveRandom();

        // Life
        Life life = cell.GetComponent<Life>();
        life.AverageLife = Settings.AVERAGE_CELLLIFE;
        life.DeltaLife = Settings.DELTA_CELLLIFE;
    }

    // Settings the virus parameters
    private void SetVirus(GameObject virus)
    {
        // Mover
        Mover mover = virus.GetComponent<Mover>();
        mover.Velocity = Settings.VELOCITY_VIRUS;
        mover.MoveRandom();

        // Life
        Life life = virus.GetComponent<Life>();
        life.AverageLife = Settings.AVERAGE_VIRUSLIFE;
        life.DeltaLife = Settings.DELTA_VIRUSLIFE;

        // Enable collider
        Collider2D collider = virus.GetComponent<Collider2D>();
        collider.enabled = true;

        // Change tag
        virus.tag = "Virus";
    }

    // Create the elements on screen
    private void Starter() { 
        // Create the cells
        for (int index = 0; index < Settings.START_CELLS; index++) { 
            // Create a single cell
            GameObject cell = (GameObject) Instantiate(this.Cell, this.GenerateRandomPostion(), Quaternion.identity);

            // Settings
            this.SetCell(cell);
        }

        // Create the virus
        for (int index = 0; index < Settings.START_VIRUS; index++)
        {
            // Create a single cell
            GameObject virus = (GameObject)Instantiate(this.Virus, this.GenerateRandomPostion(), Quaternion.identity);

            // Set paramenters
            this.SetVirus(virus);
        }
    }


    //-----------------------------------------
    // Public methods

    // Insert a new cell on the stage
    public void InsertCell() {
        // Create a single cell
        GameObject cell = (GameObject)Instantiate(this.Cell, this.GenerateRandomPostion(), Quaternion.identity);

        // Settings
        this.SetCell(cell);
    }

    // Return at the main scene
    public void Exit() {
        Application.LoadLevel("Main");
    }


    //-----------------------------------------
    // Events

    // Use this for initialization
	void Start () {
        // Set the seed
        Random.seed = (int)System.DateTime.Now.Ticks;

        // Get the screen bounds
        this.screenBounds = this.GetScreenBounds();

        // Reset the enviroment values and enable it
        Enviroment.ResetValues();
        Enviroment.enabled = true;

        // Start the simulation
        this.Starter();
	}
	
}
