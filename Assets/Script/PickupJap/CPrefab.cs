using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System.Collections;

public class CPrefab : MonoBehaviour
{
    static string PREFAB_PATH = "PickupJapanese/Button";

    [SerializeField] private Text timer;
    [SerializeField] private Dropdown FileValue;

    //画面の最大値、最小値
    private static float wMax = 1920.0f;
    private static float wMin = 0.0f;
    private static float hMax = 1080.0f;
    private static float hMin = 0.0f;

    //生成位置の最大値最小値
    private static float xMax = 8.3f;
    private static float xMin = -5.82f;
    private static float yMax = 2.75f;
    private static float yMin = -4.4f;

    //Canvasの最大値最小値
    private static float cxMax = 1499.0f;
    private static float cxMin = -1046.0f;
    private static float cyMax = 495f;
    private static float cyMin = -795f;

    private TextPos CPos;
    private DiscrimeTable DT;
    private FileRead FR;
    private Destroyprefab DP;
    private Timer TM;
    EventSystem ev;
    private PJEvent PJ;

    public Camera targetcamera = null;

    void start()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
    }

    //ゲーム開始
    public void GameStart()
    {
        var blocks = GameObject.FindGameObjectsWithTag("InitiateObj");
        foreach (var clone in blocks)
        {
            Destroy(clone);
        }
        ev = EventSystem.current;
        PJ = ev.GetComponent<PJEvent>();
        PJ.Initialize();
        Createobj(14,7);
    }

    private void Createobj(int sx,int sy)
    {
        FR = this.GetComponent<FileRead>();
        FR.FLoad("文字ひろい/" + (FileValue.value + 1).ToString() + ".txt");

        string guitxt = FR.RString();
        //UnityEngine.Debug.Log(guitxt);

        for (int x = sx-1; x >= 0; x--)
        {
            for (int y = sy - 1; y >= 0; y--)
            {
                Vector3 V = new Vector3(Normalization(x, sx-1, 0, xMax, xMin), Normalization(y, sy-1, 0, yMax, yMin), 0.0f);

                GameObject obj = (GameObject)Resources.Load(PREFAB_PATH);

                // プレハブを元にオブジェクトを生成する
                GameObject instance = (GameObject)Instantiate(obj,
                                                              V,
                                                              Quaternion.identity);
                GameObject childcanvas = instance.transform.Find("Canvas").gameObject;
                childcanvas.GetComponent<Canvas>().worldCamera = targetcamera;
                GameObject GrandText = instance.transform.Find("Canvas").gameObject.transform.Find("Text").gameObject;

                CPos = GrandText.GetComponent<TextPos>();
                CPos.TextPosition(Normalization(x, sx-1, 0, cxMax, cxMin), Normalization(y, sy-1, 0, cyMax, cyMin));

                Text UIText = GrandText.GetComponent<Text>();

                if (guitxt.Length > Math.Abs(x - sx + 1) * sy + Math.Abs(y - sy + 1))
                {
                    UIText.text = Char.ToString(guitxt[Math.Abs(x - sx + 1) * sy + Math.Abs(y - sy + 1)]);
                }
                else
                {
                    DP = instance.GetComponent<Destroyprefab>();
                    DP.NullText();
                }
            }
        }

        TM = timer.GetComponent<Timer>();
        TM.Initialize();
        TM.StartTimer();
    }

    //正規化
    private float Normalization(float C, float dMax, float dMin, float Max, float Min)
    {
        return ((C - dMin) / (dMax - dMin) * (Max - Min) + Min);
    }
}
