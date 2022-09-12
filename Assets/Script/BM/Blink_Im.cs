using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink_Im : MonoBehaviour
{


    [SerializeField]
    private float speed = 1.0f;
    private Image image;
    private float time,dtime,T;
    private GameObject manager;
    private string[] ResourceImage;
    private int BlinkID;
    private bool IsBlink;


    
    void Start()
    {
        manager = GameObject.FindWithTag("Manager");

        int i = int.Parse(transform.name);
        image = this.gameObject.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Blink/" + ImageSet() );
        IsBlink = true;
    }

    void Update()
    {
        if (IsBlink)
        {
            image.color = GetAlphaColor(image.color);
            if (dtime > T)
            {
                IsBlink = false;
                image.color = new Color(image.color.r, image.color.g, image.color.b,1.0f);
            }

            dtime += Time.deltaTime;
        }
    }


    public void SetBlinkID(int a)
    {
        BlinkID = a;
    }


    public void SetIsBlink()
    {
        T = 2.0f;
        IsBlink = true;
    }

    public void BlinkIm(float timer)
    {
        dtime = 0;
        if (dtime > timer)
            IsBlink = false;
        else
            IsBlink = true;
        T = timer;
    }

    public void BlinkSelected()
    {
        manager.GetComponent<FlashManager>().SetSelectObject(this.gameObject);
        dtime += 2f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 10.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }

    private int ImageSet()
    {
        return Random.Range(1, 11);
    }
}