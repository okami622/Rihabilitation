using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DestroyObj : MonoBehaviour
{
    public int id = 0;
    private GameEvent ge;
    [SerializeField] private int mode = 0;
    private bool flg;

    public void Initialize(int a)
    {
        mode = a;
    }

    public void onClick()
    {
        EventSystem ev = EventSystem.current;
        ge = ev.GetComponent<GameEvent>();
        flg = ge.Flags(id);
        if (mode == 0 && flg)
        {
            Destroy(this.gameObject);
        }
        if (mode == 1 && flg)
        {
            this.GetComponent<Flash>().OffFlash();
            Renderer curRenderer = this.GetComponent<SpriteRenderer>();
            curRenderer.sortingOrder = -61;
        }
        if (mode == 2)
        {
            ge.Initialize();
        }
    }

    public void flash()
    {
        this.GetComponent<Flash>().CFlash();
    }
}
