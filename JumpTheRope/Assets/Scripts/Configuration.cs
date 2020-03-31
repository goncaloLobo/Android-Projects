using UnityEngine;

public class Configuration : MonoBehaviour
{
    // pontos para um salto perfeito
    private const int PERFECT_JUMP = 100;

    // pontos para um salto normal
    private const int NORMAL_JUMP = 25;

    // diferença de tempo máxima para ser considerado um doubletap perfeito
    private static float DOUBLE_TAP_DELTA = 0.3f;

    // diferença de tempo para ser considerado um doubletap normal
    private static float DOUBLE_TAP_DELTA_BIGGER = 1.0f;

    // distancia máxima do circulo de doubletap possivel à volta de um tap
    private const int DOUBLE_TAP_RADIUS = 100;

    public static int PerfectJump()
    {
        return PERFECT_JUMP;
    }

    public static int NormalJump()
    {
        return NORMAL_JUMP;
    }

    public static float DoubleTapDelta()
    {
        return DOUBLE_TAP_DELTA;
    }

    public static float DoubleTapDeltaBigger()
    {
        return DOUBLE_TAP_DELTA_BIGGER;
    }

    public static int DoubleTapRadius()
    {
        return DOUBLE_TAP_RADIUS;
    }
}
