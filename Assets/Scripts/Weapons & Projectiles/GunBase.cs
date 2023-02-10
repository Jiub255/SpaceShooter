using UnityEngine;

public class GunBase : MonoBehaviour
{
    [SerializeField] string gunName;
    [TextArea(3, 20)]
    [SerializeField] string description;

    [SerializeField] int gunDamage;
    [SerializeField] float shotsPerSecond;

    [SerializeField] string projectileTag; // for object pool

    public int GunDamage { get { return gunDamage; } }
    public float ShotsPerSecond { get { return shotsPerSecond; } }
    public string ProjectileTag { get { return projectileTag; } }
}