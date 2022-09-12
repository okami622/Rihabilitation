using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Destroyprefab : MonoBehaviour
{
    private TextPos tpos;
    private PJEvent PJ;
    EventSystem ev;
    private bool flg = true;
    private DiscrimeTable DT;
    
    public void onClick()
    {
        ev = EventSystem.current;
        PJ = ev.GetComponent<PJEvent>();

        if (flg)
        {
            GameObject GrandText = this.transform.Find("Canvas").gameObject.transform.Find("Text").gameObject;
            tpos = GrandText.GetComponent<TextPos>();
            if (PJ.Disobj(tpos.MyText()))
            {
                flg = false;
            }
            flg = false;
        }
    }

    //テキストがないとき用のやつ
    public void NullText()
    {
        flg = false;
    }
}
