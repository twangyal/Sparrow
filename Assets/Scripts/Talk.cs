using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using System.Collections;

public class Talk : MonoBehaviour
{
    private bool isRecording = false;
    private string microphoneDevice;
    private AudioClip recordedClip;

    void Start()
    {
        // Get the default microphone device
        microphoneDevice = Microphone.devices[0];
    }

    public void Recording()
    {
            if (!isRecording)
            {
                // Start recording from the microphone
                recordedClip = Microphone.Start(microphoneDevice, false, 10, AudioSettings.outputSampleRate);
                isRecording = true;
            }
            else
            {
                // Stop recording and save the audio as a WAV file
                Microphone.End(microphoneDevice);
                isRecording = false;

                // Convert the AudioClip to a WAV byte array
                
                // Save the WAV byte array to a file
                string filePath = "/Users/tseringwangyal/Desktop/Projects/Personal/Unity/Sparrow/Assets/Scripts/ExternalScripts/ThirdParty/recordedAudio.wav";
                SavWav.Save(filePath, recordedClip);

                Debug.Log("Recording saved as: " + filePath);
                StartCoroutine(GetRequest("http://127.0.0.1:5000/audio/recordedAudio.wav"));
            
        }
    }
    IEnumerator GetRequest(string uri){
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)){
            
            yield return webRequest.SendWebRequest();
            
            if(webRequest.result == UnityWebRequest.Result.ConnectionError){
                
                Debug.Log("Error: " + webRequest.error);
            
            }else{
            
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }
    
}