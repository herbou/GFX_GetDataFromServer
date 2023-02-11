using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public struct Data {
    public string Title;
    public string Icon;
}

public class Demo : MonoBehaviour {
    [TextArea(2, 5)] public string jsonURL;

    //ui
    public TextMeshProUGUI uiTitle;
    public RawImage uiIcon;


    private void Start() {
        if (!ServerData.IsLoaded) {
            Fetch.Get(jsonURL, (result, text) => {
                if (result == UnityWebRequest.Result.Success) {
                    Data data = JsonUtility.FromJson<Data>(text);
                    uiTitle.text = data.Title;

                    Fetch.GetTexture(data.Icon, (result, texture) => {
                        uiIcon.texture = texture;

                        //Save data:
                        ServerData.Title = data.Title;
                        ServerData.Icon = texture;
                        ServerData.IsLoaded = true;
                    });
                }
            });
        }else {
            uiTitle.text = ServerData.Title;
            uiIcon.texture = ServerData.Icon;
        }
    }
}
