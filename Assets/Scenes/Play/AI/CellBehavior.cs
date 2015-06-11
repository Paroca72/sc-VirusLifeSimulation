using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CellBehavior : MonoBehaviour
{

    //-----------------------------------------
    // Constants

    private const float START_INFECTION_SCALE = 0.2f;


    //-----------------------------------------
    // Public variables

    public GameObject Virus = null; // The prefab reference


    //-----------------------------------------
    // Private variables

    private List<GameObject> infections = null; // The list of infections
    private float incubationTime = 0;           // The incubation duration in seconds


    //-----------------------------------------
    // Private methods

    // Create one infection and return it
    private GameObject CreateInfection()
    {
        // Create a copy of the current cell position
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        // Create the instance of virus
        GameObject virus = (GameObject)Instantiate(Virus, currentPosition, Quaternion.identity);

        // Life
        Life life = virus.GetComponent<Life>();
        life.AverageLife = Settings.AVERAGE_VIRUSLIFE;
        life.DeltaLife = Settings.DELTA_VIRUSLIFE;

        // Change the owner and tag
        virus.transform.parent = this.transform;
        virus.tag = "Virus";

        // Set alpha
        Renderer renderer = virus.GetComponent<Renderer>();
        renderer.material.color = new Color(1.0f, 1.0f, 1.0f);

        // Find the mover script and set to copy position
        Mover mover = virus.GetComponent<Mover>();
        mover.Velocity = Settings.VELOCITY_VIRUS * 4;
        mover.MoveLocal();

        // Now scale (cannot make before)
        virus.transform.localScale = new Vector2(START_INFECTION_SCALE, START_INFECTION_SCALE);

        // return
        return virus;
    }

    // Create n infections and store it
    private void CreateInfections(int number)
    {
        // Reset the holder
        this.infections.Clear();

        // Cycle
        for (int index = 0; index < number; index++)
        {
            // Create the single infection
            GameObject infection = this.CreateInfection();
            // Store it
            this.infections.Add(infection);
        }

        // Start incubation
        this.incubationTime = Settings.TIME_INCUBATION;
    }

    // Update infections
    private void UpdateInfections()
    {
        // Calc the progress
        float progress = (Settings.TIME_INCUBATION - this.incubationTime) / Settings.TIME_INCUBATION;

        // Fix progress
        if (progress < 0) progress = 0;
        if (progress > 1) progress = 1;

        // Only if bigger
        if (progress > START_INFECTION_SCALE)
        {
            // Set the new scale for all infections
            foreach (GameObject infection in this.infections)
            {
                // Scale current
                infection.transform.localScale = new Vector2(progress, progress);
            }
        }
    }

    // Make the virus free
    private void FreeVirus()
    {
        // Cycle all infections
        foreach (GameObject infection in this.infections)
        {
            // Free parent
            infection.transform.parent = null;

            // Start virus random movement
            Mover mover = infection.GetComponent<Mover>();
            mover.Velocity = Settings.VELOCITY_VIRUS;
            mover.MoveRandom();

            // Enable collider
            Collider2D collider = infection.GetComponent<Collider2D>();
            collider.enabled = true;

            // Set the tag
            infection.tag = "Virus";
        }
    }


    //-----------------------------------------
    // Public

    // Pass the number of infections
    public void Infect(int number)
    {
        // Check the number of infection and if already infected
        if (!this.IsInfected() && number > 0)
        {
            // Create the infections
            this.CreateInfections(number);
        }
    }

    // Get if is infected
    public bool IsInfected()
    {
        return this.infections.Count > 0;
    }


    //-----------------------------------------
    // Event

    void Awake()
    {
        // Initialize
        this.infections = new List<GameObject>();
    }

    // Update
    void Update()
    {
        // Animate the timer
        if (this.incubationTime > 0)
        {
            // Subtract the delta time
            this.incubationTime -= Time.deltaTime;

            // Update infections
            if (this.IsInfected()) this.UpdateInfections();

            // Check the life
            if (this.incubationTime <= 0)
            {
                // Free the virus
                this.FreeVirus();

                // Find the life manager and self kill
                Life life = this.GetComponent<Life>();
                life.Kill();
            }
        }
    }

}
