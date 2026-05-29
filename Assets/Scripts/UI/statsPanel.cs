using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class statsPanel : MonoBehaviour
{
    [Header("TextMeshPro References")]
    public TextMeshProUGUI winsVsFallenHero;
    public TextMeshProUGUI losesVsFallenHero;
    public TextMeshProUGUI winsVsJudge;
    public TextMeshProUGUI losesVsJudge;
    public TextMeshProUGUI totalLoses;

    void Start()
    {
        if(GameManager.Instance != null)
        {
            winsVsFallenHero.text = GameManager.Instance.victoriesAgainstBossFantasy.ToString();
            losesVsFallenHero.text = GameManager.Instance.deathsAgainstBossFantasy.ToString();
            winsVsJudge.text = GameManager.Instance.victoriesAgainstBossFinal.ToString();
            losesVsJudge.text = GameManager.Instance.deathsAgainstBossFinal.ToString();
            totalLoses.text = GameManager.Instance.totalDeaths.ToString();
        }
        
    }
}
