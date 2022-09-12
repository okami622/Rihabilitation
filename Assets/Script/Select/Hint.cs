using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hint : MonoBehaviour
{
    private GameEvent ge;

    public void onClick()
    {
        EventSystem ev = EventSystem.current;
        ge = ev.GetComponent<GameEvent>();
        ge.HintEvent();
    }
}
