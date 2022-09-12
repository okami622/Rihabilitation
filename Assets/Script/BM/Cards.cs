using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cards : MonoBehaviour
{

    private Text text;
    private Image image;
    private GameObject manager;
    private string DD;
    private bool IsOne;
    private bool reverse = false;

    private string tmp;
    void Start()
    {
        manager = GameObject.FindWithTag("Manager");
        tmp = manager.GetComponent<CardManager>().GetDropDown();
    }

    public void TurnCard()
    {
        if (!reverse)
        {
            image = this.gameObject.GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>("MatchingCard/" + tmp + "/" + transform.name);
            reverse = !reverse;
        }
    }
    public void ReturnCard()
    {
        if (reverse)
        {
            image.sprite = Resources.Load<Sprite>("MatchingCard/back");
            reverse = !reverse;
        }
    }
    public void CardSelected()
    {
        if (!manager.GetComponent<CardManager>().getTurnflg())
            if (!reverse)
                manager.GetComponent<CardManager>().SetSelectObject(this.gameObject);
    }
}
