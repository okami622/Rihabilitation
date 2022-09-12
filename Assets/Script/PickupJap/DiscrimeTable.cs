using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;

public class DiscrimeTable : MonoBehaviour
{
    private DataTable dt = new DataTable("Table");

    // Start is called before the first frame update
    void Start()
    {
        InitiateTable();
    }

    public bool Discrime(string word,int column,int a)
    {
        DataRow[] drs = dt.Select("WORD='" + word + "'");
        foreach (DataRow d in drs)
        {
            //UnityEngine.Debug.Log(d["WORD"]);
            if (d["WORD"].Equals(word))
            {
                switch (column)
                {
                    case 0:
                        if (Int32.Parse(Convert.ToString(d["VOWELS"])) == a)
                        {
                            return true;
                        }
                        break;
                    case 1:
                        if (Int32.Parse(Convert.ToString(d["CONSONANTS"])) == a)
                        {
                            return true;
                        }
                        break;
                    case 2:
                        if (d["VOICE"] == "True")
                        {
                            return true;
                        }
                        break;
                    case 3:
                        if (d["LOWER"] == "True")
                        {
                            return true;
                        }
                        break;
                    case 4:
                        if (d["KATAKANA"] == "True")
                        {
                            return true;
                        }
                        break;
                }
            }
        }
        return false;
    }

