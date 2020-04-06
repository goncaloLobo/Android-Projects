using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCorda3 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource[] sounds;
    public AudioSource salto2pernas;
    public AudioSource descricao;

    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static int highlighted;
    private Image mImage;
    private bool check;
    private static bool jogarBackToNormal, instrucoesBackToNormal, introducaoBackToNormal, corda1BackToNormal, corda2BackToNormal, corda3BackToNormal, tutorialBackToNormal;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        salto2pernas = sounds[0];
        descricao = sounds[1];

        highlighted = 0;
        check = false;
        jogarBackToNormal = instrucoesBackToNormal = introducaoBackToNormal = corda1BackToNormal = corda2BackToNormal = corda3BackToNormal = tutorialBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("CordaButton3").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonCorda1.Corda3BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda2.Corda3BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonJogar.Corda3BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonInstrucoes.Corda3BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonIntroducao.ButtonCorda3BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (Tutorial.Corda3BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        check = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!descricao.isPlaying)
        {
            descricao.Play();
            salto2pernas.PlayDelayed(descricao.clip.length);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if(ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if(ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if(ButtonCorda1.CheckForHighlighted() == 1)
        {
            corda1BackToNormal = true;
        }

        if (ButtonCorda2.CheckForHighlighted() == 1)
        {
            corda2BackToNormal = true;
        }

        if (Tutorial.CheckForHighlighted() == 1)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!descricao.isPlaying)
        {
            descricao.Play();
            salto2pernas.PlayDelayed(descricao.clip.length);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        descricao.Stop();
        salto2pernas.Stop();
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
    }

    public static bool Corda1BackToNormal()
    {
        return corda1BackToNormal;
    }

    public static bool Corda2BackToNormal()
    {
        return corda2BackToNormal;
    }

    public static bool Corda3BackToNormal()
    {
        return corda3BackToNormal;
    }

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
    }
}
