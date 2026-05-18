using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InkManager : MonoBehaviour
{
    public static InkManager instance;

    [Header("Canvas Settings")]
    public TextMeshProUGUI inkText;

    public int totalInk;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        if(inkText != null)
        {
            UpdateUI();
        }
    }

    public void AddInk(int amount)
    {
        float multiplier = GameManager.Instance.inkMultiplier;

        int finalAmount = Mathf.RoundToInt(amount * multiplier);
        totalInk += finalAmount;
        UpdateUI();
    }

    public bool SpendInk(int amount)
    {
        if(totalInk >= amount)
        {
            totalInk -=amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    public void ResetInk()
    {
        totalInk = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        inkText.text = totalInk.ToString();
    }
}
