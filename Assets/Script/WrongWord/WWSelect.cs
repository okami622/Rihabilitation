using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWSelect : MonoBehaviour
{
    private bool selectflg = true;
    private GameObject manager;
    void Start()
    {
        manager = GameObject.FindWithTag("Manager");
    }

    public void WWSelected()
    {
        if (selectflg)
        {
            manager.GetComponent<WWManager>().SetSelectObject(this.gameObject);
            
        }
    }
    public void setWWflg()
    {
        selectflg = false;
    }

    public void resetWWflg()
    {
        selectflg = true;
    }
    public bool getWWflg()
    {
        return selectflg;
    }
}
