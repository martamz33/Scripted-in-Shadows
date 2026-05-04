using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendettaLogic : MonoBehaviour
{
    private SpiritOfVendetta data;

    public void Setup(SpiritOfVendetta config)
    {
        data = config;
    }
    
    private void OnEnable() => EnemyBase.OnAnyEnemyDeath += Sprits;
    private void OnDisable() => EnemyBase.OnAnyEnemyDeath -= Sprits;

    private void Sprits(Vector3 deathPos)
    {
        for(int i = 0; i < 4; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 1.5f), 0);
            GameObject spiritObj = Instantiate(data.prefabSprit, deathPos + randomOffset, Quaternion.identity);
            
            SpiritAttack spiritScript = spiritObj.GetComponent<SpiritAttack>();
            if (spiritScript != null)
            {
                spiritScript.Setup(data.damage); // <-- Aquí le pasamos el daño (ej: 5)
            }
        }
    }
}
