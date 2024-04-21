import pathlib
import textwrap
import sys

import google.generativeai as genai

from IPython.display import display
from IPython.display import Markdown

import PIL.Image

def returnString(result):
  return str(result)

if __name__ == "__main__":
  file = sys.argv[1]

  img = PIL.Image.open(file)

  def to_markdown(text):
    text = text.replace('•', '  *')
    return Markdown(textwrap.indent(text, '> ', predicate=lambda _: True))


  GOOGLE_API_KEY = 'AIzaSyCmEqw2hJ-mOK6U479aiGSRbsPp1xur_f0'

  genai.configure(api_key=GOOGLE_API_KEY)

  model = genai.GenerativeModel('gemini-pro-vision')

  response = model.generate_content(["Tell me what object is this. Keep it in a single word.", img], stream=True)
  response.resolve()
  print(response.text)
  returnString(response.text)