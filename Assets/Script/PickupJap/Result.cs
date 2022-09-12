using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Result : MonoBehaviour
{
    EventSystem ev;
    private PJEvent PJ;
    private Timer TM;

    [SerializeField] private Text COUNT;
    [SerializeField] private Text SCORE;
    [SerializeField] private Text TIMER;

    public void OnClick()
    {
        ev = EventSystem.current;
        PJ = ev.GetComponent<PJEvent>();

        COUNT.text = PJ.count.ToString();
        SCORE.text = PJ.score.ToString();

        TM = TIMER.GetComponent<Timer>();
        TM.Stop();
    }
}
