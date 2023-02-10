using System.Collections;
using UnityEngine;

public class RandomStarBackground : MonoBehaviour
{
    // Puts stars randomly over a black background

    [SerializeField] private int minNumberOfStars = 50;
    [SerializeField] private int maxNumberOfStars = 200;

    [SerializeField] CameraController cameraController; // for border dimensions
    Vector3[] starPositions;
    private float backgroundHeight;
    private float timer;// timer made to last long enough to cross a full background

    private float yMax;
    private float yMin;

    private int howManyTimesStarsMade = 0;

    private void Start()
    {
        StartCoroutine(InitializeStars());
        SetupCamera();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            MakeStars();
            SetupCamera();
        }
    }

    void SetupCamera()
    {
        yMax = cameraController.YMax;
        yMin = cameraController.YMin;
        backgroundHeight = yMax - yMin;

        // camera goes up MoveSpeed units per second, goes backgroundHeight units.
        // Needs backgroundHeight/MoveSpeed seconds to move over entire visible background
        timer = backgroundHeight / cameraController.MoveSpeed;
    }

    private IEnumerator InitializeStars()
    {
        MakeStars();

        yield return new WaitForSeconds(0.1f);

        MakeStars();
    }

    private void MakeStars()
    {
        int numberOfStars = HowManyStars();
        starPositions = new Vector3[numberOfStars];
        SetStarPositions(numberOfStars);
        ActivateStars(numberOfStars);
        howManyTimesStarsMade++;
    }

    // pick random number of stars
    private int HowManyStars()
    {
        int numberOfStars = Random.Range(minNumberOfStars, maxNumberOfStars);
    //    Debug.Log("Number of stars: " + numberOfStars);
        return numberOfStars;
    }

    // randomly choose the stars' positions
    private void SetStarPositions(int numberOfStars)
    {
        for (int i = 0; i < numberOfStars; i++)
        {
            SetStarPosition(i);
        }
    }
    private void SetStarPosition(int starIndex)
    {
        // sets the star one backgroundHeight above currently visible screen
        starPositions[starIndex] = new Vector3(
    Random.Range(cameraController.XMin, cameraController.XMax),
    Random.Range(cameraController.YMin, cameraController.YMax) + (backgroundHeight * howManyTimesStarsMade), 
    0f);
    }

    // activate stars from object pool
    private void ActivateStars(int numberOfStars)
    {
        for (int i = 0; i < numberOfStars; i++)
        {
            ActivateStar(starPositions[i]);
        }
    }
    private void ActivateStar(Vector3 starPosition)
    {
        GameObject star = ObjectPool.SharedInstance.GetPooledObject("Star");
        if (star != null)
        {
            star.transform.position = starPosition;

            // randomize alpha
            Color temp = star.GetComponent<SpriteRenderer>().color;
            temp.a = Random.value;
            star.GetComponent<SpriteRenderer>().color = temp;

            star.SetActive(true);
        }
    }
}