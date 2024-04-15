using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Añade este using para usar TextMeshProUGUI

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswer> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public TextMeshProUGUI QuestionTxt; // Cambia UnityEngine.UI.Text a TextMeshProUGUI

    private void Start()
    {
        generateQuestion();
    }

    public void correct()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answer[i]; // Cambia a TextMeshProUGUI

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0) // Verifica si la lista tiene al menos una pregunta
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswer();
        }
        else
        {
            Debug.LogWarning("No hay preguntas disponibles.");
            // Aquí puedes agregar un manejo adicional, como mostrar un mensaje de error o detener el juego.
        }
    }
}
