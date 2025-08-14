using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EpModule : MonoBehaviour
{
    private CardGroup cardGroup;
    [SerializeField] private Button cardBackButton;

    [SerializeField] private Image cardBackBackground;
    [SerializeField] private Image cardFrontBackground;
    [SerializeField] private Image cardFrontImage;

    [SerializeField] private GameObject cardBack;
    [SerializeField] private GameObject cardFront;

    private bool objectMatch;

    [Header("DoTween Animation")]
    [SerializeField] private Vector3 selectRotation = new Vector3();
    [SerializeField] private Vector3 deselectRotation = new Vector3();
    [SerializeField] private float duration = 0.26f;
    private Tweener[] tweener = new Tweener[3];

    private void Awake()
    {
        if (cardGroup == null)
        {
            cardGroup = transform.parent.GetComponent<CardGroup>();
        }
        if (cardGroup != null)
        {
            cardGroup.Subscribe(this);
        }
    }
    private void Start()
    {
        cardBackButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // Implementation not shown in the image
    }
    private void Select()
    {
        tweener[0] = transform.DORotate(selectRotation, duration).SetEase(Ease.InOutElastic).OnUpdate(CheckSelectHalfDuration);
    }
    private void Deselect()
    {
        tweener[1] = transform.DORotate(selectRotation, duration).SetEase(Ease.InOutElastic).OnUpdate(CheckSelectHalfDuration);
    }
    private void CheckSelectHalfDuration()
    {
        float elapsed = tweener[0].Elapsed();
        float halfDuration = tweener[0].Duration() / 2f;
        if (elapsed >= halfDuration)
        {
            cardBack.SetActive(false);
            cardFront.SetActive(true);
        }
    }
    public Image GetCardFrontBackground() => cardFrontBackground;
    public Image GetCardBackBackground() => cardBackBackground;

}