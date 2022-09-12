using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EMojiButton : MonoBehaviour
{
    EventSystem ev;
    private EMojiEvent CP;

    public void Next()
    {
        ev = EventSystem.current;
        CP = ev.GetComponent<EMojiEvent>();
        CP.Next();
    }

    public void Play()
    {
        ev = EventSystem.current;
        CP = ev.GetComponent<EMojiEvent>();
        CP.GameStart();
    }
}
