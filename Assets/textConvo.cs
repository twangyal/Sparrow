using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class textConvo : MonoBehaviour
{
    [SerializeField]
    private Text _textComponent;
    private string _textToDraw = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam dictum eros ultricies tristique tincidunt. Nulla tristique rhoncus elit vel fringilla. Nunc egestas bibendum risus eget accumsan. Integer mauris";

    void Start()
    {
        _textComponent = gameObject.GetComponentInChildren<Text>();
        _textComponent.text = string.Empty;
        StartRollingText(_textToDraw,0.1f);
    }

    public void StartRollingText(string text, float time)
    {
        StartCoroutine(DrawText(text,time));
    }

    IEnumerator DrawText(string text,float time) //Keeps adding more characters to the Text component text
    {
        string[] words = text.Split(' ');
	for (int i = 0; i < words.Length; i++)
	{
		string word = words[i];
  	// add a space for words except for the last
        if (i != words.Length - 1) word += " ";	
        string previousText = _textComponent.text;
            // determine if next word needs to be on new line*
            float lastHeight = _textComponent.preferredHeight;
            _textComponent.text += word;
            if (_textComponent.preferredHeight > lastHeight)
            {
                previousText += System.Environment.NewLine;

            for (int j = 0; j < word.Length; j++)
            {
                _textComponent.text = previousText + word.Substring(0, j+1);
                yield return new WaitForSeconds(time);
            }
        }
        }
    }
}