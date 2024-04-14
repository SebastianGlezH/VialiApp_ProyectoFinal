using UnityEngine;
using UnityEngine.UI;

public class Us_SessionChecker : MonoBehaviour
{
    public Button yourButton;

    public void Start()
    {
        yourButton.onClick.AddListener(SessionManager.Instance.CheckLoginStatus);
    }
}
