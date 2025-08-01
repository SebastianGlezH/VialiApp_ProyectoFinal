using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageCarousel : MonoBehaviour
{
    public Image imageDisplay;            // Imagen principal
    public Sprite[] imageList;            // Lista de imágenes
    public float changeInterval = 3f;     // Tiempo entre cambios

    private int currentIndex = 0;
    private Coroutine autoSwitchCoroutine;

    void Start()
    {
        if (imageList.Length > 0)
        {
            imageDisplay.sprite = imageList[currentIndex];
            autoSwitchCoroutine = StartCoroutine(AutoSwitch());
        }
    }

    public void NextImage()
    {
        currentIndex = (currentIndex + 1) % imageList.Length;
        imageDisplay.sprite = imageList[currentIndex];
    }

    public void PrevImage()
    {
        currentIndex = (currentIndex - 1 + imageList.Length) % imageList.Length;
        imageDisplay.sprite = imageList[currentIndex];
    }

    private IEnumerator AutoSwitch()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeInterval);
            NextImage();
        }
    }
}
