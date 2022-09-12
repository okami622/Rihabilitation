using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FlashManager : MonoBehaviour
{
    //生成するゲームオブジェクト
    [SerializeField]
    private GameObject target = default;
    [SerializeField]
    private GameObject ClearPic = default;
    [SerializeField]
    private Button button = default;
    [SerializeField]
    private Text miss = default;
    [SerializeField]
    private Text pass = default;
    [SerializeField]
    private Text sliderVal = default;
    [SerializeField]
    private Text timer = default;
    [SerializeField]
    private Slider slider = default;
    [SerializeField]
    private AudioSource audioSource = default;
    [SerializeField]
    private AudioClip OK = default;
    [SerializeField]
    private AudioClip NG = default;

    private GameObject manager;
    private GameObject before;
    private GameObject Next;
    private GameObject ChildObject;
    private GameObject selectObject;
    private GameObject DestroyObj;

    private bool isRight = false,isGame = false;

    private int Rpass=0, Lpass = 0, Rmiss=0, Lmiss = 0, nextID=0,clickID;
    private int MaxID;
    private int[] Coordinate = new int[128];
    //8*16

    private float time = 0f;

    private string selectname = "";

    void Awake()
    {
        manager = GameObject.FindWithTag("Manager");
        Random.InitState(System.DateTime.Now.Millisecond);
        miss.text = "間違い\n左 " + Lmiss + " 右 " + Rmiss;
        pass.text = "見逃し\n左 " + Lpass + " 右 " + Rpass;
        timer.text = "" + 0;
    }

    public void GameStart()
    {

        miss.text = "間違い\n左 " + Lmiss + " 右 " + Rmiss;
        pass.text = "見逃し\n左 " + Lpass + " 右 " + Rpass;
        MaxID = (int)slider.value * 10;
        Cslot();
        InstanceCreate();
        button.interactable = false;
        BlinkNext();
        isGame = true;
        ClearPic.SetActive(false);
    }

    void Update()
    {
        if(!isGame)
        {
            sliderVal.text = "" + slider.value * 10;
        }
        else
        {
            time += Time.deltaTime;
            timer.text = time.ToString("f2");
        }
    }

    public void PassID()
    {
        if (isGame)
        {
            nextID++;
            
            if (isRight) Rpass++;
            else Lpass++;
            
            //text更新
            pass.text = "見逃し\n左 " + Lpass + " 右 " + Rpass;

            if (nextID < MaxID)
                BlinkNext();
            else
                fin();
        }
    }

    public void BlinkNext()
    {
        ChildObject = transform.GetChild(nextID).gameObject;
        Next = ChildObject;
        if (nextID != 0)
        {
            before = transform.GetChild(nextID-1).gameObject;
            before.SetActive(false);
        }


        

        Next.GetComponent<Blink_Im>().BlinkIm(1.0f);

        if (Next.transform.position.x < this.transform.position.x)
            isRight = false;
        else
            isRight = true;
        //2～10つ目
    }

    public void SetSelectObject(GameObject gameObject)
    {
        selectObject = gameObject;
        selectname = selectObject.name;
        if (isGame)
        {
            if (selectname == nextID.ToString())
            {

                audioSource.PlayOneShot(OK);
                nextID++;
                if(nextID < MaxID)
                    BlinkNext();

                else
                    fin();
            }
            else
            {
                audioSource.PlayOneShot(NG);
                if (isRight) Rmiss++;
                else Lmiss++;
                //text更新
                miss.text = "間違い\n左 " + Lmiss + " 右 " + Rmiss;
            }
            
        }
    }

    private void InstanceCreate()
    {
        Vector3 Origine = new Vector3(2.62f,8.09f,0f);
        Vector3 tmp = Origine;
        
        for (int i=0;i < MaxID; i++)
        {
            tmp = Origine + new Vector3( 1.03f * (Coordinate[i] % 16) , -1.135f * (Coordinate[i] / 16) , 0f);
            var obj = Instantiate(target, tmp, Quaternion.identity, manager.transform) as GameObject;
            obj.name = "" + i;
            obj.GetComponent<Blink_Im>().SetBlinkID(i);
        }
    }

    private void Cslot()
    {
        for (int i = 0; i < 128; i++)
        {
            Coordinate[i] = i;
        }
        int tmp;
        int rnd1,rnd2;
        for (int i = 0; i < 4000; i++)
        {
            rnd1 = Random.Range(0, 128);
            rnd2 = Random.Range(0, 128);
            tmp = Coordinate[rnd1];
            Coordinate[rnd1] = Coordinate[rnd2];
            Coordinate[rnd2] = tmp;
        }
    }

    private void fin()
    {
        button.interactable = true;
        isGame = false;
        nextID = 0;
        Lpass = 0;
        Rpass = 0;
        Lmiss = 0;
        Rmiss = 0;
        time = 0f;
        ClearPic.SetActive(true);
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}