using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public int cardID; // ID para identificar el par
    public Image frontImage; // Imagen frontal de la carta
    public GameObject front; // Parte frontal
    public GameObject back;  // Parte trasera

    private bool isFlipped = false;
    private MemoryGameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<MemoryGameManager>();
    }

    public void OnCardClicked()
    {
        if (!isFlipped && !gameManager.IsBusy) // Evitar clics dobles
        {
            FlipCard();
            gameManager.CardRevealed(this);
        }
    }

    public void FlipCard()
    {
        isFlipped = true;
        transform.DORotate(new Vector3(0, 90, 0), 0.25f).OnComplete(() =>
        {
            back.SetActive(false);
            front.SetActive(true);
            transform.DORotate(new Vector3(0, 0, 0), 0.25f);
        });
    }

    public void HideCard()
    {
        isFlipped = false;
        transform.DORotate(new Vector3(0, 90, 0), 0.25f).OnComplete(() =>
        {
            front.SetActive(false);
            back.SetActive(true);
            transform.DORotate(new Vector3(0, 0, 0), 0.25f);
        });
    }

    public void MatchFound()
    {
        // Animación cuando hay match
        transform.DOScale(1.2f, 0.2f).OnComplete(() =>
        {
            transform.DOScale(0, 0.3f).SetEase(Ease.InBack);
        });
    }
}
