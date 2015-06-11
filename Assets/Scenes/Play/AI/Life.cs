using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour
{

    //-----------------------------------------
    // Constants

    private const float FADE_DURATION = 1f;


    //-----------------------------------------
    // Public variables

    public Sprite Death = null;     // The image of death
    public int AverageLife = 0;     // The average life in seconds
    public int DeltaLife = 0;       // The delta life to add to life in seconds


    //-----------------------------------------
    // Private variables

    private Mover mover = null;         // The mover script
    private Renderer render = null;     // Find the mover linked to cell

    private float life = 0.0f;          // The life of virus in seconds
    private bool isDie = false;         // Trigger

    private float fade = 0.0f;          // The fade duration in seconds


    //-----------------------------------------
    // Private methods

    // Die
    private void Die()
    {
        // Check if already die
        if (!isDie)
        {
            // Trigger
            isDie = true;

            // Disable collider
            Collider2D colliderCell = this.GetComponent<Collider2D>();
            colliderCell.enabled = false;

            // Stop the cell move
            mover.Stop();

            // Change image
            SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
            renderer.sprite = Death;

            // Free cell
            Destroy(this.gameObject, FADE_DURATION);

            // Start the fade-out
            this.fade = FADE_DURATION;
        }
    }

    // Apply alpha
    private void ApplyAlpha(float value)
    {
        // Get the color
        Color color = this.render.material.color;

        // Apply alpha
        color.a = value;
        // Apply color
        this.render.material.color = color;
    }


    //-----------------------------------------
    // Public methods

    // Kill
    public void Kill()
    {
        // Set the life to die duration
        this.life = 0;
    }


    //-----------------------------------------
    // Event

    // When created
    void Awake()
    {
        // Initialize
        this.mover = this.GetComponent<Mover>();
        this.render = this.GetComponent<Renderer>();
    }

    // When start
    void Start()
    {
        // Calc the life of this virus
        this.life = this.AverageLife + Random.Range(-this.DeltaLife, this.DeltaLife);
    }

    // Update
    void Update()
    {
        // Subtract the delta time
        this.life -= Time.deltaTime;

        // Check the life
        if (Enviroment.CalcLife(this.life) <= 2 && !this.isDie)
        {
            // Make a die
            this.Die();
        }

        // Fade-out
        if (this.fade > 0) {
            // Subtract the delta time
            this.fade -= Time.deltaTime;

            // Apply alpha out
            this.ApplyAlpha(this.fade);
        }
    }

}
