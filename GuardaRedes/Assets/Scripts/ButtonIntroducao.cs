using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonIntroducao : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    public AudioSource[] sounds;
    public AudioSource introducao;
    public AudioSource intro;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        introducao = sounds[0];
        intro = sounds[1];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            clicktime = Time.time;
            intro.Play();
        }

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            introducao.Play();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        intro.Play();
    }
}
