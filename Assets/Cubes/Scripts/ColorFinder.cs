using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFinder : MonoBehaviour
{
    GameObject[] cubes;

    AddNewCubes addNewCubes;
    CubesRandomaizer CubesRandomaizer;
    SwapCubes swapCubes;

    AudioSource audioSource;
    [SerializeField] AudioClip clearRow;

    int countCubes;

    void Start()
    {
        CubesRandomaizer = GetComponent<CubesRandomaizer>();
        countCubes = CubesRandomaizer.areaSize;
        cubes = new GameObject[countCubes*countCubes];
        addNewCubes = GetComponent<AddNewCubes>();
        swapCubes = GetComponent<SwapCubes>();
        audioSource = GetComponent<AudioSource>();
    }

    public void FindColor(string color)
    {
        cubes = GameObject.FindGameObjectsWithTag(color);

        int count = 0; //element-by-order count

        //count X elements
        SortX(cubes);

        for (int i = 0; i < cubes.Length - 1; i++)
        {
            if (cubes[i].transform.position.x == cubes[i + 1].transform.position.x)
            {
                count++;
                if (count == countCubes - 1)
                {
                    count = 0;
                    swapCubes.positionOn = false;
                    ClearRowX(cubes, Mathf.RoundToInt(cubes[i].transform.position.x));
                }
                    
            }
            else
            {
                count = 0;
            }
        }

        //count Y elements
        SortY(cubes);

        count = 0;

        for (int i = 0; i < cubes.Length - 1; i++)
        {
            if (cubes[i].transform.position.y == cubes[i + 1].transform.position.y)
            {
                count++;
                if (count == countCubes - 1)
                {
                    count = 0;
                    swapCubes.positionOn = false;
                    ClearRowY(cubes, Mathf.RoundToInt(cubes[i].transform.position.y));
                }
                    
            }
            else
            {
                count = 0;
            }
        }
    }

    //if row X-axis have matches then clear row X
    void ClearRowX(GameObject[] array, int x)
    {
        foreach (GameObject gameObject in array)
        {
            if (gameObject.transform.position.x == x)
            {
                addNewCubes.ClearRow(gameObject);
                Destroy(gameObject);
            }
        }
        StartCoroutine(addNewCubes.AddCubes());
        audioSource.Stop();
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(clearRow);
        }
    }

    //if row Y-axis have matches then clear row Y
    void ClearRowY(GameObject[] array, int y)
    {
        foreach (GameObject gameObject in array)
        {
            if (gameObject.transform.position.y == y)
            {
                addNewCubes.ClearRow(gameObject);
                Destroy(gameObject);
            }
        }
        StartCoroutine(addNewCubes.AddCubes());
        audioSource.Stop();
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(clearRow);
        }
    }

    //sort array for count X-axis elements
    void SortX(GameObject[] array)
    {
        int indx;
        for (int i = 0; i < array.Length; i++)
        {
            indx = i;
            for (int j = i; j < array.Length; j++)
            {
                if (array[j].transform.position.x < array[indx].transform.position.x)
                {
                    indx = j;
                }
            }
            if (array[indx] == array[i])
                continue;
            GameObject temp = array[i];
            array[i] = array[indx];
            array[indx] = temp;
        }
    }

    //sort array for count Y-axis elements
    void SortY(GameObject[] array)
    {
        int indx;
        for (int i = 0; i < array.Length; i++)
        {
            indx = i;
            for (int j = i; j < array.Length; j++)
            {
                if (array[j].transform.position.y < array[indx].transform.position.y)
                {
                    indx = j;
                }
            }
            if (array[indx] == array[i])
                continue;
            GameObject temp = array[i];
            array[i] = array[indx];
            array[indx] = temp;
        }
    }
}