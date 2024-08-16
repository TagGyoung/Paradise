using UnityEngine;
using UnityEngine.UI;

public class SafeUI : MonoBehaviour
{
    [SerializeField] bool success = false;

    [SerializeField] Image[] redLights;
    [SerializeField] Image[] greenLights;

    [SerializeField] Text[] safeNumbers;

    char[] uiNumbers = { '1', '0', '0', '4'};

    private void Update()
    {
        if (success == false) Success();
        else Fail();
    }

    private void Success()
    {
        for (int i = 0; i < uiNumbers.Length; i++)
        {
            if (uiNumbers[i] != safeNumbers[i].text[0]) return;
        }

        for (int i = 0; i < redLights.Length; i++)
        {
            redLights[i].color = Color.black;
            greenLights[i].color = Color.green;
        }

        success = true;
    }

    private void Fail()
    {
        for (int i = 0; i < uiNumbers.Length; i++)
        {
            if (uiNumbers[i] != safeNumbers[i].text[0]) success = false;
        }

        if (success == true) return;

        for (int i = 0; i < redLights.Length; i++)
        {
            redLights[i].color = Color.red;
            greenLights[i].color = Color.black;
        }
    }

    public void ExitButton()
    {
        if (success == false) gameObject.SetActive(false);
        else Destroy(gameObject);

        CursorManager.ActiveMouse(false, CursorLockMode.Locked);

        CursorManager.interactable = true;
    }
}