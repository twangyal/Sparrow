import pathlib
import textwrap
import sys

import google.generativeai as genai

from IPython.display import display
from IPython.display import Markdown

import PIL.Image

from flask import Flask
from flask_cors import CORS
from flask import request, jsonify

def returnString(result):
  return str(result)

@app.route('/objectRec', methods=['POST'])
def objectRec():
  file = sys.argv[1]

  img = PIL.Image.open(file)
  GOOGLE_API_KEY = 'AIzaSyCmEqw2hJ-mOK6U479aiGSRbsPp1xur_f0'

  genai.configure(api_key=GOOGLE_API_KEY)

  model = genai.GenerativeModel('gemini-pro-vision')

  response = model.generate_content(["Tell me what object is this. Keep it in a single word.", img], stream=True)
  response.resolve()
  print(response.text)
  returnString(response.text)

if __name__ == '__main__':
  app = Flask(__name__)
  CORS(app)
  app.run(debug=True)