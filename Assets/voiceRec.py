import speech_recognition as sr
from flask import Flask, jsonify
app = Flask(__name__)

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
    return jsonify(text)
  except sr.UnknownValueError:
    print("Could not understand audio")
  except sr.RequestError as e:
    print("Error; {0}".format(e))

if __name__ == "__main__":
  app.run()
