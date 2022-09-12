using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class K_Button : MonoBehaviour
{
    EventSystem ev;
    private KanaEvent CP;

    public void Play()
    {
        ev = EventSystem.current;
        CP = ev.GetComponent<KanaEvent>();
        CP.GameStart();
    }

    public void Next()
    {
        ev = EventSystem.current;
        CP = ev.GetComponent<KanaEvent>();
        CP.Next();
    }

    public void Hint()
    {
        ev = EventSystem.current;
        CP = ev.GetComponent<KanaEvent>();
        CP.Hint();
    }
}
