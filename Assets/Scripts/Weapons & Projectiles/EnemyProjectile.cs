using UnityEngine;
using UnityEngine.Events;

public class EnemyProjectile : MonoBehaviour
{
    public static event UnityAction<int> EnemyProjectileHitPlayer;

    public int damage = 1;
    public float speed = 10f;
    public Rigidbody2D rb;

    public void LaunchEnemyProjectile(Vector2 target)
    {
        rb.velocity = target * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemyProjectileHitPlayer?.Invoke(damage);
            //Debug.Log("projectile hit player for " + damage + " damage");
        }
        gameObject.SetActive(false);
    }
}