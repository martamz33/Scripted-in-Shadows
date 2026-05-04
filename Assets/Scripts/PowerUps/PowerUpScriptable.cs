using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpScriptable", menuName = "Roguelike/PowerUp")]
public abstract class PowerUpScriptable : ScriptableObject {
    public FantasyPowerUp powerUpName;
    [TextArea] public string description;
    public Sprite icon;
    public int damage;
    public float duration;

    public abstract void ApplyEffect(GameObject player);
}

