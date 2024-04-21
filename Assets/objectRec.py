import sys

import google.generativeai as genai

from IPython.display import display
from IPython.display import Markdown

import PIL.Image

if __name__ == "__main__":
  file = sys.argv[1]

  img = PIL.Image.open(file)

  GOOGLE_API_KEY = 'AIzaSyCmEqw2hJ-mOK6U479aiGSRbsPp1xur_f0'

  genai.configure(api_key=GOOGLE_API_KEY)

  model = genai.GenerativeModel('gemini-pro-vision')

  response = model.generate_content(["Tell me what object is this. Keep it in a single word.", img], stream=True)
  response.resolve()
  print(response.text)
