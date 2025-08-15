using UnityEngine;
using System.Collections;
using TMPro;

public class MemoryGameManager : MonoBehaviour
{
    public bool IsBusy { get; private set; } = false;

    private Card firstCard;
    private Card secondCard;

    [Header("Timer Settings")]
    public float timeRemaining = 60f; // Tiempo en segundos
    public TextMeshProUGUI timerText; // Referencia al texto TMP
    private bool timerRunning = true;

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                GameOver();
            }
        }
    }

void UpdateTimerDisplay()
{
    int minutes = Mathf.FloorToInt(timeRemaining / 60);
    int seconds = Mathf.FloorToInt(timeRemaining % 60);

    if (timeRemaining <= 10)
        timerText.color = Color.red;
    else
        timerText.color = Color.white;

    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
}

    void GameOver()
    {
        Debug.Log("¡Tiempo agotado!");
        // Aquí puedes desactivar las cartas o mostrar un panel de fin de juego
    }

    public void CardRevealed(Card card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null && card != firstCard)
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        IsBusy = true;
        yield return new WaitForSeconds(0.5f);

        if (firstCard.cardID == secondCard.cardID)
        {
            firstCard.MatchFound();
            secondCard.MatchFound();
        }
        else
        {
            firstCard.HideCard();
            secondCard.HideCard();
        }

        firstCard = null;
        secondCard = null;
        IsBusy = false;
    }
}
