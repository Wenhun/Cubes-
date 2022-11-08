using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AddNewCubes : MonoBehaviour
{
    GameObject[] cubesAdd;

    public GameObject[,] cubes; // array for tracking cubes, removing and adding cubes

    CubesRandomaizer cubesRandomaizer;
    SwapCubes swapCubes;
    SwapCount swapCount;
    ColorFinder colorFinder;
    Score score;
    MagnetShake magnetShake;

    AudioSource audioSource;
    [SerializeField] AudioClip addCube;
    [SerializeField] AudioClip rowDown;
    float speed = 30;

    int cubeCount; //number available cubes
    [SerializeField] int countSwap = 5;
    int points = 0;
    [SerializeField] int pointsAddToScore = 10;

    private void Start()
    {
        cubesRandomaizer = GetComponent<CubesRandomaizer>();
        swapCubes = GetComponent<SwapCubes>();
        swapCount = FindObjectOfType<SwapCount>();
        score = FindObjectOfType<Score>();
        colorFinder = GetComponent<ColorFinder>();
        audioSource = GetComponent<AudioSource>();

        cubeCount = cubesRandomaizer.areaSize;
        swapCount.ChangeCountSwap(countSwap);

        cubesAdd = new GameObject[cubeCount * cubeCount];
        cubes = new GameObject[cubeCount, cubeCount];

        //create array
        AddToArray("Red");
        AddToArray("Blue");
        AddToArray("Green");
        AddToArray("Yellow");
    }

    //Cubes down
    IEnumerator DownRow()
    {
        //shake magnet
        float tempAngle, tempTime;
        tempAngle = GameObject.Find("Horizontal Magnet").GetComponent<MagnetShake>().maxAngle;
        tempTime = GameObject.Find("Horizontal Magnet").GetComponent<MagnetShake>().toggleTime;
        GameObject.Find("Horizontal Magnet").GetComponent<MagnetShake>().maxAngle = 10;
        GameObject.Find("Horizontal Magnet").GetComponent<MagnetShake>().toggleTime = 0.2f;

         for (int j = 0; j < cubeCount - 1; j++)
         {
             for (int i = 0; i < cubeCount; i++)
             {
                 if (cubes[i, j] == null && cubes[i, j + 1] != null)
                 {
                     Vector3 temp = new Vector3(i, j, 0);

                     while(cubes[i, j + 1].transform.position != temp)
                     {
                        cubes[i, j + 1].transform.position = Vector3.MoveTowards(cubes[i, j + 1].transform.position, temp, Time.deltaTime*speed);
                        yield return null;
                     }
                     cubes[i, j] = cubes[i, j + 1];
                     cubes[i, j + 1] = null;

                    audioSource.Stop();
                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(rowDown);
                    }
                }
             }
             yield return null;
         }
         //return magnet to start position 
         GameObject.Find("Horizontal Magnet").GetComponent<MagnetShake>().maxAngle = tempAngle;
         GameObject.Find("Horizontal Magnet").GetComponent<MagnetShake>().toggleTime = tempTime;
        yield break;
    }

    //Cubes left
    IEnumerator LeftRow()
    {
        //shake magnet
        float tempAngle, tempTime;
        tempAngle = GameObject.Find("Vertical Magnet").GetComponent<MagnetShake>().maxAngle;
        tempTime = GameObject.Find("Vertical Magnet").GetComponent<MagnetShake>().toggleTime;
        GameObject.Find("Vertical Magnet").GetComponent<MagnetShake>().maxAngle = 10;
        GameObject.Find("Vertical Magnet").GetComponent<MagnetShake>().toggleTime = 0.2f;

         for (int i = 0; i < cubeCount - 1; i++)
         {
             for (int j = 0; j < cubeCount; j++)
             {
                 if (cubes[i, j] == null && cubes[i + 1, j] != null)
                 {
                     Vector3 temp = new Vector3(i, j, 0);

                    while(cubes[i + 1, j].transform.position != temp)
                    {
                        cubes[i + 1, j].transform.position = Vector3.MoveTowards(cubes[i + 1, j].transform.position, temp, Time.deltaTime*speed);
                        yield return null;
                    }
                     
                     cubes[i, j] = cubes[i + 1, j];
                     cubes[i + 1, j] = null;

                    audioSource.Stop();
                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(rowDown);
                    }
                }
             }
             yield return null;
         }
        //return magnet to start position 
        GameObject.Find("Vertical Magnet").GetComponent<MagnetShake>().maxAngle = tempAngle;
        GameObject.Find("Vertical Magnet").GetComponent<MagnetShake>().toggleTime = tempTime;
        yield break;
    }

    //adding new cubes
    public IEnumerator AddCubes()
    {
        yield return StartCoroutine(DownRow());
        yield return StartCoroutine(LeftRow());

        //change available swap
        countSwap++;
        countSwap++;
        swapCount.ChangeCountSwap(countSwap);

        points += pointsAddToScore;
        score.IncreaseScore(points);

        for (int i = 0; i < cubeCount; i++)
        {
            for (int j = 0; j < cubeCount; j++)
            {
                yield return null;
                if (cubes[i, j] == null)
                {
                    cubesRandomaizer.CreateCubes(i, j);
                    audioSource.Stop();
                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(addCube);
                    }
                    AddToArray("Red");
                    AddToArray("Blue");
                    AddToArray("Green");
                    AddToArray("Yellow");

                    colorFinder.FindColor("Red");
                    colorFinder.FindColor("Blue");
                    colorFinder.FindColor("Green");
                    colorFinder.FindColor("Yellow");

                    yield return new WaitForSeconds(0.05f);;
                }
            }
        }
        swapCubes.positionOn = true;
        yield break;
    }

    //adding cubes in array for tracking
    void AddToArray(string color)
    {
        cubesAdd = GameObject.FindGameObjectsWithTag(color);

        foreach (GameObject cube in cubesAdd)
        {
            for (int i = 0; i < cubeCount; i++)
                for (int j = 0; j < cubeCount; j++)
                {
                    if (Mathf.RoundToInt(cube.transform.position.x) == i && Mathf.RoundToInt(cube.transform.position.y) == j)
                    {
                        cubes[i, j] = cube;
                    }
                }
        }
    }

    //clear note about cubes that have destroy into array
    public void ClearRow(GameObject kill)
    {
        for (int i = 0; i < cubeCount; i++)
            for (int j = 0; j < cubeCount; j++)
            {
                if (kill == cubes[i, j])
                {
                    cubes[i, j] = null;
                }
            }
    }

    //change array of cubes that have swap
    public void SwapIntoArray(GameObject cube1, GameObject cube2)
    {
         GameObject temp = null;

         int i1 = 0, j1 = 0, i2 = 0, j2 = 0; //indexes of cubes that have swapped

         for (int i = 0; i < cubeCount; i++)
             for (int j = 0; j < cubeCount; j++)
             {
                 if (cube1 == cubes[i, j])
                 {
                     i1 = i;
                     j1 = j;
                 }
                 if (cube2 == cubes[i, j])
                 {
                     i2 = i;
                     j2 = j;
                 }
             }
         temp = cubes[i1, j1];
         cubes[i1, j1] = cubes[i2, j2];
         cubes[i2, j2] = temp;

        //change available swap
        countSwap--;
        if (countSwap < 0)
            ReloadLevel();

        swapCount.ChangeCountSwap(countSwap);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
