using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MojiBlink : MonoBehaviour
{
    private float speed = 1.0f;
    private Text tex;
    private bool IsBlink;
    private float time, dtime, T;
    private GameObject ChildObject;

    void Awake()
    {
        ChildObject = transform.GetChild(0).gameObject;
        ChildObject = ChildObject.transform.GetChild(0).gameObject;
        tex = ChildObject.GetComponent<Text>();
    }
    void Update()
    {
        if (IsBlink)
        {
            tex.color = GetAlphaColor(tex.color);
            if (dtime > T)
            {
                IsBlink = false;
                tex.color = new Color(tex.color.r, tex.color.g, tex.color.b, 1.0f);
            }
            dtime += Time.deltaTime;
        }
    }

    public void SetIsBlink()//点滅（Next不明）
    {
        IsBlink = true;
        T = 2.0f;
    }

    public void stopBlink()//onClickに入れる
    {
        IsBlink = false;
        tex.color = new Color(tex.color.r, tex.color.g, tex.color.b, 1.0f);
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 10.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}