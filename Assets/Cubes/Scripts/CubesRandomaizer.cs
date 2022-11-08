using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesRandomaizer : MonoBehaviour
{
    [SerializeField] [Range(5, 7)] public int areaSize = 5;

    GameObject[,] startArray; 

    [SerializeField] GameObject redCube; //index 1
    [SerializeField] GameObject blueCube; //index 2
    [SerializeField] GameObject greenCube; //index 3
    [SerializeField] GameObject yellowCube; //index 4

    ColorFinder colorFinder;

    void Awake()
    {
        startArray = new GameObject[areaSize, areaSize];
        //create game cubes
        for (int i = 0; i < areaSize; i++)
            for (int j = 0; j < areaSize; j++)
            {
                CreateCubes(i, j);
            }
        FindSameColor();
    }

    public void CreateCubes(int x, int y)
    {
        int cubeIndex = Random.Range(1, 5);

        switch (cubeIndex)
        {
            case 1:
                startArray[x,y] = Instantiate(redCube, new Vector3(x, y, 0), Quaternion.identity, this.transform);
                break;
            case 2:
                startArray[x,y] = Instantiate(blueCube, new Vector3(x, y, 0), Quaternion.identity, this.transform);
                break;
            case 3:
                startArray[x,y] = Instantiate(greenCube, new Vector3(x, y, 0), Quaternion.identity, this.transform);
                break;
            case 4:
                startArray[x,y] = Instantiate(yellowCube, new Vector3(x, y, 0), Quaternion.identity, this.transform);
                break;
        }
    }

    //Remove the cube if there is a match
    void FindSameColor()
    {
        for (int i = 0; i < areaSize; i++)
        {
            int count = 0;
            for (int j = 0; j < areaSize - 1; j++)
            {
                if (startArray[i, j].tag == startArray[i, j + 1].tag)
                    count++;
                if (count == areaSize - 1)
                {
                    Destroy(startArray[i,j]);
                    CreateCubes(i, j);
                }
            }
        }

        for (int j = 0; j < areaSize; j++)
        {
            int count = 0;
            for (int i = 0; i < areaSize - 1; i++)
            {
                if (startArray[i, j].tag == startArray[i + 1, j].tag)
                    count++;
                if (count == areaSize - 1)
                {
                    Destroy(startArray[i,j]);
                    CreateCubes(i, j);
                }
            }  
        }
    }

}    


