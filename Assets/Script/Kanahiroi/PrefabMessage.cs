using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PrefabMessage : MonoBehaviour
{
    EventSystem ev;
    private KanaEvent MA;
    private GameObject ChildObject;
    private string tex;
    public bool flg = true;
    private int mode;

    public void onClick()
    {
        ev = EventSystem.current;
        
        MA = ev.GetComponent<KanaEvent>();

        if (flg)
        {
            ChildObject = transform.GetChild(0).gameObject;
            ChildObject = ChildObject.transform.GetChild(0).gameObject;

            tex = ChildObject.GetComponent<Text>().text;
            if (MA.KanaJudge(tex))
            {
                if (getErase())
                {
                    Destroy(this.gameObject);
                }
                flg = false;
            }
        }
    }

    //テキストがないとき用のやつ
    public void NullText()
    {
        flg = false;
    }

    private bool getErase()
    {
        switch (mode)
        {
            case 0:
                return true;
            case 1:
                return false;
        default:
            return true;
        }
    }

    public void Mode(int x)
    {
        mode = x;
    }
}
