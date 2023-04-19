using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // 追加しましょう

public class TextScript : MonoBehaviour
{

    public Change change;
    public GameObject score_object = null; // Textオブジェクト

    // 初期化
    void Start()
    {
    }

    // 更新
    void Update()
    {   
        // オブジェクトからTextコンポーネントを取得
        Text score_text = score_object.GetComponent<Text>();
        string text = "";
        string text_1 = "";

        text = change.AI_Message;

        if(text != text_1)
        {
            text_1 = text;
            // テキストの表示を入れ替える
            score_text.text = text;
        }
    }
}