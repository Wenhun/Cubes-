using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCubes : MonoBehaviour
{
    ColorFinder colorFinder;
    AddNewCubes addNewCubes;

    public GameObject cube1 = null, cube2 = null;

    Vector3 position1, position2;

    public bool positionOn = true;

    AudioSource audioSource;
    [SerializeField] AudioClip swap;

    private void Start()
    {
        colorFinder = GetComponent<ColorFinder>();
        addNewCubes = GetComponent<AddNewCubes>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //swap cubes
        if (cube1 != null && cube2 != null )
        {
            if (cube1.tag != cube2.tag)
                Swap();
            else
            {
                //light off
                cube1.transform.GetChild(0).gameObject.SetActive(false);
                cube2.transform.GetChild(0).gameObject.SetActive(false);

                cube1.transform.GetChild(2).gameObject.SetActive(false);
                cube1.transform.GetChild(3).gameObject.SetActive(false);
                cube1.transform.GetChild(4).gameObject.SetActive(false);
                cube1.transform.GetChild(5).gameObject.SetActive(false);

                //reset picked cubes
                positionOn = true;
                cube1 = null;
                cube2 = null;
            }
        }
    }

    void Swap()
    {
        //off swap for other cubes
        if (positionOn)
        {
            position1 = cube1.transform.position;
            position2 = cube2.transform.position;
            positionOn = false;
        }

        //slowly movement for cubes
        float travelPercent = 0f;
        travelPercent += Time.deltaTime * 3f;

        cube1.transform.position = Vector3.MoveTowards(cube1.transform.position, position2, travelPercent);
        cube2.transform.position = Vector3.MoveTowards(cube2.transform.position, position1, travelPercent);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(swap);
        }
        //then swap is done
        if (cube1.transform.position == position2 && cube2.transform.position == position1)
        {

            //light off
            cube1.transform.GetChild(0).gameObject.SetActive(false);
            cube2.transform.GetChild(0).gameObject.SetActive(false);

            //sending information about cubes
            addNewCubes.SwapIntoArray(cube1, cube2);

            //ready for new swap
            positionOn = true;
            cube1 = null;
            cube2 = null;

            //search matches after swap
            colorFinder.FindColor("Red");
            colorFinder.FindColor("Blue");
            colorFinder.FindColor("Green");
            colorFinder.FindColor("Yellow");
        }
    }
}
