using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonVidas : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource [] sounds;
    public AudioSource vidas; //sounds[0]
    public AudioSource vidas1; //sounds[1]
    public AudioSource vidas2; //sounds[2]
    public AudioSource vidas3; //sounds[3]

    public GameObject GameManagerGO;
    private static bool comoJogarBackToNormal, introducaoBackToNormal, jogarBackToNormal, pontosBackToNormal, tempoBackToNormal;
    private static int highlighted;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        vidas = sounds[0];
        vidas1 = sounds[1];
        vidas2 = sounds[2];
        vidas3 = sounds[3];

        highlighted = 0;
        comoJogarBackToNormal = introducaoBackToNormal = jogarBackToNormal = pontosBackToNormal = tempoBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("LivesTag").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonComoJogar.VidasBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonIntroducao.VidasBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonJogar.VidasBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonTempo.VidasBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
        {
            if (!vidas.isPlaying)
                vidas.Play();
        }

        if (GameManager.GetCurrentState() == GameManager.GameManagerState.Gameplay)
        {
            switch (PlayerControlSwipe.GetCurrentNumberOfLives())
            {
                case 3:
                    if (!vidas3.isPlaying)
                        vidas3.Play();
                    break;
                case 2:
                    if (!vidas2.isPlaying)
                        vidas2.Play();
                    break;
                case 1:
                    if (!vidas1.isPlaying)
                        vidas1.Play();
                    break;
            }
        }
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ButtonComoJogar.CheckForHighlighted() == 1)
        {
            comoJogarBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (ButtonJogar.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if(ButtonTempo.CheckForHighlighted() == 1)
        {
            tempoBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
        {
            if (!vidas.isPlaying)
                vidas.Play();
        }

        if (GameManager.GetCurrentState() == GameManager.GameManagerState.Gameplay)
        {
            switch (PlayerControlSwipe.GetCurrentNumberOfLives())
            {
                case 3:
                    if (!vidas3.isPlaying)
                        vidas3.Play();
                    break;
                case 2:
                    if (!vidas2.isPlaying)
                        vidas2.Play();
                    break;
                case 1:
                    if (!vidas1.isPlaying)
                        vidas1.Play();
                    break;
            }
        }
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static bool TempoBackToNormal()
    {
        return tempoBackToNormal;
    }

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static bool ComoJogarBackToNormal()
    {
        return comoJogarBackToNormal;
    }

    public static bool PontosBackToNormal()
    {
        return pontosBackToNormal;
    }
}
