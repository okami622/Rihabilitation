using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System.Collections;

public class EMojiEvent : MonoBehaviour
{
    static string PREFAB_PATH = "Emoji/Button";

    [SerializeField] private Text timer;
    [SerializeField] private Text AnswerText0;
    [SerializeField] private Text AnswerText1;
    [SerializeField] private Dropdown FileValue;
    [SerializeField] private Dropdown Mode1;
    [SerializeField] private Dropdown Mode2;
    [SerializeField] private RawImage rawImage;

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
    private EMojiPrefabMessage DP;
    private Timer TM;
    private SE sE;
    EventSystem ev;

    int Count, MaxCount, TCount, Answer, Answer_Count;
    private bool first;
    private int[] coodinate;

    private string[] files;
    private string[] files_name;

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
        if (Mode2.value == 0) themeShuffle();

        Next();
    }

    public void Next()
    {
        if (!first)
        {
            Count++;
            TCount = 0;
            Answer = 0;
            Answer_Count = 0;
        }

        if (Count > MaxCount - 1) return;

        first = false;

        ChangeSprite(files_name[Count]);

        TM.StartTimer();

        Createobj(14, 7);
    }

    public bool EMojiJudge(string word)//buttonのテキストを入れて判定
    { 
        ev = EventSystem.current;
        sE = ev.GetComponent<SE>();

        bool tmp = false;
        UnityEngine.Debug.Log(word);
        UnityEngine.Debug.Log(files_name[Count]);
        if (word[0].Equals(files_name[Count][TCount]))
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
        if (files_name[Count].Length - 1 < TCount)
        {
            Count++;
            TCount = 0;
            TM.Stop();
            Clear(Answer, Answer_Count - Answer);
            Answer = 0;
            Answer_Count = 0;
        }

        return tmp;
    }

    private void Clear(int a, int b)
    {
        AnswerText0.text = "正解  " + a.ToString();
        AnswerText1.text = "間違い" + b.ToString();
    }

    private void Createobj(int sx, int sy)
    {
        var blocks = GameObject.FindGameObjectsWithTag("InitiateObj");
        foreach (var clone in blocks)
        {
            Destroy(clone);
        }

        int i = 0;
        coodinate = new int[sx * sy];
        for (int x = sx - 1; x >= 0; x--)
            for (int y = sy - 1; y >= 0; y--)
                coodinate[i++] = sx * x + y;

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

            DP = instance.GetComponent<EMojiPrefabMessage>();
            DP.Mode(Mode1.value);

            if (files_name[Count].Length > i)
            {
                UIText.text = Char.ToString(files_name[Count][i]);
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

        ReadFiles();

        MaxCount = files_name.Length;
    }

    private void ReadFiles()
    {
        string path = Application.dataPath + "/StreamingAssets/絵を文字に/" + GetFName() + "/";
        files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
        files_name = files;
        string pattern = "[.png]";
        for (int i = 0;i<files.Length;i++)
        {
            files_name[i] = Regex.Replace(Path.GetFileName(files[i]), pattern, "");
        }
    }

    private void themeShuffle()
    {
        string tmp;
        int rnd1, rnd2;
        for (int i = 0; i < 400; i++)
        {
            rnd1 = UnityEngine.Random.Range(0, MaxCount);
            rnd2 = UnityEngine.Random.Range(0, MaxCount);
            tmp = files_name[rnd1];
            files_name[rnd1] = files_name[rnd2];
            files_name[rnd2] = tmp;
        }
        for (int i = 0; i < files_name.Length; i++)
        {
            UnityEngine.Debug.Log(files_name[i]);
        }

    }

    private void ChangeSprite(string filename)
    {

        byte[] bytes = File.ReadAllBytes(Application.dataPath + "/StreamingAssets/絵を文字に/" + GetFName() + "/" + filename + ".png");
        Texture2D texture = new Texture2D(200, 200); ;
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(bytes);

        rawImage.texture = texture;
        rawImage.SetNativeSize();
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

    private string GetFName()
    {
        switch (FileValue.value)
        {
            case 0:
                return "やさい";
            case 1:
                return "乗り物";
            case 2:
                return "動物";
            case 3:
                return "その他";
            default:
                return "";
        }
    }
}
