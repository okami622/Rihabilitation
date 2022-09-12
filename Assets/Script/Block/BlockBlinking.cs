using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class BlockBlinking : MonoBehaviour
{
    public Button start_button;
    public Button hinto_button;
    public Button setting_button;
    public Button[] bBlock = new Button[120]; //ブロック
    public Button M_button;
    public Button Yes_button;
    public Button No_button;
    public Button End_button;
    public Button Ans_button;
    public Button exit_button;
    public Dropdown dropdown;
    public Toggle N_order;  //正順(Normal order)
    public Toggle R_order;  //逆順(Reverse order)
    public int[] x;
    public int[] r;
    public int[] Memory = new int[120];
    public int[] ans;
    public int P_index = 1;
    public int A_index = 0;
    public int count = 0;
    public int R_count = 0; //逆順の正解数カウント
    public int start_flag = 0;
    public int setting_flag = 0;
    public int question_flag = 0;   //ブロック点滅中
    public int bclick_flag = 0;
    public float interval;
    //MessagePanel
    public GameObject M_panel;      //問題が正解または不正解時のパネル
    public GameObject Next_panel;   //次問題に移るかどうかのパネル
    public GameObject End_panel;    //全問題(9問)終了時のパネル
    //MessagePanelText
    public Text MPtext;
    //現在の問題数
    public Text problem_text;
    //全問終了後のパネル表示とテキスト表示
    public GameObject ans_panel;
    public Text[] ans_text = new Text[9];
    //sound
    GameObject se;
    // Start is called before the first frame update
    void Start()
    {
        hinto_button.interactable = false;
        se = GameObject.Find("EventSystem");
    }
    // Update is called once per frame
    void Update()
    {
        problem_text.text = P_index.ToString("");
        if(dropdown.value == 0)
        {
            interval = 1.0f;
        }
        else if (dropdown.value == 1)
        {
            interval = 2.0f;
        }
        else if (dropdown.value == 2)
        {
            interval = 3.0f;
        }
    }
    public void InputSetting()
    {
        //bBlock[0].GetComponent<Image>().color = Color.red;
        if(setting_flag == 1)
        {
            for(int si = 0; si < 120; si++)
            {
                bBlock[si].GetComponent<Image>().color = Color.white;
            }
            x = Enumerable.Range(0, 120).OrderBy(n => Guid.NewGuid()).Take(25).ToArray();
            for (int i = 0; i < 25; i++)
            {
                bBlock[x[i]].GetComponent<Image>().color = Color.black;
            }
        }
        else if (setting_flag == 0)
        {
            x = Enumerable.Range(0, 120).OrderBy(n => Guid.NewGuid()).Take(25).ToArray();
            for (int i = 0; i < 25; i++)
            {
                bBlock[x[i]].GetComponent<Image>().color = Color.black;
            }
            setting_flag = 1;
        }
    }
    //ブロック点滅の再生
    public void InputStart()
    {
        if(setting_flag == 0)
        {
            return;
        }
        if (start_flag == 0)
        {
            if(R_order.isOn)
            {
                count = P_index - 1;
            }
            StartCoroutine("wait");
            start_flag = 1;
        }
        setting_button.interactable = false;
        start_button.interactable = false;
    }
    public void InputHinto()
    {

        if (bclick_flag == 0)
        {
            if (start_flag == 1)
            {
                StartCoroutine("wait");
            }
        }
        
    }
    IEnumerator wait()
    {
        hinto_button.interactable = false;
        for (int j = 0; j < P_index; j++)
        {
            do
            {
                r = Enumerable.Range(0, 120).OrderBy(n => Guid.NewGuid()).Take(1).ToArray();
            } while (x[j] != r[0]);
            bBlock[r[0]].GetComponent<Image>().color = Color.red;
            Memory[j] = x[j];       //点滅したブロックを記憶
            yield return new WaitForSecondsRealtime(interval);
            bBlock[r[0]].GetComponent<Image>().color = Color.black;
        }
        question_flag = 1;
        hinto_button.interactable = true;
    }
    public void BlockClick(int number)
    {
        if (question_flag == 0)
        {
            return;
        }
        else if (question_flag == 1)
        {
            //正順の処理
            if (N_order.isOn)
            {
                if (Memory[count] == number)    //正解の時の処理
                {
                    //sound
                    se.GetComponent<SE>().OK();
                    bBlock[Memory[count]].GetComponent<Image>().color = Color.white;
                    count++;
                    if (count == P_index)
                    {
                        ans[A_index] = count;
                        count = 0;
                        MPtext.text = "正解";
                        M_panel.SetActive(true);
                    }
                }
                else if (Memory[count] != number)
                {
                    se.GetComponent<SE>().NG();
                    ans[A_index] = count;
                    count = 0;
                    MPtext.text = "不正解";
                    M_panel.SetActive(true);
                }
            }
            //逆順の処理
            else if(R_order.isOn)
            {
                if (Memory[count] == number)    //正解の時の処理
                {
                    se.GetComponent<SE>().OK();
                    bBlock[Memory[count]].GetComponent<Image>().color = Color.white;
                    count--;
                    R_count++;
                    if (count == -1)
                    {
                        ans[A_index] = R_count;
                        R_count = 0;
                        MPtext.text = "正解";
                        M_panel.SetActive(true);
                    }
                }
                else if (Memory[count] != number)
                {
                    se.GetComponent<SE>().NG();
                    ans[A_index] = R_count;
                    R_count = 0;
                    MPtext.text = "不正解";
                    M_panel.SetActive(true);
                }
            }
        }
        bclick_flag = 1;
    }
    public void M_panelClose()
    {
        if (P_index == 9)
        {
            M_panel.SetActive(false);
            End_panel.SetActive(true);
        }
        else
        {
            M_panel.SetActive(false);
            Next_panel.SetActive(true);
        }
        question_flag = 0;
    }
    public void InputNextYes()  //次問題に移行
    {
        Next_panel.SetActive(false);
        A_index++;
        P_index++;
        InputSetting();
        //block setting
        //再生（スタート）ボタンを押してもらう
        start_flag = 0;
        bclick_flag = 0;
        setting_button.interactable = true;
        start_button.interactable = true;
    }
    public void InputNextNo()   //移行せずに終了
    {
        Next_panel.SetActive(false);
        End_panel.SetActive(true);
        start_flag = 0;
        setting_button.interactable = true;
        start_button.interactable = true;
    }
    public void InputEndOK()    //ゲーム終了時のボタン(結果画面に移行)
    {
        int i = 1;
        ans_panel.SetActive(true);
        for(int j = 0; j < P_index; j++)
        {
            ans_text[j].text = i.ToString("");
            ans_text[j].text += " : ";
            ans_text[j].text += ans[j].ToString("");
            i++;
        }
        End_panel.SetActive(false);
    }
    public void InputAnsOK()    //結果画面の終了
    {
        ans_panel.SetActive(false);
        A_index = 0;
        for (int i = 0; i < 120; i++)
        {
            bBlock[i].GetComponent<Image>().color = Color.white;
        }
        for (int j = 0; j < P_index; j++)
        {
            ans_text[j].text = "";
        }
        P_index = 1;
        start_flag = 0;
        setting_button.interactable = true;
        start_button.interactable = true;
    }
    public void InputExit()     //終了ボタンクリック時
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }
}
