using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectRequest : MonoBehaviour{
    void Start(){
        StartCoroutine(GetRequest("http://127.0.0.1:5000/"));
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