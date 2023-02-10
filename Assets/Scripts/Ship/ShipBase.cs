using System.Collections.Generic;
using UnityEngine;

public class ShipBase : MonoBehaviour
{
    // attach one to each ship
    [SerializeField] string shipName;
    [TextArea(3, 20)]
    [SerializeField] string description;
    [SerializeField] Sprite bodySprite;

    public List<Vector3> gunPositions;

    // ship stats
    [SerializeField] float shipSpeed;
    [SerializeField] int shipHealth;
    [SerializeField] int shipSmashDamage;
    [SerializeField] float knockbackForce;

    public int ShipHealth { get { return shipHealth; } }
    public Sprite BodySprite { get { return bodySprite; } }
    public int ShipSmashDamage { get { return shipSmashDamage; } }
    public float ShipSpeed { get { return shipSpeed; } }
    public float KnockbackForce { get { return knockbackForce; } }
}