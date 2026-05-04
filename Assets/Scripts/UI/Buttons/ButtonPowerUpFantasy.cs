using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonPowerUpFantasy : MonoBehaviour
{
    [Header("Settings Canvas")]
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI description;

    private PowerUpScriptable currentPowerUp;
    private UIManager manager;

    public void Setup(PowerUpScriptable power, UIManager ui)
    {
        currentPowerUp = power;
        manager = ui;

        iconImage.sprite = power.icon;
        nameText.text = power.powerUpName.ToString();
        description.text = power.description;
    }

    public void OnClick()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        currentPowerUp.ApplyEffect(player);

        string IDInk = ObteinIDForInk(currentPowerUp.powerUpName.ToString());
        DialogueManager.instance.ContinueAfterPowerUpChoice(IDInk);

        manager.closeMenu();
    }

    private string ObteinIDForInk(string powerUpName)
    {
        if(powerUpName.Contains("Vengance"))  return "venganza";
        if(powerUpName.Contains("Battle"))    return "batalla";
        if(powerUpName.Contains("Salvation")) return "salvacion";
        return "";
    }
}
