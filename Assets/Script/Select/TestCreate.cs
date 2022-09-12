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


public class TestCreate : MonoBehaviour
{
    //読み込み用prefab,AssetsfolderにResourcesフォルダを作成してプレハブを入れてください
    static string PREFAB_PATH = "Select/Button1";

    [SerializeField] private Dropdown mode1;
    [SerializeField] private Dropdown mode2;
    [SerializeField] private Dropdown val;
    [SerializeField] private Text timer;

    //画面の最大値、最小値
    private static float wMax = 1920.0f;
    private static float wMin = 0.0f;
    private static float hMax = 1080.0f;
    private static float hMin = 0.0f;

    //生成位置の最大値最小値
    private static float xMax = 8.3f;
    private static float xMin = -8.3f;
    private static float yMax = 4.4f;
    private static float yMin = -4.4f;

    //Canvasの最大値最小値
    private static float cxMax = 1499.0f;
    private static float cxMin = -1499.0f;
    private static float cyMax = 795f;
    private static float cyMin = -795f;

    private TextPos CPos;
    private DestroyObj DObj;
    EventSystem ev;
    private GameEvent ge;
    private SE Se;
    private Timer TM;

    public Camera targetcamera = null;
    private char[] src = { 'あ', 'い', 'う', 'え', 'お', 'か', 'き', 'く', 'け', 'こ', 'さ', 'し', 'す', 'せ', 'そ', 'た', 'ち', 'つ', 'て', 'と', 'な', 'に', 'ぬ', 'ね', 'の', 'は', 'ひ', 'ふ', 'へ', 'ほ' };

    //private int size = 5;

    void start()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
    }

    //座標変換用Vector3生成用関数
    //vector3(x,y,z)(x{-7→7},y{-4,4})
    private Vector3 convertVector(float x, float y)
    {

        Vector3 v = new Vector3(Normalization(x, wMax, wMin, xMax, xMin), Normalization(y, hMax, hMin, yMax, yMin), 0.0f);

        return v;
    }

    //正規化
    private float Normalization(float C, float dMax, float dMin, float Max, float Min)
    {
        return ((C - dMin) / (dMax - dMin) * (Max - Min) + Min);
    }

    private string Gamemode2(int i)
    {
        if(mode2.value != 1)
        {
            return Convert.ToString(i + 1);
        }
        else
        {
            if(i % 2 != 0)
            {
                return src[i/2].ToString();
            }
            else
            {
                return Convert.ToString(i / 2 + 1);
            }

            return "0";
        }
        return "0";
    }

    public void onClick()
    {
        //tag付きのオブジェクトの削除
        var blocks = GameObject.FindGameObjectsWithTag("InitiateObj");
        foreach(var clone in blocks)
        {
            Destroy(clone);
        }
        ev = EventSystem.current;
        ge = ev.GetComponent<GameEvent>();
        ge.Initialize();
        ge.GetComponent<SE>().Play();

        if(mode2.value != 0)
        {
            Createobj(2 * (val.value * 5 + 5));
        }
        else
        {
            Createobj(val.value * 5 + 5);
        }
        TM = timer.GetComponent<Timer>();
        TM.Initialize();
        TM.StartTimer();
    }

    private void Createobj(int j)
    {
        for (int i = 0; i <j ; i++)
        {
            float x = UnityEngine.Random.Range(270.0f, wMax);
            float y = UnityEngine.Random.Range(hMin, hMax - 180.0f);
            Vector3 V = convertVector(x, y);

            GameObject obj = (GameObject)Resources.Load(PREFAB_PATH);

            // プレハブを元にオブジェクトを生成する
            GameObject instance = (GameObject)Instantiate(obj,
                                                          V,
                                                          Quaternion.identity);
            Renderer curRenderer = instance.GetComponent<SpriteRenderer>();
            curRenderer.sortingOrder = -i;
            DObj = instance.GetComponent<DestroyObj>();
            DObj.id = i;
            DObj.Initialize(mode1.value);

            GameObject childcanvas = instance.transform.Find("Canvas").gameObject;
            childcanvas.GetComponent<Canvas>().worldCamera = targetcamera;
            GameObject GrandText = instance.transform.Find("Canvas").gameObject.transform.Find("Text").gameObject;

            CPos = GrandText.GetComponent<TextPos>();
            CPos.TextPosition(Normalization(x, wMax, wMin, cxMax, cxMin), Normalization(y, hMax, hMin, cyMax, cyMin));

            Text UIText = GrandText.GetComponent<Text>();

            UIText.text = Gamemode2(i);
            instance.GetComponent<DestroyObj>().id = i;
        }
    }
}
