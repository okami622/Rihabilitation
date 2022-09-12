using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour
{
    //生成するゲームオブジェクト
    [SerializeField]
    private GameObject target = default;
    [SerializeField]
    private GameObject ClearPic = default;
    [SerializeField]
    private Button button = default;
    [SerializeField]
    private Text timer = default;
    [SerializeField]
    private Text NumberSheet = default;
    [SerializeField]
    private Dropdown choiceCard = default;
    [SerializeField]
    private Dropdown CNum = default;
    [SerializeField]
    private AudioSource audioSource = default;
    [SerializeField]
    private AudioClip OK = default;
    [SerializeField]
    private AudioClip NG = default;

    private GameObject manager;
    private GameObject Choosed;
    private GameObject selectObject;
    private GameObject ChildObject;

    private bool isGame = false;
    private bool One = true;//現在の選択が一枚目かどうか
    private bool turnflg = false;
    //もとに戻すためにひっくり返しているかどうかのフラグtrueならひっくり返している

    private int clickID;
    private int MaxCard = 0;
    private int CA;//正答枚数
    private int[] Coordinate;

    private string selectname = "";
    private string[] SourceImage = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };

    //5*17
    private float time = 0f;
    private float OneSec = 0f;

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
            OneSec += Time.deltaTime;
            timer.text = time.ToString("f2");
        }
    }

    public void GameStart()//Buttonで呼び出し
    {
        if (!isGame)
        {
            ClearPic.SetActive(false);
            MaxCard = CardNumber();
            Cslot();
            InstanceCreate();
            button.interactable = false;
            isGame = true;
            One = true;
            timer.text = "" + 0;
            NumberSheet.text = "取得枚数\n" + CA;
        }
    }

    public string[] GetImage()
    {
        return SourceImage;
    }

    public bool getTurnflg()
    {
        return turnflg;
    }

    public string GetDropDown()
    {
        switch (choiceCard.value)
        {
            case 1:
                return "Animal";
            case 2:
                return "Vehicle";
            case 3:
                return "Vegetable";
            default:
                return "PlayingCard";
        }

    }

    private int CardNumber()
    {
        switch (CNum.value)
        {
            case 1:
                Coordinate = new int[24];
                return 12;
            default:
                Coordinate = new int[12];
                return 6;

        }
    }

    public void SetSelectObject(GameObject gameObject)
    {
        selectObject = gameObject;
        selectname = selectObject.name;

        if (isGame && !turnflg)
        {
            selectObject.GetComponent<Cards>().TurnCard();
            if (One)
            {
                Choosed = selectObject;
                One = false;
            }
            else
            {
                if (selectname == Choosed.name)
                {
                    audioSource.PlayOneShot(OK);
                    CA++;
                    NumberSheet.text = "取得枚数\n" + CA;
                }
                else
                {
                    audioSource.PlayOneShot(NG);
                    turnflg = true;
                    Invoke("ReturnCard", 1);
                }
                One = true;
            }
            if (CA == MaxCard) fin();
        }
    }

    private void ReturnCard()
    {
        selectObject.GetComponent<Cards>().ReturnCard();
        Choosed.GetComponent<Cards>().ReturnCard();
        turnflg = false;
    }

    private void InstanceCreate()
    {
        Vector3 Origine;
        Vector3 tmp;

        for (int i = 0; i < MaxCard * 2; i++)
        {
            if (MaxCard == 6)
            {
                Origine = new Vector3(7.36f, 7.1f, 0f);
                tmp = Origine + new Vector3(1.98f * (Coordinate[i] % 4), -2.93f * (Coordinate[i] / 4), 0f);
            }
            else
            {
                Origine = new Vector3(3.4f, 7.1f, 0f);
                tmp = Origine + new Vector3(1.98f * (Coordinate[i] % 8), -2.93f * (Coordinate[i] / 8), 0f);
            }
            var obj = Instantiate(target, tmp, Quaternion.identity, manager.transform) as GameObject;
            if (i / 2 < 9)
                obj.name = "0" + (i / 2 + 1);
            else
                obj.name = "" + (i / 2 + 1);

        }

    }

    private void Cslot()
    {
        int cardnum = 0;
        if (MaxCard == 6) cardnum = 12;
        else cardnum = 24;

        for (int i = 0; i < cardnum; i++)
        {
            Coordinate[i] = i;
        }
        int tmp;
        int rnd1, rnd2;
        for (int i = 0; i < 400; i++)
        {
            rnd1 = Random.Range(0, cardnum);
            rnd2 = Random.Range(0, cardnum);
            tmp = Coordinate[rnd1];
            Coordinate[rnd1] = Coordinate[rnd2];
            Coordinate[rnd2] = tmp;
        }
    }

    private bool isDestroy()
    {
        return false;
    }

    private void fin()
    {
        button.interactable = true;
        isGame = false;
        CA = 0;
        ClearPic.SetActive(true);
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
