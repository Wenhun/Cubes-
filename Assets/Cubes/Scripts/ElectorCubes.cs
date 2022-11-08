using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElectorCubes : MonoBehaviour
{
    SwapCubes swapCubes;
    CubesRandomaizer cubesRandomaizer;
    AddNewCubes addNewCubes;
    AudioSource audioSource;
    [SerializeField] AudioClip click;

    private void Start()
    {
        swapCubes = GetComponentInParent<SwapCubes>();
        cubesRandomaizer = FindObjectOfType<CubesRandomaizer>();
        audioSource = GetComponent<AudioSource>();
        addNewCubes = FindObjectOfType<AddNewCubes>();
    }

    //choice cubes for swap
    private void OnMouseDown()
    {
        #region PICK FIRST CUBE FOR SWAP
        if (swapCubes.cube1 == null && swapCubes.positionOn)
        {
            swapCubes.cube1 = gameObject;
            //coordinates picked cube
            int x = Mathf.RoundToInt(swapCubes.cube1.transform.position.x);
            int y = Mathf.RoundToInt(swapCubes.cube1.transform.position.y);
            //light on
            swapCubes.cube1.transform.GetChild(0).gameObject.SetActive(true);
            //play sound click
            audioSource.PlayOneShot(click);
            #region turn on edge markers
            if (y == 0)
            {
                swapCubes.cube1.transform.GetChild(2).gameObject.SetActive(true);
                swapCubes.cube1.transform.GetChild(3).gameObject.SetActive(true);
                swapCubes.cube1.transform.GetChild(4).gameObject.SetActive(true);
            }
            else
            if (y == 4)
            {
                swapCubes.cube1.transform.GetChild(2).gameObject.SetActive(true);
                swapCubes.cube1.transform.GetChild(3).gameObject.SetActive(true);
                swapCubes.cube1.transform.GetChild(5).gameObject.SetActive(true);
            }
            else
            if (x == 0)
            {
                swapCubes.cube1.transform.GetChild(3).gameObject.SetActive(true);
                swapCubes.cube1.transform.GetChild(4).gameObject.SetActive(true);
                swapCubes.cube1.transform.GetChild(5).gameObject.SetActive(true);
            }
            else
            if (x == 4)
            {
                swapCubes.cube1.transform.GetChild(2).gameObject.SetActive(true);
                swapCubes.cube1.transform.GetChild(4).gameObject.SetActive(true);
                swapCubes.cube1.transform.GetChild(5).gameObject.SetActive(true);
            }
            else
            {
                swapCubes.cube1.transform.GetChild(2).gameObject.SetActive(true);
                swapCubes.cube1.transform.GetChild(3).gameObject.SetActive(true);
                swapCubes.cube1.transform.GetChild(4).gameObject.SetActive(true);
                swapCubes.cube1.transform.GetChild(5).gameObject.SetActive(true);
            }
            #endregion 
            #region checking neighboring cube tags
            if (x >= 0 && x < 4)
                if (addNewCubes.cubes[x + 1, y].tag == swapCubes.cube1.tag)
                {
                    swapCubes.cube1.transform.GetChild(3).gameObject.SetActive(false);
                }
            if (x > 0 && x <= 4)
                if (addNewCubes.cubes[x - 1, y].tag == swapCubes.cube1.tag)
                {
                    swapCubes.cube1.transform.GetChild(2).gameObject.SetActive(false);
                }
            if (y >= 0 && y < 4)
                if (addNewCubes.cubes[x, y + 1].tag == swapCubes.cube1.tag)
                {
                    swapCubes.cube1.transform.GetChild(4).gameObject.SetActive(false);
                }
            if (y > 0 && y <= 4)    
                if (addNewCubes.cubes[x, y - 1].tag == swapCubes.cube1.tag)
                {
                    swapCubes.cube1.transform.GetChild(5).gameObject.SetActive(false);
                }    
           #endregion
            #region disable markers in corners
            if (x == 0 && y == 0)
            {
                swapCubes.cube1.transform.GetChild(2).gameObject.SetActive(false);
                swapCubes.cube1.transform.GetChild(5).gameObject.SetActive(false);
            }
            if (x == 0 && y == 4)
            {
                swapCubes.cube1.transform.GetChild(2).gameObject.SetActive(false);
                swapCubes.cube1.transform.GetChild(4).gameObject.SetActive(false);
            }
            if (x == 4 && y == 0)
            {
                swapCubes.cube1.transform.GetChild(3).gameObject.SetActive(false);
                swapCubes.cube1.transform.GetChild(5).gameObject.SetActive(false);
            }
            if (x == 4 && y == 4)
            {
                swapCubes.cube1.transform.GetChild(3).gameObject.SetActive(false);
                swapCubes.cube1.transform.GetChild(4).gameObject.SetActive(false);
            }
           #endregion
        }
        else
            //block cubes if they not on axises first cube
            if ((gameObject.transform.position.x == swapCubes.cube1.transform.position.x + 1 && gameObject.transform.position.y == swapCubes.cube1.transform.position.y) ||
                (gameObject.transform.position.x == swapCubes.cube1.transform.position.x - 1 && gameObject.transform.position.y == swapCubes.cube1.transform.position.y) ||
                (gameObject.transform.position.x == swapCubes.cube1.transform.position.x && gameObject.transform.position.y == swapCubes.cube1.transform.position.y + 1) ||
                (gameObject.transform.position.x == swapCubes.cube1.transform.position.x && gameObject.transform.position.y == swapCubes.cube1.transform.position.y - 1))
        #endregion    
            #region PICK SECOND CUBE FOR SWAP
            {
                 if (swapCubes.cube2 == null)
                 {
                     swapCubes.cube2 = gameObject;
                     //play sound click
                     audioSource.PlayOneShot(click);
                     //light on
                     swapCubes.cube2.transform.GetChild(0).gameObject.SetActive(true);
                     //pointers off
                     swapCubes.cube1.transform.GetChild(2).gameObject.SetActive(false);
                     swapCubes.cube1.transform.GetChild(3).gameObject.SetActive(false);
                     swapCubes.cube1.transform.GetChild(4).gameObject.SetActive(false);
                     swapCubes.cube1.transform.GetChild(5).gameObject.SetActive(false);
                }
            }
            #endregion
            else
            //reset picked position
                if (swapCubes.positionOn)
                {
                    swapCubes.positionOn = false;
                    swapCubes.cube1.transform.GetChild(0).gameObject.SetActive(false);
                    swapCubes.cube1.transform.GetChild(2).gameObject.SetActive(false);
                    swapCubes.cube1.transform.GetChild(3).gameObject.SetActive(false);
                    swapCubes.cube1.transform.GetChild(4).gameObject.SetActive(false);
                    swapCubes.cube1.transform.GetChild(5).gameObject.SetActive(false);
                    swapCubes.cube1 = null;
                    swapCubes.positionOn = true;  
                }   
    }
}