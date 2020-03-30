using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonIntroducao : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    float clickdelay = 0.5f;
    public AudioSource[] sounds;
    public AudioSource introducao; // sounds[0]
    public AudioSource intro; // sounds[1]
    private float currentTapTime;
    private float lastTapTime;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        introducao = sounds[0];
        intro = sounds[1];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        intro.Play();
        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            introducao.Play();
        }
        lastTapTime = currentTapTime;
    }

    private bool CheckForDoubleTap(float currentTapTime, float previousTapTime)
    {
        if (currentTapTime - previousTapTime < clickdelay)
        {
            return true;
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        intro.Play();
    }
}
