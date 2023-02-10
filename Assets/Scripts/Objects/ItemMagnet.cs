using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    // On each magnet-able item, on the parent

    bool isWithinMagnetRadius;
    Transform player;
    Rigidbody2D rb;
    [SerializeField] float speedBase = 2f;
    [SerializeField] float startMagnetDistance = 2f;
    [SerializeField] float stopMagnetDistance = 4f;

    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = (player.position - transform.position).magnitude;

        if (distanceToPlayer < startMagnetDistance)
            if (!isWithinMagnetRadius)
                isWithinMagnetRadius = true;
        else if (distanceToPlayer > stopMagnetDistance)
            if (isWithinMagnetRadius)
                isWithinMagnetRadius = false;

        if (isWithinMagnetRadius)
        {
            // Chase player, increasing speed the closer you get. magnet-style
            Vector3 differenceVector = player.position - transform.position;
            float distance = differenceVector.magnitude;

            // Trying inverse square law here
            rb.MovePosition(transform.position +
                (differenceVector.normalized * speedBase * Time.fixedDeltaTime) / (distance * distance));
        }
    }
}