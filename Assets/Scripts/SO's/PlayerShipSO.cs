using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerShipSO : ScriptableObject
{
    // make a scriptable object for each player ship. to store guns, upgrades, etc.,
    // and one to store the actual player ship
    [SerializeField] string shipName;
    [SerializeField] GameObject playerShip;
    [SerializeField] List<GameObject> guns = new List<GameObject>();
    [SerializeField] bool shipOwned;
    [SerializeField] int currentShipIndex;
    [SerializeField] int shipCurrentHealth;
    [SerializeField] int shipMaxHealth;

    public string ShipName
    {
        get { return shipName; }
        set { shipName = value; }
    }

    public GameObject PlayerShip 
    { 
        get { return playerShip; }
        set { playerShip = value; }
    }

    public List<GameObject> Guns
    {
        get { return guns; }
        set { guns = value; }
    }

    public bool ShipOwned
    {
        get { return shipOwned; }
        set { shipOwned = value; }
    }

    public int CurrentShipIndex
    {
        get { return currentShipIndex; }
        set { currentShipIndex = value; }
    }

    public int ShipCurrentHealth
    {
        get { return shipCurrentHealth; }
        set { shipCurrentHealth = value; }
    }

    public int ShipMaxHealth
    {
        get { return shipMaxHealth; }
        set { shipMaxHealth = value; }
    }
}