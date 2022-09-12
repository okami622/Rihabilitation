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

public class KanaEvent: MonoBehaviour
{
    static string PREFAB_PATH = "KanaHiroi/Button";

    [SerializeField] private Text timer;
    [SerializeField] private Text AnswerText0;
    [SerializeField] private Text AnswerText1;
    [SerializeField] private Dropdown FileValue;
    [SerializeField] private Dropdown Mode1;
    [SerializeField] private Dropdown Mode2;

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
    private FileRead FR;
    private PrefabMessage DP;
    private MojiBlink MB;
    private Timer TM;
    private SE sE;
    EventSystem ev;

    int Count,MaxCount,TCount,Answer,Answer_Count;
    string guitxt;
    string[] Newtxt;
    private bool first;
    private int[] coodinate;

    public Camera targetcamera = null;

    void start()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
    }

    //ゲーム開始
    public void GameStart()
    {
        Initialize();

        Config();
        if (Mode2.value == 0)themeShuffle();

        Next();
    }

    public void Next()//修正必要
    {
        if (!first) {
            Count++;
            TCount = 0;
            Answer = 0;
            Answer_Count = 0;
        }

        if (Count > MaxCount - 1) return;

        first = false;

        TM.StartTimer();

        Createobj(14,7);
    }

    public bool KanaJudge(string word)//buttonのテキストを入れて判定
    {
        ev = EventSystem.current;

        sE = ev.GetComponent<SE>();

        bool tmp = false;
        if (word[0].Equals(Newtxt[Count][TCount]))
        {
            tmp = true;
            TCount++;
            sE.OK();
        }
        else
        {
            sE.NG();
        }

        if (tmp) Answer++;
        Answer_Count++;
        
        if (Newtxt[Count].Length - 1 < TCount)
        {
            TM.Stop();
            Clear(Answer, Answer_Count - Answer);
        }

        return tmp;
    }

    private void Clear(int a,int b)
    {
        AnswerText0.text = "正解  " + a.ToString();
        AnswerText1.text = "間違い" + b.ToString();
    }

    public void Hint()
    {
        var blocks = GameObject.FindGameObjectsWithTag("InitiateObj");
        int j = 0;
        foreach (var clone in blocks)
        {
            DP = clone.GetComponent<PrefabMessage>();
            if (DP.flg)
            {
                GameObject GrandText = clone.transform.Find("Canvas").gameObject.transform.Find("Text").gameObject;

                Text UIText = GrandText.GetComponent<Text>();
                if (UIText.text[0].Equals(Newtxt[Count][TCount]))
                {
                    MB = clone.GetComponent<MojiBlink>();
                    MB.SetIsBlink();
                }
                UnityEngine.Debug.Log(j);
                j++;
            }
        }
    }

    private void Createobj(int sx, int sy)
    {
        var blocks = GameObject.FindGameObjectsWithTag("InitiateObj");
        foreach (var clone in blocks)
        {
            Destroy(clone);
        }

        //ここの部分は大幅修正
        //ランダム生成そのものには成功してるから成否判定をどうのこうの
        int i = 0;
        coodinate = new int[sx * sy];
        for (int x = sx - 1; x >= 0; x--)
            for (int y = sy - 1; y >= 0; y--)
                coodinate[i++] = sx * x + y;
        //この辺苦戦中
        Vector3 V;
        textShuffle(sx * sy);
        for (i = 0; i < sx * (sy); i++)
        {
            GameObject obj = (GameObject)Resources.Load(PREFAB_PATH);

            // プレハブを元にオブジェクトを生成する
            GameObject instance = (GameObject)Instantiate(obj,
             V = new Vector3(Normalization(coodinate[i] / sx, sx - 1, 0, xMax, xMin), Normalization(coodinate[i] % sx, sy - 1, 0, yMax, yMin), 0.0f),
                                                          Quaternion.identity);
            GameObject childcanvas = instance.transform.Find("Canvas").gameObject;
            childcanvas.GetComponent<Canvas>().worldCamera = targetcamera;
            GameObject GrandText = instance.transform.Find("Canvas").gameObject.transform.Find("Text").gameObject;

            CPos = GrandText.GetComponent<TextPos>();
            CPos.TextPosition(Normalization(coodinate[i] / sx, sx - 1, 0, cxMax, cxMin), Normalization(coodinate[i] % sx, sy - 1, 0, cyMax, cyMin));
            //ここ問題
            Text UIText = GrandText.GetComponent<Text>();

            DP = instance.GetComponent<PrefabMessage>();
            DP.Mode(Mode1.value);

            if (Newtxt[Count].Length > i)
            {
                UIText.text = Char.ToString(Newtxt[Count][i]);
            }
            else
            {
                DP.NullText();
            }
        }
    }

    //正規化
    private float Normalization(float C, float dMax, float dMin, float Max, float Min)
    {
        return ((C - dMin) / (dMax - dMin) * (Max - Min) + Min);
    }

    private void Initialize()
    {
        TM = timer.GetComponent<Timer>();
        first = true;
        Count = 0;
        MaxCount = 0;
        TCount = 0;
        Answer = 0;
        Answer_Count = 0;
    }

    private void Config()
    {
        TM.Initialize();
        TM.StartTimer();

        FR = this.GetComponent<FileRead>();
        FR.FLoad("仮名ひろい/" + (FileValue.value + 1).ToString() + ".txt");

        guitxt = FR.RString();

        Newtxt = guitxt.Split(char.Parse("\n"));

        string pattern = "[.png]";
        for (int i = 0; i < Newtxt.Length; i++)
        {
            Newtxt[i] = Newtxt[i].Trim('\r');
        }

        MaxCount = Newtxt.Length;
    }

    private void themeShuffle()
    {
        string tmp;
        int rnd1, rnd2;
        for (int i = 0; i < 400; i++)
        {
            rnd1 = UnityEngine.Random.Range(0, MaxCount);
            rnd2 = UnityEngine.Random.Range(0, MaxCount);
            tmp = Newtxt[rnd1];
            Newtxt[rnd1] = Newtxt[rnd2];
            Newtxt[rnd2] = tmp;
        }
        for (int i = 0; i < Newtxt.Length; i++)
        {
            UnityEngine.Debug.Log(Newtxt[i]);
        }

    }
    private void textShuffle(int randmax)
    {
        int tmp;
        int rnd1, rnd2;
        for (int i = 0; i < 6000; i++)
        {
            rnd1 = UnityEngine.Random.Range(0, randmax);
            rnd2 = UnityEngine.Random.Range(0, randmax);
            tmp = coodinate[rnd1];
            coodinate[rnd1] = coodinate[rnd2];
            coodinate[rnd2] = tmp;
        }

    }
}

