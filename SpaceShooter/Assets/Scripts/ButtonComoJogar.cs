using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonComoJogar : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource comoJogar;
    public GameObject GameManagerGO;
    private float currentTapTime;
    private float lastTapTime;

    private static bool jogarBackToNormal, introducaoBackToNormal, tempoBackToNormal, pontosBackToNormal, vidasBackToNormal, pontuacaoBackToNormal;
    private static int highlighted;
    private static int soundOn = 0;

    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        comoJogar = GetComponent<AudioSource>();

        highlighted = 0;
        jogarBackToNormal = introducaoBackToNormal = tempoBackToNormal = pontosBackToNormal = vidasBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("ButtonComoJogar").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonJogar.ComoJogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetComoJogarBackToNormal();
        }

        if (ButtonIntroducao.ComoJogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetComoJogarBackToNormal();
        }

        if (ButtonTempo.ComoJogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTempo.ResetComoJogarBackToNormal();
        }

        if (ButtonVidas.ComoJogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonVidas.ResetComoJogarBackToNormal();
        }

        if (ButtonPontos.ComoJogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontos.ResetComoJogarBackToNormal();
        }

        if (ButtonPontuacao.ComoJogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontuacao.ResetComoJogarBackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (comoJogar.isPlaying)
                comoJogar.Stop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!comoJogar.isPlaying)
            comoJogar.Play();

        
        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (PlayerControlSwipe.GetInstrucoesCancelAction())
            {
                PlayerControlSwipe.ResetInstrucoesCancelAction();
            }
            else
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instructions);
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
        if (ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (ButtonTempo.CheckForHighlighted() == 1)
        {
            tempoBackToNormal = true;
        }

        if (ButtonVidas.CheckForHighlighted() == 1)
        {
            vidasBackToNormal = true;
        }

        if (ButtonPontos.CheckForHighlighted() == 1)
        {
            pontosBackToNormal = true;
        }

        if (ButtonPontuacao.CheckForHighlighted() == 1)
        {
            pontuacaoBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!comoJogar.isPlaying)
        {
            if (ButtonJogar.GetSoundOn() == 1)
            {
                ButtonJogar.SetSoundOn();
                comoJogar.Play();
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 0)
            {
                comoJogar.Play();
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 1)
            {
                ButtonTempo.SetSoundOn();
                comoJogar.Play();
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 0)
            {
                comoJogar.Play();
                soundOn = 1;
            }

            if (ButtonPontos.GetSoundOn() == 1)
            {
                ButtonPontos.SetSoundOn();
                comoJogar.Play();
                soundOn = 1;
            }

            if (ButtonPontos.GetSoundOn() == 0)
            {
                comoJogar.Play();
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 1)
            {
                ButtonVidas.SetSoundOn();
                comoJogar.Play();
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 0)
            {
                comoJogar.Play();
                soundOn = 1;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public static int GetSoundOn()
    {
        return soundOn;
    }

    public static void SetSoundOn()
    {
        soundOn = 0;
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static void ResetJogarBackToNormal()
    {
        jogarBackToNormal = false;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void ResetIntroducaoBackToNormal()
    {
        introducaoBackToNormal = false;
    }

    public static bool TempoBackToNormal()
    {
        return tempoBackToNormal;
    }

    public static void ResetTempoBackToNormal()
    {
        tempoBackToNormal = false;
    }

    public static bool PontosBackToNormal()
    {
        return pontosBackToNormal;
    }

    public static void ResetPontosBackToNormal()
    {
        pontosBackToNormal = false;
    }

    public static bool VidasBackToNormal()
    {
        return vidasBackToNormal;
    }

    public static void ResetVidasBackToNormal()
    {
        vidasBackToNormal = false;
    }

    public static bool PontuacaoBackToNormal()
    {
        return pontuacaoBackToNormal;
    }

    public static void ResetPontuacaoBackToNormal()
    {
        pontuacaoBackToNormal = false;
    }
}
