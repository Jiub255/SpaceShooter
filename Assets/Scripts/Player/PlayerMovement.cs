using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // movement
    [SerializeField] Rigidbody2D rb;
    float moveSpeed;
    [SerializeField] float autoMoveSpeed = 1f;
    Vector2 movementVector;

    // player movement bounds
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Vector3 playerSize;
    [SerializeField] Camera mainCamera;

    // player ship scriptable object
    [SerializeField] PlayerShipSO playerShipSO;

    public float AutoMoveSpeed { get { return autoMoveSpeed; } }

    private void OnEnable()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        SetupShip();
    }

    private void Update()
    {
        SetMovementVector();
    }

    private void FixedUpdate()
    {
        SetBounds();

        // does automatic forward movement together with player input movement
        rb.MovePosition(rb.position + (movementVector * moveSpeed + Vector2.up * autoMoveSpeed) * Time.fixedDeltaTime);
    }

    void SetMovementVector()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
        movementVector.Normalize();
    }

    void SetBounds()
    {
        // get x and y mins and maxes, translated to worldspace coordinates
        xMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        // TODO using child index seems easily broken. what's a better way?
        GameObject colliderChild = FindChildWithTag(gameObject, "Player");

        // measure wingtip to wingtip distance
        playerSize.x = colliderChild.GetComponent<Collider2D>().bounds.max.x -
            colliderChild.GetComponent<Collider2D>().bounds.min.x;

        // measure nose to tail distance
            // want to find max of all max y's from the guns eventually, using the ship for now
        playerSize.y = colliderChild.GetComponent<Collider2D>().bounds.max.y -
            colliderChild.GetComponent<Collider2D>().bounds.min.y;

        // magic numbers here, not perfect but works for now
        rb.position = new Vector2(
        Mathf.Clamp(rb.position.x, xMin + playerSize.x / 2, xMax - playerSize.x / 2),
        Mathf.Clamp(rb.position.y, yMin + playerSize.y / 2, yMax - playerSize.y));
    }

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }


    public void SetupShip()
    {
        // get reference to gameobject stored on playership scriptable object
        GameObject playerShip = playerShipSO.PlayerShip; // seems like im doing unnecessary stuff

        // instantiate the ship
        GameObject ship = Instantiate(playerShip, transform.position, Quaternion.identity, transform);
        
        // set ship speed
        moveSpeed = playerShipSO.PlayerShip.GetComponent<ShipBase>().ShipSpeed;
    }

    public void ChangeShip()
    {
        // destroy old ship and guns
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            Debug.Log("destroyed child: " + child.name);
        }

        SetupShip();
    }
}