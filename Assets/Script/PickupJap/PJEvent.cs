using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PJEvent : MonoBehaviour
{
    [SerializeField] private Dropdown Vowels;
    [SerializeField] private Dropdown Consonants;

    [SerializeField] private Toggle mode1;
    [SerializeField] private Toggle mode2;
    [SerializeField] private Toggle mode3;
    [SerializeField] private Toggle mode4;
    [SerializeField] private Toggle mode5;

    private DiscrimeTable DT;
    private SE se;

    public int count;
    public int score;

    void start()
    {
        
        Initialize();
    }

    public void Initialize()
    {
        count = 0;
        score = 0;
    }

    public bool Disobj(string word)
    {
        count++;
        se = this.GetComponent<SE>();
        DT = this.GetComponent<DiscrimeTable>();
        bool a = false;
        if (mode1.isOn)
        {
            if (DT.Discrime(word, 0, Vowels.value)) a = true;
        }
        if (mode2.isOn)
        {
            if (DT.Discrime(word, 1, Consonants.value)) a = true;
        }
        if (mode3.isOn)
        {
            if (DT.Discrime(word, 2, 0)) a = true;
        }
        if (mode4.isOn)
        {
            if (DT.Discrime(word, 3, 0)) a = true;
        }
        if (mode5.isOn)
        {
            if (DT.Discrime(word, 4, 0)) a = true;
        }

        if (a) 
        {
            score++;
            se.OK();
        }
        else
        {
            se.NG();
        }

        return a;
    }
}
