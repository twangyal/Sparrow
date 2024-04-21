from flask import Flask, jsonify
import google.generativeai as genai

from IPython.display import display
from IPython.display import Markdown

import PIL.Image
app = Flask(__name__)

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

if __name__ == "__main__":
  app.run()
