using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordColor : MonoBehaviour
{
    public void OnClic()
    {
        GetComponentInChildren<Text>().color = new Color(1, 0, 0, 1);
    }
    public void RetCol()
    {
        GetComponentInChildren<Text>().color = new Color(0, 0, 0, 1);
    }
}
