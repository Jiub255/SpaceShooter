using UnityEngine;

public class CameraController : MonoBehaviour
{
    float moveSpeed;
    PlayerMovement playerMovement;
    [SerializeField] Camera thisCamera;
    [SerializeField] float xMin;
    [SerializeField] float xMax;
    [SerializeField] float yMin;
    [SerializeField] float yMax;
    [SerializeField] float offset = 7f;

    public float XMin { get { return xMin; } }
    public float XMax { get { return xMax; } }
    public float YMin { get { return yMin; } }
    public float YMax { get { return yMax; } }
    public float MoveSpeed { get { return moveSpeed; } }

    private void Awake()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        MatchPlayersAutoMoveSpeed();
        GetBorderPositions();
        SetBorders();
    }

    private void FixedUpdate()
    {
        transform.position += moveSpeed * Vector3.up * Time.fixedDeltaTime;
    }

    void MatchPlayersAutoMoveSpeed()
    {
        moveSpeed = playerMovement.AutoMoveSpeed;
    }

    void GetBorderPositions()
    {
        xMin = thisCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = thisCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = thisCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = thisCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    void SetBorders()
    {
        // Top Border
        Vector3 temp = Vector3.zero;
        temp.y = yMax + offset;
        transform.GetChild(0).transform.position = temp;
        transform.GetChild(0).transform.localScale = new Vector3(((xMax + offset) * 2) + 1, 1f, 1f);

        // Bottom Border
        temp = Vector3.zero;
        temp.y = yMin - offset;
        transform.GetChild(2).transform.position = temp;
        transform.GetChild(2).transform.localScale = new Vector3(((xMax + offset) * 2) + 1, 1f, 1f);

        // Star Killer Border
        temp = Vector3.zero;
        temp.y = yMin - offset - 2;
        transform.GetChild(4).transform.position = temp;
        transform.GetChild(4).transform.localScale = new Vector3((xMax * 2) + 1, 1f, 1f);

        // Right Border
        temp = Vector3.zero;
        temp.x = xMax + offset;
        transform.GetChild(1).transform.position = temp;
        transform.GetChild(1).transform.localScale = new Vector3(1f, ((yMax + offset) * 2) - 1, 1f);

        // Left Border
        temp = Vector3.zero;
        temp.x = xMin - offset;
        transform.GetChild(3).transform.position = temp;
        transform.GetChild(3).transform.localScale = new Vector3(1f, ((yMax + offset) * 2) - 1, 1f);
    }
}