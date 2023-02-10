using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Spawns random enemies at random places

    [SerializeField] private int minNumberOfEnemies = 0;
    [SerializeField] private int maxNumberOfEnemies = 2;

    [SerializeField] CameraController cameraController; // for border dimensions
    private float timer;// timer made to last long enough to cross a full background
    private int howManyTimesSpawnedEnemies = 0;

    [SerializeField] List<EnemyType> enemyTypes = new List<EnemyType>();

    List<Vector3> potentialEnemyPositions = new List<Vector3>();
    List<Vector3> enemyPositions = new List<Vector3>();

    private void Start()
    {
        CalculateSpawnOdds();
        SetPotentialPositions();
        MakeEnemies();
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
            SetPotentialPositions();
            MakeEnemies();
            SetupCamera();
        }
    }

    void SetupCamera()
    {
        float backgroundHeight = cameraController.YMax - cameraController.YMin;

        // camera goes up MoveSpeed units per second, background is backgroundHeight units tall.
        // Needs backgroundHeight/MoveSpeed seconds to move over entire visible background
        timer = backgroundHeight / (8 * cameraController.MoveSpeed);
    }

    void SetPotentialPositions()
    {
        potentialEnemyPositions.Clear();

        int width = Mathf.FloorToInt(cameraController.XMax - cameraController.XMin);
        int height = Mathf.FloorToInt(cameraController.YMax - cameraController.YMin);

        for (int i = 0; i < width; i++)
        {
            potentialEnemyPositions.Add(new Vector3(i - width / 2,
                (height / 8) * (howManyTimesSpawnedEnemies + 1 + height), 0f));
        }
    }

    private void MakeEnemies()
    {
        int numberOfEnemies = HowManyEnemies();
        enemyPositions.Clear();
        SetEnemyPositions(numberOfEnemies);
        ActivateEnemies(numberOfEnemies);
        howManyTimesSpawnedEnemies++;
    }

    private int HowManyEnemies()
    {
        int numberOfEnemies = Random.Range(minNumberOfEnemies, maxNumberOfEnemies);
      //  Debug.Log("Number of enemies: " + numberOfEnemies);
        return numberOfEnemies;
    }

    private void SetEnemyPositions(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SetEnemyPosition();
        }
    }
    private void SetEnemyPosition()
    {
        int rand = Random.Range(0, potentialEnemyPositions.Count + 1);
        Debug.Log("Potential enemy positions: " + potentialEnemyPositions.Count + 
            ", random number: " + rand);
        Vector3 randomPotentialPosition = potentialEnemyPositions[rand];
        enemyPositions.Add(randomPotentialPosition);
    }

    private void ActivateEnemies(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            ActivateEnemy(enemyPositions[i]);
        }
    }
    private void ActivateEnemy(Vector3 enemyPosition)
    {
        GameObject enemy = ObjectPool.SharedInstance.GetPooledObject(ChooseRandomEnemyTag());
        if (enemy != null)
        {
            enemy.transform.position = enemyPosition;
            enemy.SetActive(true);
/*            EnemyShoot enemyShoot = enemy.GetComponent<EnemyShoot>();
            enemyShoot.IsShooting = false;
            enemyShoot.CanShoot = true;*/
        }
    }

    string ChooseRandomEnemyTag()
    {
        float totalOdds = CalculateSpawnOdds();
        float rand = Random.Range(0, totalOdds);
      //  Debug.Log("rand = " + rand + ", total = " + totalOdds);
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            if (enemyTypes[i].MinOdds <= rand && rand <= enemyTypes[i].MaxOdds)
            {
              //  Debug.Log("returned " + enemyTypes[i].tag);
                return enemyTypes[i].tag;
            }
        }
      //  Debug.Log("returned null");
        return null;
    }

    float CalculateSpawnOdds()
    {
        // feels overcomplicated

        /*        totalOdds = enemyTypes[0].relativeOddsToSpawn;
                enemyTypes[0].MinOdds = 0;
                enemyTypes[0].MaxOdds = enemyTypes[0].relativeOddsToSpawn;*/
        float totalOdds = 0f;
        for (int i = 0; i < enemyTypes.Count; i++)
        {
          //  Debug.Log("i = " + i);
            totalOdds += enemyTypes[i].relativeOddsToSpawn;
            enemyTypes[i].MinOdds = 0;
            enemyTypes[i].MaxOdds = 0;
            for (int j = 0; j < i; j++)
            {
                enemyTypes[i].MinOdds += enemyTypes[j].relativeOddsToSpawn;
            }
            enemyTypes[i].MaxOdds = enemyTypes[i].MinOdds + enemyTypes[i].relativeOddsToSpawn;
/*            enemyTypes[i].MinOdds = enemyTypes[i - 1].relativeOddsToSpawn;
            enemyTypes[i].MaxOdds = enemyTypes[i - 1].relativeOddsToSpawn + enemyTypes[i].relativeOddsToSpawn;*/
        }
        return totalOdds;
    }
}

[System.Serializable]
public class EnemyType 
{
    public string tag;
    public float relativeOddsToSpawn;
    float minOdds;
    float maxOdds;
    // 1 is "normal", 0.01 for really rare, 5 or whatever for more common
    // add all relative odds for total, then divide each relative odds by total for actual odds

    public float MinOdds 
    { 
        get { return minOdds; }
        set { minOdds = value; }
    }
    public float MaxOdds
    {
        get { return maxOdds; }
        set { maxOdds = value; }
    }
}