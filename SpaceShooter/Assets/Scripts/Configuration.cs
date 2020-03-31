using UnityEngine;

public class Configuration : MonoBehaviour
{
    // velocidade minima esperada para um swipe
    private const int MINIMUM_FLING_VELOCITY = 50;

    // diferença de tempo máxima para ser considerado um doubletap perfeito
    private static float DOUBLE_TAP_DELTA = 0.3f;

    // distancia máxima do circulo de doubletap possivel à volta de um tap
    private const int DOUBLE_TAP_RADIUS = 100;

    private const int MAX_LIVES = 3;

    public static float DoubleTapDelta()
    {
        return DOUBLE_TAP_DELTA;
    }

    public static int MinimumFlingVelocity()
    {
        return MINIMUM_FLING_VELOCITY;
    }

    public static int DoubleTapRadius()
    {
        return DOUBLE_TAP_RADIUS;
    }

    public static int MaxLives()
    {
        return MAX_LIVES;
    }
}
