using UnityEngine;
using System.Collections;

public class Enviroment {

    //---------------------------------------
    // Public variables

    public static int pH = Settings.START_PH;
    public static int temperature = Settings.START_TEMPERATURE;

    public static bool enabled = true;


    //---------------------------------------
    // Public methods

    // Reset the enviroment values
    public static void ResetValues() {
        Enviroment.pH = Settings.START_PH;
        Enviroment.temperature = Settings.START_TEMPERATURE;
    }

    // Calc the enviroment contribute of temperature and pH
    public static float CalcLife(float life)
    {
        // Check if enabled
        if (!Enviroment.enabled) return life;

        // Calc the multipler factor for temperature
        float tFraction = Mathf.Exp((Settings.START_TEMPERATURE - Enviroment.temperature) * 0.3f);

        // Calc the multipler factor for pH
        float pFraction = 1 - 4f * Mathf.Pow(((float)Enviroment.pH / 14f) - 0.5f, 2);

        // The calc and the limit
        float calc = life * tFraction * pFraction;
        if (calc < 0) calc = 0;

        // return
        return calc;
    }

    // Calc the enviroment contribute to velocity
    public static float CalcVelocity(float velocity)
    {
        // Check if enabled
        if (!Enviroment.enabled) return velocity;

        // If temperature is under the threshold stop the movement
        if (Enviroment.temperature < Settings.STOP_MOVEMENT)
        {
            return 0f;
        }

        // Calc the multipler factor
        int delta = Settings.MAXVALUE_TEMPERATURE - Settings.MINVALUE_TEMPERATURE;
        float fraction = (float)Enviroment.temperature / (float)delta;

        // return the calc
        return velocity * fraction;
    }

    // Calc the enviroment contribute to incubation
    public static float CalcIncubation() {
        // Check if enabled
        if (!Enviroment.enabled) return Settings.TIME_INCUBATION;

        // Calc the multipler factor for pH
        float fraction = (float)Enviroment.pH / 7;

        // Calc the time and limit
        float calc = Settings.TIME_INCUBATION * fraction;
        if (calc < Settings.TIME_MININCUBATION) calc = Settings.TIME_MININCUBATION;

        // return
        return calc;
    }

}
