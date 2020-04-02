using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonIntroducao : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    private float clickdelay = 0.5f;
    public AudioSource [] sounds;
    public AudioSource introducao;
    public AudioSource intro;
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
        if (!introducao.isPlaying)
            introducao.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
            {
                introducao.Play();
            }
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
        if(!intro.isPlaying)
            intro.Play();
    }
}
