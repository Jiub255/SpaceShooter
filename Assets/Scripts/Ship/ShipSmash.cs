using UnityEngine;
using UnityEngine.Events;

public class ShipSmash : MonoBehaviour
{
    // player ship scriptable object
    [SerializeField] PlayerShipSO playerShipSO;

    // ship smash damage
    int smashDamage;
    public static event UnityAction<int, int, float, Vector3> HitEnemy;
    float knockbackForce;

    private void OnEnable()
    {
        SetupSmashStats();
    }

    void SetupSmashStats()
    {
        smashDamage = playerShipSO.PlayerShip.GetComponent<ShipBase>().ShipSmashDamage;
        knockbackForce = playerShipSO.PlayerShip.GetComponent<ShipBase>().KnockbackForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            int enemyID = collision.collider.gameObject.transform.parent.GetInstanceID();
            Vector3 knockbackDirection = collision.transform.position - transform.position;
            HitEnemy?.Invoke(enemyID, smashDamage, knockbackForce, knockbackDirection);
            Debug.Log("ship smash enemy");
        }        
    }
}