    private void InitiateTable()
    {
        //列追加
        dt.Columns.Add("WORD");
        //母音あから0んで5
        dt.Columns.Add("VOWELS",typeof(int));
        //子音あから0わで9
        dt.Columns.Add("CONSONANTS",typeof(int));
        //濁音か否か
        dt.Columns.Add("VOICE",typeof(bool));
        //小文字か否か
        dt.Columns.Add("LOWER", typeof(bool));
        //カタカナか否か
        dt.Columns.Add("KATAKANA",typeof(bool));

        //行追加
        //あ行
        dt.Rows.Add("あ", "0", "0", "false", "false", "false");
        dt.Rows.Add("い", "1", "0", "false", "false", "false");
        dt.Rows.Add("う", "2", "0", "false", "false", "false");
        dt.Rows.Add("え", "3", "0", "false", "false", "false");
        dt.Rows.Add("お", "4", "0", "false", "false", "false");

        dt.Rows.Add("ぁ", "0", "0", "false", "true", "false");
        dt.Rows.Add("ぃ", "1", "0", "false", "true", "false");
        dt.Rows.Add("ぅ", "2", "0", "false", "true", "false");
        dt.Rows.Add("ぇ", "3", "0", "false", "true", "false");
        dt.Rows.Add("ぉ", "4", "0", "false", "true", "false");

        dt.Rows.Add("ア", "0", "0", "false", "false", "true");
        dt.Rows.Add("イ", "1", "0", "false", "false", "true");
        dt.Rows.Add("ウ", "2", "0", "false", "false", "true");
        dt.Rows.Add("エ", "3", "0", "false", "false", "true");
        dt.Rows.Add("オ", "4", "0", "false", "false", "true");

        dt.Rows.Add("ァ", "0", "0", "false", "true", "true");
        dt.Rows.Add("ィ", "1", "0", "false", "true", "true");
        dt.Rows.Add("ゥ", "2", "0", "false", "true", "true");
        dt.Rows.Add("ェ", "3", "0", "false", "true", "true");
        dt.Rows.Add("ォ", "4", "0", "false", "true", "true");

        //か行
        dt.Rows.Add("か", "0", "1", "false", "false", "false");
        dt.Rows.Add("き", "1", "1", "false", "false", "false");
        dt.Rows.Add("く", "2", "1", "false", "false", "false");
        dt.Rows.Add("け", "3", "1", "false", "false", "false");
        dt.Rows.Add("こ", "4", "1", "false", "false", "false");

        dt.Rows.Add("が", "0", "1", "true", "false", "false");
        dt.Rows.Add("ぎ", "1", "1", "true", "false", "false");
        dt.Rows.Add("ぐ", "2", "1", "true", "false", "false");
        dt.Rows.Add("げ", "3", "1", "true", "false", "false");
        dt.Rows.Add("ご", "4", "1", "true", "false", "false");

        dt.Rows.Add("カ", "0", "1", "false", "false", "true");
        dt.Rows.Add("キ", "1", "1", "false", "false", "true");
        dt.Rows.Add("ク", "2", "1", "false", "false", "true");
        dt.Rows.Add("ケ", "3", "1", "false", "false", "true");
        dt.Rows.Add("コ", "4", "1", "false", "false", "true");

        dt.Rows.Add("ガ", "0", "1", "true", "false", "true");
        dt.Rows.Add("ギ", "1", "1", "true", "false", "true");
        dt.Rows.Add("グ", "2", "1", "true", "false", "true");
        dt.Rows.Add("ゲ", "3", "1", "true", "false", "true");
        dt.Rows.Add("ゴ", "4", "1", "true", "false", "true");

        //さ行
        dt.Rows.Add("さ", "0", "2", "false", "false", "false");
        dt.Rows.Add("し", "1", "2", "false", "false", "false");
        dt.Rows.Add("す", "2", "2", "false", "false", "false");
        dt.Rows.Add("せ", "3", "2", "false", "false", "false");
        dt.Rows.Add("そ", "4", "2", "false", "false", "false");

        dt.Rows.Add("ざ", "0", "2", "true", "false", "false");
        dt.Rows.Add("じ", "1", "2", "true", "false", "false");
        dt.Rows.Add("ず", "2", "2", "true", "false", "false");
        dt.Rows.Add("ぜ", "3", "2", "true", "false", "false");
        dt.Rows.Add("ぞ", "4", "2", "true", "false", "false");

        dt.Rows.Add("サ", "0", "2", "false", "false", "true");
        dt.Rows.Add("シ", "1", "2", "false", "false", "true");
        dt.Rows.Add("ス", "2", "2", "false", "false", "true");
        dt.Rows.Add("セ", "3", "2", "false", "false", "true");
        dt.Rows.Add("ソ", "4", "2", "false", "false", "true");

        dt.Rows.Add("ザ", "0", "2", "true", "false", "true");
        dt.Rows.Add("ジ", "1", "2", "true", "false", "true");
        dt.Rows.Add("ズ", "2", "2", "true", "false", "true");
        dt.Rows.Add("ゼ", "3", "2", "true", "false", "true");
        dt.Rows.Add("ゾ", "4", "2", "true", "false", "true");

        //た行
        dt.Rows.Add("た", "0", "3", "false", "false", "false");
        dt.Rows.Add("ち", "1", "3", "false", "false", "false");
        dt.Rows.Add("つ", "2", "3", "false", "false", "false");
        dt.Rows.Add("て", "3", "3", "false", "false", "false");
        dt.Rows.Add("と", "4", "3", "false", "false", "false");
                                
        dt.Rows.Add("だ", "0", "3", "true", "false", "false");
        dt.Rows.Add("ぢ", "1", "3", "true", "false", "false");
        dt.Rows.Add("づ", "2", "3", "true", "false", "false");
        dt.Rows.Add("で", "3", "3", "true", "false", "false");
        dt.Rows.Add("ど", "4", "3", "true", "false", "false");

        dt.Rows.Add("っ", "2", "3", "false", "true", "false");

        dt.Rows.Add("タ", "0", "3", "false", "false", "true");
        dt.Rows.Add("チ", "1", "3", "false", "false", "true");
        dt.Rows.Add("ツ", "2", "3", "false", "false", "true");
        dt.Rows.Add("テ", "3", "3", "false", "false", "true");
        dt.Rows.Add("ト", "4", "3", "false", "false", "true");
                                
        dt.Rows.Add("ダ", "0", "3", "true", "false", "true");
        dt.Rows.Add("ヂ", "1", "3", "true", "false", "true");
        dt.Rows.Add("ヅ", "2", "3", "true", "false", "true");
        dt.Rows.Add("デ", "3", "3", "true", "false", "true");
        dt.Rows.Add("ド", "4", "3", "true", "false", "true");

        dt.Rows.Add("ッ", "2", "3", "false", "true", "true");

        //な行
        dt.Rows.Add("な", "0", "4", "false", "false", "false");
        dt.Rows.Add("に", "1", "4", "false", "false", "false");
        dt.Rows.Add("ぬ", "2", "4", "false", "false", "false");
        dt.Rows.Add("ね", "3", "4", "false", "false", "false");
        dt.Rows.Add("の", "4", "4", "false", "false", "false");
                                
        dt.Rows.Add("ナ", "0", "4", "false", "false", "true");
        dt.Rows.Add("ニ", "1", "4", "false", "false", "true");
        dt.Rows.Add("ヌ", "2", "4", "false", "false", "true");
        dt.Rows.Add("ネ", "3", "4", "false", "false", "true");
        dt.Rows.Add("ノ", "4", "4", "false", "false", "true");

        //は行
        dt.Rows.Add("は", "0", "5", "false", "false", "false");
        dt.Rows.Add("ひ", "1", "5", "false", "false", "false");
        dt.Rows.Add("ふ", "2", "5", "false", "false", "false");
        dt.Rows.Add("へ", "3", "5", "false", "false", "false");
        dt.Rows.Add("ほ", "4", "5", "false", "false", "false");
                                
        dt.Rows.Add("ば", "0", "5", "true", "false", "false");
        dt.Rows.Add("び", "1", "5", "true", "false", "false");
        dt.Rows.Add("ぶ", "2", "5", "true", "false", "false");
        dt.Rows.Add("べ", "3", "5", "true", "false", "false");
        dt.Rows.Add("ぼ", "4", "5", "true", "false", "false");
                                
        dt.Rows.Add("ハ", "0", "5", "false", "false", "true");
        dt.Rows.Add("ヒ", "1", "5", "false", "false", "true");
        dt.Rows.Add("フ", "2", "5", "false", "false", "true");
        dt.Rows.Add("ヘ", "3", "5", "false", "false", "true");
        dt.Rows.Add("ホ", "4", "5", "false", "false", "true");
                                
        dt.Rows.Add("バ", "0", "5", "true", "false", "true");
        dt.Rows.Add("ビ", "1", "5", "true", "false", "true");
        dt.Rows.Add("ブ", "2", "5", "true", "false", "true");
        dt.Rows.Add("ベ", "3", "5", "true", "false", "true");
        dt.Rows.Add("ボ", "4", "5", "true", "false", "true");

        //ま行
        dt.Rows.Add("ま", "0", "6", "false", "false", "false");
        dt.Rows.Add("み", "1", "6", "false", "false", "false");
        dt.Rows.Add("む", "2", "6", "false", "false", "false");
        dt.Rows.Add("め", "3", "6", "false", "false", "false");
        dt.Rows.Add("も", "4", "6", "false", "false", "false");
                                
        dt.Rows.Add("マ", "0", "6", "false", "false", "true");
        dt.Rows.Add("ミ", "1", "6", "false", "false", "true");
        dt.Rows.Add("ム", "2", "6", "false", "false", "true");
        dt.Rows.Add("メ", "3", "6", "false", "false", "true");
        dt.Rows.Add("モ", "4", "6", "false", "false", "true");

        //や行
        dt.Rows.Add("や", "0", "7", "false", "false", "false");
        dt.Rows.Add("ゆ", "2", "7", "false", "false", "false");
        dt.Rows.Add("よ", "4", "7", "false", "false", "false");

        dt.Rows.Add("ゃ", "0", "7", "false", "true", "false");
        dt.Rows.Add("ゅ", "2", "7", "false", "true", "false");
        dt.Rows.Add("ょ", "4", "7", "false", "true", "false");

        dt.Rows.Add("ヤ", "0", "7", "false", "false", "true");
        dt.Rows.Add("ユ", "2", "7", "false", "false", "true");
        dt.Rows.Add("ヨ", "4", "7", "false", "false", "true");

        dt.Rows.Add("ャ", "0", "7", "false", "true", "true");
        dt.Rows.Add("ュ", "2", "7", "false", "true", "true");
        dt.Rows.Add("ョ", "4", "7", "false", "true", "true");

        //ら行
        dt.Rows.Add("ら", "0", "8", "false", "false", "false");
        dt.Rows.Add("り", "1", "8", "false", "false", "false");
        dt.Rows.Add("る", "2", "8", "false", "false", "false");
        dt.Rows.Add("れ", "3", "8", "false", "false", "false");
        dt.Rows.Add("ろ", "4", "8", "false", "false", "false");

        dt.Rows.Add("ラ", "0", "8", "false", "false", "true");
        dt.Rows.Add("リ", "1", "8", "false", "false", "true");
        dt.Rows.Add("ル", "2", "8", "false", "false", "true");
        dt.Rows.Add("レ", "3", "8", "false", "false", "true");
        dt.Rows.Add("ロ", "4", "8", "false", "false", "true");

        //わ行
        dt.Rows.Add("わ", "0", "9", "false", "false", "false");
        dt.Rows.Add("を", "4", "9", "false", "false", "false");
        dt.Rows.Add("ん", "5", "9", "false", "false", "false");

        dt.Rows.Add("ワ", "0", "9", "false", "false", "true");
        dt.Rows.Add("ヲ", "4", "9", "false", "false", "true");
        dt.Rows.Add("ン", "5", "9", "false", "false", "true");
    }
}
