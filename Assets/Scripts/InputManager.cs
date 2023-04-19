using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class InputManager : MonoBehaviour
{
    //InputFieldを格納するための変数
    InputField inputField;
    public /*new*/ string message;

    // Start is called before the first frame update
    void Start()
    {
        //InputFieldコンポーネントを取得
        inputField = GameObject.Find("InputField (Legacy)").GetComponent<InputField>();
    }


    //入力された名前情報を読み取ってコンソールに出力する関数
    public void GetInputName()
    {
        //InputFieldからテキスト情報を取得する
        message = inputField.text;
        //Debug.Log(message);

        //入力フォームのテキストを空にする
        inputField.text = "";
    }
}