using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class QRRedeem : MonoBehaviour
{
    [SerializeField] private RawImage qrImage;
    [SerializeField] private Text qrText;

    public class Result
    {
        public string result;
        public string imageName;
    }

    private void Start()
    {
        if(PlayerPrefs.GetString("IMG_URL") == "")
        {
            StartCoroutine(GetQRImage("https://jfaisolution.com/api/image-download"));
        }
        else
        {
            StartCoroutine(ShowQRImage("https://jfaisolution.com/public/Image/" + PlayerPrefs.GetString("IMG_URL")));
            qrText.text = "You have already redeemed QR code.";
        }
        
    }

    IEnumerator GetQRImage(string url)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            Result loadData = JsonUtility.FromJson<Result>(uwr.downloadHandler.text);
            StartCoroutine(ShowQRImage("https://jfaisolution.com/public/Image/" + loadData.imageName));
            PlayerPrefs.SetString("IMG_URL", loadData.imageName);
        }
    }

    IEnumerator ShowQRImage(string qrPath)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(qrPath);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            qrImage.gameObject.SetActive(true);
            qrImage.texture = myTexture;
        }
    }
}
