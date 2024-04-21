using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectRequest : MonoBehaviour{
    void start(){
        startCoroutine(GetRequest("http://127.0.0.1:5000/"))
    }

    IEnumerator GetRequest(string uri){
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)){
            
            yield return webRequest.SendWebRequest();
            
            if(webRequest.isNetworkError){
                
                Debug.Log("Error: " + webRequest.Error);
            
            }else{
                Debug.Log(webRequest.downloadHandler.text);
            }
        }

    }
}