using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Diagnostics;
using System.Text;

public class Change : MonoBehaviour
{   
    Process pr = null;
    public InputManager inputmanager;
    string input_1 = "";
    int Animation;
    public string AI_Message;
    bool Animation_value = false;
    [SerializeField] Animator mAnimator;

    // Start is called before the first frame update
    void Start()
    {
        HeavyMethod();
    }

    void Update()
    {
        input();
        EndGame();
        Animation_1();
    }

    /// <summary>
    /// python�R�[�h��񓯊��Ŏ��s
    /// <summary>
    public void HeavyMethod()
    {
        pr = new Process();

        // python�t�@�C���̎w��
        pr.StartInfo.FileName = @"D:\Users\kaika\miniconda3\python.exe";
        // ���s������python�̃R�[�h���w��
        pr.StartInfo.Arguments = @" -u D:\Unity_project\kotoha\Assets\Scripts\Animation_Change.py";

        // �R���\�[����ʂ�\�������Ȃ�
        pr.StartInfo.CreateNoWindow = true;

        // �񓯊����s�ɕK�v
        pr.StartInfo.UseShellExecute = false;
        pr.StartInfo.RedirectStandardOutput = true;
        pr.StartInfo.RedirectStandardInput = true;
        pr.StartInfo.RedirectStandardError = true;

        // �C�x���g�n���h���o�^�i�W���o�͎��j
        pr.OutputDataReceived += process_DataReceived;

        pr.ErrorDataReceived += Process_ErrorDataReceived;

        // �C�x���g�n���h���o�^�i�v���Z�X�I�����j
        pr.EnableRaisingEvents = true;

        pr.Start();
        pr.BeginOutputReadLine(); //�񓯊��ŕW���o�͓ǂݎ��
        pr.BeginErrorReadLine();
    }

    /// <summary>
    /// �W���o�͂����������Ɏ��s
    /// </summary>
    void process_DataReceived(object sender, DataReceivedEventArgs e)
    {   
        
        string output = e.Data;
        print(output);

        if (output.StartsWith("Animation:"))
        {
            output = output.Remove(0, 10);
            Animation = int.Parse(output);
            print(Animation);
            Animation_value = true;
        }

        if (output.StartsWith("b'AI_Message:"))
        {

            AI_Message = /*@"\xe3\x81\x93\xe3\x82\x93\xe3\x81\xab\xe3\x81\xa1\xe3\x81\xaf\xef\xbc\x81";*/output.Replace("b'AI_Message:", "").Replace("'", "");

            print(AI_Message);

            // \xをエスケープ文字から実際の文字に変換する
            AI_Message = AI_Message.Replace(@"\x", "");

            print(AI_Message);

            // 16進数文字列をバイト配列に変換する
            byte[] bytes = new byte[AI_Message.Length / 2];

            for (int i = 0; i < AI_Message.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(AI_Message.Substring(i, 2), 16);
            }

            // バイト配列を使用して文字列をデコードする
            AI_Message = Encoding.UTF8.GetString(bytes);

            print(AI_Message);

        }

        if (output.StartsWith("AI_Message:"))
        {
            AI_Message = output.Replace("AI_Message:", "");
            print(AI_Message);
        }
    }

    public void Animation_1()
    {   
        if(Animation_value == true)
        {
            mAnimator.SetInteger("state", Animation); // state�Ƃ���Int�l�p�����[�^�[��1
        }
    }

    public void input()
    {
        string input = inputmanager.message;

        if (input != input_1)
        {
            input_1 = input;
            input = "Human_Message:" + input;
            /*byte[] bytes = Encoding.UTF8.GetBytes(input);
            pr.StandardInput.WriteLine(bytes);*/
            pr.StandardInput.WriteLine(input);
            pr.StandardInput.Flush();
        }
    }

    void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (e.Data != null)
        {
            UnityEngine.Debug.LogError($"Pythonプログラムからエラーが出力されました：{e.Data}");
        }
    }

    public void EndGame()
    {
        //Esc�������ꂽ��
        if (Input.GetKey(KeyCode.Escape))
        {
            pr.Kill();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
            Application.Quit();//�Q�[���v���C�I��
#endif
        }
    }
}