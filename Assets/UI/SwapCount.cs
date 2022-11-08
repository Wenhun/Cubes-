using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwapCount : MonoBehaviour
{
    TMP_Text swapText;

    void Start()
    {
        swapText = GetComponent<TMP_Text>();
        swapText.text = "Start";
    }

    public void ChangeCountSwap(int numberSwap)
    {
        swapText.text = "Available swap: " + numberSwap.ToString();
    }

}
