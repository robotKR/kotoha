using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class InputManager : MonoBehaviour
{
    //InputField���i�[���邽�߂̕ϐ�
    InputField inputField;
    public /*new*/ string message;

    // Start is called before the first frame update
    void Start()
    {
        //InputField�R���|�[�l���g���擾
        inputField = GameObject.Find("InputField (Legacy)").GetComponent<InputField>();
    }


    //���͂��ꂽ���O����ǂݎ���ăR���\�[���ɏo�͂���֐�
    public void GetInputName()
    {
        //InputField����e�L�X�g�����擾����
        message = inputField.text;
        //Debug.Log(message);

        //���̓t�H�[���̃e�L�X�g����ɂ���
        inputField.text = "";
    }
}