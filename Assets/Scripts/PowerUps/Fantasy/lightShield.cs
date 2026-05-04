using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightShield : MonoBehaviour
{
    private LightOfSalvation data;
    
    private float timer = 0f;
    private float currentRandomTimeLapse = 0f;
    private float spawnTime = 0.5f;
    private bool isShieldActive = false;

    private GhostHealth ghostHealth;

    public void Setup(LightOfSalvation config)
    {
        data = config;

        ghostHealth = GetComponent<GhostHealth>();

        SetRandomTime();
    }

    void Update()
    {
        if(isShieldActive) return;

        float healthPercentage = ghostHealth.actualHealth / ghostHealth.totalHealth;

        if(healthPercentage <= 0.15f)
        {
            timer += Time.deltaTime;

            if(timer >= currentRandomTimeLapse)
            {
                StartCoroutine(ActiveShieldRoutine());
            }
        }
        else
        {
            timer = 0f;
        }
    }

    private void SetRandomTime()
    {
        currentRandomTimeLapse = Random.Range(data.minCooldown, data.maxCooldown);
        timer = 0f;
    }

    private IEnumerator ActiveShieldRoutine()
    {
        isShieldActive = true;

        GameObject shield = Instantiate(data.prefabShield, transform.position, transform.rotation);

        shield.transform.localScale = Vector3.zero;

        ghostHealth.isInvulnerable = true;
        
        float t = 0;
        Vector3 finalState = Vector3.one;

        while(t < spawnTime)
        {
            t+= Time.deltaTime;

            float normalizedTime = t/spawnTime;

            shield.transform.localScale = Vector3.Lerp(Vector3.zero, finalState, normalizedTime);

            yield return null;
        }

        shield.transform.localScale = finalState;

        yield return new WaitForSeconds(data.shieldDuration - spawnTime);

        if(shield != null) Destroy(shield);

        ghostHealth.isInvulnerable = false;

        SetRandomTime();
        isShieldActive = false;
    }
}
