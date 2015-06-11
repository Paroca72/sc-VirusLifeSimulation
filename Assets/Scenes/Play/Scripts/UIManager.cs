using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    //-----------------------------------------
    // Private variables

    private Slider pHSlider = null;             // The text control relative at pH
    private Slider temperatureSlider = null;    // The text control relative at temperature
    private Text pHValue = null;                // The text control relative at pH
    private Text temperatureValue = null;       // The text control relative at temperature
    private Text timerValue = null;             // The text control relative at life timer
    private Text populationValue = null;        // The text control relative at population
    private Button insertButton = null;         // The button control relative at new cell insert
    private Image gameOverImage = null;         // The image control relative at the finish of the game

    private long startTime = 0;                 // Hold the start time


    //-----------------------------------------
    // Private methods

    // Find all controls on UI
    private void Finder() { 
        // Find all texts
        Component[] texts = this.GetComponentsInChildren(typeof(Text), true);

        // Analize the result
        foreach (Text text in texts)
        { 
            // Select case and assign the relative variable
            switch (text.name.ToString())
            {
                case "pH Value": this.pHValue = text; break;
                case "Temperature Value": this.temperatureValue = text; break;
                case "Timer Value": this.timerValue = text; break;
                case "Population Value": this.populationValue = text; break;
            }
        }

        // Find all sliders
        Component[] sliders = this.GetComponentsInChildren(typeof(Slider), true);

        // Analize the result
        foreach (Slider slider in sliders)
        {
            // Select case and assign the relative variable
            switch (slider.name.ToString())
            {
                case "pH Slider": this.pHSlider = slider; break;
                case "Temperature Slider": this.temperatureSlider = slider; break;
            }
        }

        // Find all buttons
        Component[] buttons = this.GetComponentsInChildren(typeof(Button), true);

        // Analize the result
        foreach (Button button in buttons)
        {
            // Select case and assign the relative variable
            switch (button.name.ToString())
            {
                case "Insert Button": this.insertButton = button; break;
            }
        }

        // Find all images
        Component[] images = this.GetComponentsInChildren(typeof(Image), true);

        // Analize the result
        foreach (Image image in images)
        {
            // Select case and assign the relative variable
            switch (image.name.ToString())
            {
                case "Game Over Image": this.gameOverImage = image; break;
            }
        }
    }

    // Disable all input controls
    private void DisableInputControls() { 
        // Buttons
        this.insertButton.interactable = false;

        // Sliders
        this.pHSlider.interactable = false;
        this.temperatureSlider.interactable = false;
    }

    // Make a game over
    private void GameOver() {
        // Disable all input controls
        this.DisableInputControls();

        // Show the game over image
        this.gameOverImage.enabled = true;
    }


    //-----------------------------------------
    // Events

    // Use this for initialization
	void Awake () {
	    // Find all controls
        this.Finder();
	}

    void Start() { 
        // Save the current time
        this.startTime = System.DateTime.Now.Ticks;
    }
	
    // When the UI need to update
    void OnGUI() { 
        // Update the sliders value
        this.pHValue.text = "( " + this.pHSlider.value + " )";
        this.temperatureValue.text = "( " + this.temperatureSlider.value + " )";

        // Calc the elapsed time
        long deltaTime = System.DateTime.Now.Ticks - this.startTime;
        System.DateTime time = new System.DateTime(deltaTime);
        int milliseconds = Mathf.RoundToInt(time.Millisecond / 10);

        // Update the population value
        GameObject[] population = GameObject.FindGameObjectsWithTag("Virus");
        this.populationValue.text = "Virus Population: " + population.Length.ToString();

        // Check if game is finish
        if (population.Length == 0)
        {
            // Game over
            this.GameOver();
        }
        else {
            // Update the timer value
            this.timerValue.text = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Minute, time.Second, milliseconds);
        }
    }
}
