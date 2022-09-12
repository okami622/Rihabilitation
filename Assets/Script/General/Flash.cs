using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash: MonoBehaviour
{

    //public
    public float speed = 1.0f;

    //private
    private Text text;
    private SpriteRenderer image;
    private float time;
    private bool flg;

    private void Initialize()
    {
        image = this.gameObject.GetComponent<SpriteRenderer>();
        flg = false;
    }

    void Update()
    {
        if (flg)//点滅条件
        {
            image.material.color = GetAlphaColor(image.material.color);
        }
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 10.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }

    public void CFlash()
    {
        Initialize();
        flg = !flg;
    }

    public void OffFlash()
    {
        flg = false;
    }
}