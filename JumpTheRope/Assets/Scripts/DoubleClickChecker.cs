using UnityEngine;

public class DoubleClickChecker : MonoBehaviour
{
    private bool tap, doubleTap;
    private Vector2 startTouch;
    private float doubleTapDelta = 0.5f;
    private float lastTap;

    public AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play();
    }

    void Update()
    {
        tap = doubleTap = false;

#if UNITY_EDITOR
        Update_Standalone();
#else
        Update_Mobile();
#endif
    }

    private void Update_Standalone()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
            doubleTap = Time.time - lastTap < doubleTapDelta;
            lastTap = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = Vector2.zero;
        }

        if (tap)
            Debug.Log("tap!!");

        if (doubleTap)
            Debug.Log("doubletap!!");
    }

    private void Update_Mobile()
    {
        if(Input.touches.Length != 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.touches[0].position;
                doubleTap = Time.time - lastTap < doubleTapDelta;
                lastTap = Time.time;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = Vector2.zero;
            }

            if (tap)
                Debug.Log("tap!!");

            if (doubleTap)
                Debug.Log("doubletap!!");
        }
    }

    private void Reset()
    {
        startTouch = Vector2.zero;
    }
}
