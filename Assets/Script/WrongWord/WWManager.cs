using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WWManager : MonoBehaviour
{
    [SerializeField]
    private GameObject target = default;
    [SerializeField]
    private GameObject ClearPic = default;
    [SerializeField]
    private GameObject scvRead = default;
    [SerializeField]
    private Button button = default;
    [SerializeField]
    private Text sliderVal = default;
    [SerializeField]
    private Slider slider = default;
    [SerializeField]
    private Text timer = default;
    [SerializeField]
    private Text Answered = default;
    [SerializeField]
    private Text Judge = default;
    [SerializeField]
    private AudioSource audioSource = default;
    [SerializeField]
    private AudioClip OK = default;
    [SerializeField]
    private AudioClip NG = default;

    private int MaxID=112;
    private int[] Coordinate = new int[112];
    //8*14
    private GameObject manager;
    private GameObject ChildObject;
    private GameObject selectObject;

    private bool isGame = false;

    private float time = 0f;

    private int correctCount = 0;
    private int wrongCount = 0;
    private int wrongVal=0,wrongPos;
    private int[] wronglist,wronglisttmp = new int[28];
    
    private string WW;
    private string selectname = "";
    private string[] datas;

    void Awake()
    {
        manager = GameObject.FindWithTag("Manager");
        Random.InitState(System.DateTime.Now.Millisecond);
    }


    void Update()
    {
        if (isGame)
        {
            time += Time.deltaTime;
            timer.text = time.ToString("f2");
            Answered.text = correctCount + "/" + slider.value;
        }
        else
        {
            sliderVal.text = "" + slider.value;
        }
    }

    public void GameStart()//Buttonで呼び出し
    {
        if (!isGame)
        {
            ClearPic.SetActive(false);
            wrongVal = (int)slider.value;
            var CR = scvRead.GetComponent<CsvRead>();
            datas = CR.GetCsvRead(Random.Range(0, CR.GetLnCnt()));
            wrongPos = int.Parse(datas[1]) -1;
            wronglist = new int[wrongVal];
            wronglist = wrongrand();
            timer.text = "" + 0;
            Answered.text = "0 / " + slider.value;
            InstanceCreate();
            button.interactable = false;
            isGame = true;
            correctCount = 0;
            wrongCount = 0;
            Judge.text = "正解 ：間違い\n" + correctCount + " ：" + wrongCount;
        }
    }
    
    public void SetSelectObject(GameObject gameObject)
    {
        selectObject = gameObject;
        selectname = selectObject.name;
        if (isGame)
        {
            if (selectname == WW)
            {
                selectObject.GetComponent<WWSelect>().setWWflg();
                audioSource.PlayOneShot(OK);
                correctCount++;
                Answered.text = correctCount + " / " + wrongVal;
                if (correctCount >= wrongVal)
                    fin();
            }
            else
            {
                wrongCount++;
                audioSource.PlayOneShot(NG);
                //text更新
            }
                Judge.text = "正解 ：間違い\n" + correctCount + " ：" + wrongCount;

        }
    }

    private void InstanceCreate()
    {
        for (int i = 0; i < 112; i++)
        {
            Coordinate[i] = i;
        }
        Vector3 Origine = new Vector3(2.71f, 8.05f, 0f);
        Vector3 tmp = Origine;
        WW = datas[2].Substring(0, 1);
        for (int i = 0; i < MaxID; i++)
        {
            int wlength = datas[0].Length;
            bool wwflg=false;
            tmp = Origine + new Vector3(1.15f * (Coordinate[i] % 14), -1.12f * (Coordinate[i] / 14), 0f);
            var obj = Instantiate(target, tmp, Quaternion.identity, manager.transform) as GameObject;
            var childtext = obj.GetComponentInChildren<Text>();
            for(int j=0;j<wrongVal;j++)
                if (wronglist[j] == (i / wlength))
                {
                    wwflg = true;
                    break;
                }
            if ( wwflg && ((i% wlength) == wrongPos))
                obj.name = WW;
            else
                obj.name = datas[0].Substring(i % wlength, 1);
                childtext.text = obj.name;
        }
    }

    private int[] wrongrand()
    {
        for (int i = 0; i < 28; i++)
            wronglisttmp[i] = i;

        int rnd1, rnd2,tmp;
        for (int i = 0; i < 500; i++)
        {
            rnd1 = Random.Range(0, 28);
            rnd2 = Random.Range(0, 28);
            tmp = wronglisttmp[rnd1];
            wronglisttmp[rnd1] = wronglisttmp[rnd2];
            wronglisttmp[rnd2] = tmp;
        }
        int[] list = new int[wrongVal];
        for(int i = 0; i < wrongVal; i++)
        {
            list[i] = wronglisttmp[i];
        }
        return list;
    }

    private void fin()
    {
        button.interactable = true;
        isGame = false;
        time = 0f;
        ClearPic.SetActive(true);
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
