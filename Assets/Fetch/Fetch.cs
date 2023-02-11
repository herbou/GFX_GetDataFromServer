using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System.Collections;

public static class Fetch {
    private class MyMono : MonoBehaviour { }

    private static MyMono mono;

    private static void Init() {
        if (mono == null) {
            GameObject g = new("#Fetcher");
            mono = g.AddComponent<MyMono>();
        }
    }

    // Tex
    // t data -------------------------------------------------------------------------------
    public static void Get(string url, UnityAction<UnityWebRequest.Result,string> onComplete) {
        Init();
        mono.StartCoroutine(_GetCo(url, onComplete));
    }
        
    private static IEnumerator _GetCo(string url, UnityAction<UnityWebRequest.Result,string> onComplete) {
        UnityWebRequest request = UnityWebRequest.Get(url.Trim());

        yield return request.SendWebRequest();

        onComplete?.Invoke(request.result, request.downloadHandler.text);
               
        request.Dispose();
        request.downloadHandler.Dispose();
    }



    // Texture data -------------------------------------------------------------------------------
    public static void GetTexture(string url, UnityAction<UnityWebRequest.Result,Texture2D> onComplete) {
        Init();
        mono.StartCoroutine(_GetTextureCo(url, onComplete));
    }
        
    private static IEnumerator _GetTextureCo(string url, UnityAction<UnityWebRequest.Result,Texture2D> onComplete) {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url.Trim());
        Texture2D texture = null;

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            texture = DownloadHandlerTexture.GetContent(request);

        onComplete?.Invoke(request.result, texture);

        request.Dispose();
        request.downloadHandler.Dispose();
    }

}
