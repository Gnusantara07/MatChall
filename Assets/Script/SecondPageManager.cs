using UnityEngine;
using TMPro;

public class SecondPageManager : MonoBehaviour
{
    public TMP_Text displayNameText;

    void Start()
    {
        if (displayNameText != null)
        {
            displayNameText.text = "Welcome, " + InputController.userName;
        }
    }
}
