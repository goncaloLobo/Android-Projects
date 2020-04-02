using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTempo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    float clicked = 0;
    public AudioSource tempo;

    void Start()
    {
        tempo = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            if(!tempo.isPlaying)
                tempo.Play();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!tempo.isPlaying)
            tempo.Play();
    }
}
