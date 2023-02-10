using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    List<float> shootTimerLength = new List<float>();
    List<float> shootTimer = new List<float>();
    List<bool> canShoot = new List<bool>();

    [SerializeField] List<Vector3> projectileSpawnOffsets;

    [SerializeField] StatsSO playerStats; // for pilot bonuses

    [SerializeField] PlayerShipSO playerShipSO; // for gun(s) information: positions, damages, shots per second, etc.

    private void OnEnable()
    {
        InitializeGuns();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)) // GetKey so you can hold and repeatedly shoot
        {
            for (int i = 0; i < playerShipSO.Guns.Count; i++)
            {
                if (canShoot[i] == false)
                {
                    CountDownShootTimer(i);
                }
                else
                {    
                    ResetShootTimer(i);
                    FireGun(i);
                }
            }
        }
    }

    void CountDownShootTimer(int i)
    {
        shootTimer[i] -= Time.deltaTime;
        if (shootTimer[i] <= 0)
        {
            canShoot[i] = true;
        }
    }

    void ResetShootTimer(int i)
    {
        canShoot[i] = false;
        shootTimer[i] = shootTimerLength[i];
    }

    void FireGun(int i)
    {
        GameObject currentProjectile = ActivateProjectile(projectileSpawnOffsets[i], i);
        currentProjectile.GetComponent<Projectile>().LaunchAndSetDamage(Vector2.up, 
            playerShipSO.Guns[i].GetComponent<GunBase>().GunDamage);
    }

    GameObject ActivateProjectile(Vector3 spawnOffset, int index)
    {
        string projectileTag = playerShipSO.Guns[index].GetComponent<GunBase>().ProjectileTag;
        GameObject currentProjectile = ObjectPool.SharedInstance.GetPooledObject(projectileTag);
        if (currentProjectile != null)
        {
            currentProjectile.transform.position = transform.position + spawnOffset;
            currentProjectile.transform.rotation = transform.rotation;
            currentProjectile.SetActive(true);
        }

        return currentProjectile;
    }

    public void InitializeGuns()
    {
        ShipBase shipBase = playerShipSO.PlayerShip.GetComponent<ShipBase>();
        int numberOfGuns = playerShipSO.Guns.Count;
        for (int i = 0; i < numberOfGuns; i++)
        {
            // initialize timers/bools
            shootTimerLength.Add(1 / playerShipSO.Guns[i].GetComponent<GunBase>().ShotsPerSecond);
            shootTimer.Add(0f);
            canShoot.Add(true);
            // instantiate gun
            GameObject gun = Instantiate(playerShipSO.Guns[i], transform.position + shipBase.gunPositions[i], 
                Quaternion.identity, transform);
            // set player as parent, keep world position
            //gun.transform.SetParent(transform,true);
            // set projectile spawn offsets
            projectileSpawnOffsets.Add(gun.transform.localPosition + new Vector3(0f,
                gun.GetComponent<Collider2D>().bounds.max.y - gun.GetComponent<Collider2D>().bounds.min.y,
                0f));
        }
    }

    public void ResetGuns()
    {
        shootTimerLength.Clear();
        shootTimer.Clear();
        canShoot.Clear();
        projectileSpawnOffsets.Clear();
        InitializeGuns();
    }
}