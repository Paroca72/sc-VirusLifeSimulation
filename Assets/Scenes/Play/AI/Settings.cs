using UnityEngine;
using System.Collections;

public class Settings {

    // ----------------------------------
    // Constants

    public const int TIME_MININCUBATION = 5;            // The minimal time request for incubation
    public const int TIME_INCUBATION = 15;              // The time of incubation seconds
    public const int TIME_TRASMISSIONINFECTION = 3;     // The time used for infect a cell

    public const int MAX_INFECTION = 3;                 // max number of trasmissible infection

    public const int AVERAGE_CELLLIFE = 5 * 60;         // The average cell life in seconds
    public const int DELTA_CELLLIFE = 1 * 60;           // The delta of cell life

    public const int AVERAGE_VIRUSLIFE = 3 * 60;        // The average cell life in seconds
    public const int DELTA_VIRUSLIFE = 1 * 60;          // The delta of cell life

    public const int START_VIRUS = 1;                   // Start with this number of virus on screen
    public const int START_CELLS = 4;                   // Start with this number of cells on screen

    public const int START_PH = 7;                      // The start value of enviroment pH
    public const int START_TEMPERATURE = 22;            // The start value of enviroment temperature

    public const int STOP_MOVEMENT = 0;                 // Stop the movement under this temperature

    public const int MINVALUE_TEMPERATURE = -10;        // The min temperature for wenviroment
    public const int MAXVALUE_TEMPERATURE = 40;         // The max temperature for wenviroment

    public const float VELOCITY_VIRUS = 0.032f;         // The cells velocity on the screen
    public const float VELOCITY_CELLS = 0.004f;         // The cells velocity on the screen
}
