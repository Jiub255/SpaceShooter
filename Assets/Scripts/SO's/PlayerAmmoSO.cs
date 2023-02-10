using UnityEngine;

[CreateAssetMenu]
public class PlayerAmmoSO : ScriptableObject
{
    [SerializeField] int bombs;
    [SerializeField] int maxBombs;

    [SerializeField] int killalls;
    [SerializeField] int maxKillalls;

    public int Bombs
    {
        get { return bombs; }
        set { bombs = value; }
    }

    public int MaxBombs
    {
        get { return maxBombs; }
        set { maxBombs = value; }
    }
}