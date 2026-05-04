using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlast : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 8f;
    private float lifeTime;

    [Header("Damage")]
    public float damagePerSeconds = 3f;
    private int damage;

    public void Setup(SpiritOfBattle data)
    {
        lifeTime = data.duration;
        damage = data.damage;

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();

            if(enemy != null)
            {
                enemy.StartCoroutine(DamageOverTime(enemy));
            }       
        }
    }

    private IEnumerator DamageOverTime(EnemyBase enemy)
    {
        for(int i = 0; i < damagePerSeconds; i++)
        {
            if(enemy == null) yield break;

            enemy.TakeDamage(damage);

            yield return new WaitForSeconds(1f);
        }
    }
}
