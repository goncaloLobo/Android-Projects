using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPontos : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    float clicked = 0;
    public AudioSource pontos;
    public GameObject GameManagerGO;

    void Start()
    {
        pontos = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            if(!pontos.isPlaying)
                pontos.Play();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!pontos.isPlaying)
            pontos.Play();
    }
}
