import os
import sys
import openai
import chardet
import re

os.environ["OPENAI_API_KEY"] = ""

Emosion = ""
Animesion = "1"
Animesion_1 = "1"
Human_Message = ""

def ChatGPT():
    global Human_Message

    AI_Message = ""

    print("ChatGPT waiting...")
    Human_Message_escaped = Human_Message.encode('unicode_escape').decode('utf-8')
    Human_Message_bytes = Human_Message_escaped.encode('utf-8')
    Human_Message = Human_Message_bytes.decode('utf-8')
    print(type(Human_Message_bytes))
    print(chardet.detect(Human_Message_bytes))
    print(Human_Message)

    response = openai.ChatCompletion.create(
        model="gpt-3.5-turbo",
        messages=[
            {"role": "system", "content": "You are a pretty high school girl. Please speak Japanese."},
            {"role": "user", "content": "かわいいね。"},
            {"role": "assistant", "content": "えへへ、ありがとうございます。"},
            {"role": "user", "content" : Human_Message},
            ]
    )

    AI_Message = "AI_Message:" + response['choices'][0]['message']['content']
    if contains_japanese(AI_Message):
        AI_Message = AI_Message.encode('utf-8')
    print(AI_Message)

def contains_japanese(text):
    return bool(re.search('[ぁ-んァ-ン一-龥]', text))

def get_animation_name():
    if Emosion == "surprise":
        return "1"
    elif Emosion == "sadness":
        return "2"
    else:
        return "0"

def Animation():
    global Animesion_1  # グローバル変数として定義
    Animesion = get_animation_name()

    if Animesion != Animesion_1:
        Animesion_1 = Animesion
        print("Animation:" + Animesion)

def read_input():
    for line in sys.stdin:
        if line.startswith("Human_Message:"):
            Human_Message = line.strip() # 改行コードを削除してデータを取得
            Human_Message = Human_Message.replace("Human_Message:","")
            ChatGPT()

def main():
    while True:
        Animation()
        read_input()

main()
