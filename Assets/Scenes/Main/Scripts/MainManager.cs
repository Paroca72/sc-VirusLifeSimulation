using UnityEngine;
using System.Collections;

public class MainManager : MonoBehaviour {
    //-----------------------------------------
    // Public variables

    public GameObject Virus = null; // Virus prefab


    //-----------------------------------------
    // Private methods

    // Settings the virus parameters
    private void SetVirus(GameObject virus)
    {
        // Mover
        Mover mover = virus.GetComponent<Mover>();
        mover.Velocity = 0.05f;
        mover.MoveRandom();

        // Life
        Life life = virus.GetComponent<Life>();
        life.AverageLife = int.MaxValue;
        life.DeltaLife = 0;
    }

    // Create the elements on screen
    private void Starter() { 
            // Create a single cell
            GameObject virus = (GameObject)Instantiate(this.Virus, Vector2.zero, Quaternion.identity);

            // Settings
            this.SetVirus(virus);
    }


    //-----------------------------------------
    // Events

    // Use this for initialization
	void Start () {
	    // Start the simulation
        this.Starter();
	}

    // Update
    void Update() { 
        // Check for pression
        if (Input.GetMouseButtonDown(0)) { 
            // Load the next scene
            Application.LoadLevel("Play");
        }
    }
}
