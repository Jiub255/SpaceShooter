using UnityEngine;

[System.Serializable]
public class Loot
{
    public GameObject thisLoot;
    public float lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public GameObject LootPowerup()
    {
        float cumulativeProbability = 0f;
        float currentProbability = Random.Range(0, 100);
        for (int i = 0; i < loots.Length; i++)
        {
            cumulativeProbability += loots[i].lootChance;
            if (currentProbability <= cumulativeProbability)
            {
                return loots[i].thisLoot;
            }
        }

        return null;
    }
}