using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float sineWaveAmplitudeMultiplier = 1f; // sideways speed, 0f for a straight line
    [SerializeField] float sinCycleLengthMultiplier = 1f;
    [SerializeField] float spinsPerSecond;
    float startTime;

    // Knockback
    Rigidbody2D rb;
    [SerializeField] float knockbackTime = 1f;
    bool knockbackActive;

    private void OnEnable()
    {
        startTime = Time.time;

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!knockbackActive)
        {
            // Move in a downwards sin wave
            rb.MovePosition(transform.position + Vector3.down * moveSpeed * Time.fixedDeltaTime 
                + Vector3.right * Mathf.Sin((Time.time - startTime + Mathf.PI / 4) * sinCycleLengthMultiplier)
                * sineWaveAmplitudeMultiplier * Time.fixedDeltaTime);

        }
        // Spin
        transform.rotation *= Quaternion.Euler(0, 0, spinsPerSecond * Time.fixedDeltaTime * 360);
    }

    public void StartKnockbackCoroutine(Vector3 knockbackDirection, float knockback)
    {
        StartCoroutine(KnockbackCoroutine(knockbackDirection, knockback));
    }

    public IEnumerator KnockbackCoroutine(Vector3 knockbackDirection, float knockback)
    {
        knockbackActive = true;
        rb.AddForce(knockbackDirection * knockback, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockbackTime);
        knockbackActive = false;
    }
}