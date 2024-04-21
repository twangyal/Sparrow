from flask import Flask, jsonify
import google.generativeai as genai

from IPython.display import display
from IPython.display import Markdown

import speech_recognition as sr
from flask import Flask, jsonify
import os;

import PIL.Image
app = Flask(__name__)

GOOGLE_API_KEY = 'AIzaSyCmEqw2hJ-mOK6U479aiGSRbsPp1xur_f0'

genai.configure(api_key=GOOGLE_API_KEY)

model = genai.GenerativeModel(model_name="models/gemini-1.5-pro-latest",
system_instruction= "You're a store clerk. Help someone learn a language they ask for based on the responses they give. Communicate with them in that language. They'll be completing a shopping trip at your store, speak to them and try to help them learn")
chat = model.start_chat(history=[])

@app.route("/<adress>")
def get_prompt(adress):
  file = adress

  img = PIL.Image.open(file)

  GOOGLE_API_KEY = 'AIzaSyCmEqw2hJ-mOK6U479aiGSRbsPp1xur_f0'

  genai.configure(api_key=GOOGLE_API_KEY)

  model = genai.GenerativeModel('gemini-pro-vision')

  response = model.generate_content(["Tell me what object is this. Keep it in a single word.", img], stream=True)
  response.resolve()
  return jsonify(response.text)

@app.route("/audio/<filename>")
def transcribe_audio(filename):
  # Initialize recognizer
  r = sr.Recognizer()
  
  # Load audio file
  with sr.AudioFile(filename) as source:
    audio = r.record(source)
  
  # Transcribe audio
  try:
    text = r.recognize_google(audio)
    append_to_file(text, "conversation.txt")
    file_path = "/Users/tseringwangyal/Desktop/Projects/Personal/Unity/Sparrow/Assets/Scripts/ExternalScripts/ThirdParty/recordedAudio.wav"
    
    os.remove(file_path)
    
    keep_convo("conversation.txt")

    return jsonify(text)
  except sr.UnknownValueError:
    print("Could not understand audio")
  except sr.RequestError as e:
    print("Error; {0}".format(e))

@app.route("/lang/<language>")
def start_convo(language):
    msg = "Speak to me in " + language + " and I will try to help you learn it."
    response = chat.send_message(msg)
    append_to_file(msg, "conversation.txt")
    return jsonify(response.text)


def keep_convo(filename):
    with open("./"+filename, "r") as file:
        lines = file.readlines()
        for line in lines:
            response = chat.send_message(line)
            append_to_file(response.text, "conversation.txt")
    return jsonify(response.text)

def append_to_file(string, filename):
    with open("./"+filename, "a") as file:
        file.write(string + "\n")


if __name__ == "__main__":
  app.run()
