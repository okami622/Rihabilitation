using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPos : MonoBehaviour
{
    public void TextPosition(float x, float y)
    {
        transform.localPosition = new Vector3(x, y, 0.0f);
    }

    public string MyText(){
        return this.GetComponent<Text>().text;
    }
}
