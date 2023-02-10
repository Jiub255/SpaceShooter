using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public static event UnityAction<int, int, float, Vector3> HitEnemy;

    [SerializeField] int damage = 1;
    [SerializeField] float knockback;
    Vector3 knockbackDirection;
    [SerializeField] float speed = 10f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] StatsSO playerStats; // for bulletDamageBonus

    public void LaunchAndSetDamage(Vector2 target, int projectileDamage)
    {
        rb.velocity = target * speed;
        knockbackDirection = target;
        damage = projectileDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            int enemyID = collision.gameObject.transform.parent.GetInstanceID();
            HitEnemy?.Invoke(enemyID, damage + playerStats.bulletDamageBonus, knockback, knockbackDirection);
        }
     //   Debug.Log("projectile hit " + collision.name);
        gameObject.SetActive(false);
    }
}