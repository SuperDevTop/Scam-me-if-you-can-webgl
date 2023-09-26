using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class QRRedeem : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetURLFromPage();

    public string ReadURL()
    {
        return GetURLFromPage();
    }

    [SerializeField] private RawImage qrImage;
    [SerializeField] private Text qrText;
    [SerializeField] private GameObject waitingUI;
    private string frontURL;

    public class Result
    {
        public string result;
        public string imageName;
    }

    void Start()
    {
        //if (PlayerPrefs.GetString("IMG_URL") == "")
        //{
        //    StartCoroutine(GetQRImage("https://jfaisolution.com/api/image-download"));
        //}
        //else
        //{
        //    StartCoroutine(ShowQRImage("https://jfaisolution.com/public/Image/" + PlayerPrefs.GetString("IMG_URL")));
        //    qrText.text = "You have already redeemed QR code.";
        //}
      
        frontURL = ReadURL().Split('/')[0] + "//" + ReadURL().Split('/')[2];
        waitingUI.SetActive(true);
        StartCoroutine(GetQRImage(frontURL + "/api/image-download"));
    }

    void Update() 
    { 

    }

    IEnumerator GetQRImage(string url)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            //Debug.Log("Received: " + uwr.downloadHandler.text);
            StartCoroutine(GetQRImage(frontURL + "/api/image-download"));
        }
        else
        {
            //Debug.Log("Received: " + uwr.downloadHandler.text);
            Result loadData = JsonUtility.FromJson<Result>(uwr.downloadHandler.text);
            string imageUrl = loadData.imageName.ToString();
            //PlayerPrefs.SetString("IMG_URL", loadData.imageName);
            //StartCoroutine(ShowQRImage("https://jfaisolution.com/public/Image/" + PlayerPrefs.GetString("IMG_URL")));
            StartCoroutine(ShowQRImage(frontURL + "/public/Image/" + imageUrl));
        }
    }

    IEnumerator ShowQRImage(string qrPath)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(qrPath);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Debug.Log("Received: " + www.downloadHandler.text);
            StartCoroutine(ShowQRImage(qrPath));
        }
        else
        {
            Debug.Log("Received: " + www.downloadHandler.text);
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            qrImage.gameObject.SetActive(true);
            qrImage.texture = myTexture;
            waitingUI.SetActive(false);
        }
    }
}
