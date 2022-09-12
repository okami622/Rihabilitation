using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameEvent : MonoBehaviour
{
    private int id;
    [SerializeField] private Dropdown val;
    [SerializeField] private Dropdown mode;
    [SerializeField] private GameObject Clear;
    [SerializeField] private Text timer;
    private bool flg,hflg;
    private DestroyObj Dobj;
    private SE Se;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        id = 0;
        flg = true;
        hflg = false;
        Clear.SetActive(false);
        Se = this.GetComponent<SE>();
    }

    public bool Flags(int ids)
    {
        if(id == ids)
        {
            id++;
            Se.OK();
            flg = true;
        }
        else
        {
            Se.NG();
            flg =  false;
        }
        if(id + 1 > val.value * 5 + 5 && mode.value == 0)
        {
            id = 0;
            Clear.SetActive(true);
            timer.GetComponent<Timer>().Stop();
        }
        if (id + 1 > 2 * (val.value * 5 + 5) && mode.value == 1)
        {
            id = 0;
            Clear.SetActive(true);
            timer.GetComponent<Timer>().Stop();
        }
        return flg;
    }

    public void HintEvent()
    {
        hflg = !hflg;
        if (hflg)
        {
            var blocks = GameObject.FindGameObjectsWithTag("InitiateObj");
            foreach (var clone in blocks)
            {
                Dobj = clone.GetComponent<DestroyObj>();
                if(id == Dobj.id)
                {
                    Dobj.flash();
                }
            }
        }
    }
}
