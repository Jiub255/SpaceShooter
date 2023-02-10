using UnityEngine;

[CreateAssetMenu]
public class StatsSO : ScriptableObject
{
    // basic stats
    public int currentHealth;
    public int maxHealth;
    public int money;

    // "pilot bonuses"
    public float speedBonus;

    // do i want these?
    public int bulletDamageBonus; // add this amount to projectile damage
    public float shotsPerSecondBonus; // add to shotspersecond
}