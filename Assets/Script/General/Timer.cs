using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float countTime = 0;
    private bool flg;

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (flg)
        {
            countTime += Time.deltaTime; //スタートしてからの秒数を格納
            GetComponent<Text>().text = countTime.ToString("F2"); //小数2桁にして表示
        }
    }

    public void Initialize()
    {
        flg = false;
        countTime = 0;
        GetComponent<Text>().text = ("0.00");
    }

    public void Stop()
    {
        flg = false;
    }

    public void StartTimer()
    {
        flg = true;
    }
}