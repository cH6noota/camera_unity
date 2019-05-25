using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;
using UnityEngine.Networking;
using System;

public class CameraTest : MonoBehaviour
{
    public RawImage rawimage;  //Image for rendering what the camera sees.
    WebCamTexture webcamTexture = null;
    
    void Start()
    {
        //Save get the camera devices, in case you have more than 1 camera.
        WebCamDevice[] camDevices = WebCamTexture.devices;
        
        //Get the used camera name for the WebCamTexture initialization.
        string camName = camDevices[0].name;
        webcamTexture = new WebCamTexture(camName);
        
        //Render the image in the screen.
        rawimage.texture = webcamTexture;
        rawimage.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
    
    void Update()
    {
        //This is to take the picture, save it and stop capturing the camera image.
        //if(Input.GetMouseButtonDown(0))
        //{
         //   SaveImage();
            //webcamTexture.Stop();
        //}
    }

    public void TakePhoto()
    {
        SaveImage();
        webcamTexture.Stop();
        Destroy (rawimage);
    }
    
    
    void SaveImage()
    {
        //Create a Texture2D with the size of the rendered image on the screen.
        Texture2D texture = new Texture2D(rawimage.texture.width, rawimage.texture.height, TextureFormat.ARGB32, false);
        
        //Save the image to the Texture2D
        texture.SetPixels(webcamTexture.GetPixels());
        texture.Apply();
        
        //Encode it as a PNG.
        byte[] bytes = texture.EncodeToPNG();
        StartCoroutine (UploadFile ( bytes ));
        
        //Save it in a file.
        File.WriteAllBytes(Application.dataPath + "/images/testimg.png", bytes);
    }
    
    IEnumerator UploadFile(byte[] img) {
       //今の時間を取得
       DateTime dt = DateTime.Now;
       string filename = dt.ToString("HH_mm_ss")+".png" ;
        // formにバイナリデータを追加
        WWWForm form = new WWWForm ();
        form.AddBinaryData ("file", img,filename , "image/png");
        // HTTPリクエストを送る
        UnityWebRequest request = UnityWebRequest.Post ("http://ik1-334-27288.vs.sakura.ne.jp/hack06/upload.php", form);
        yield return request.Send ();
        
        if (request.isError) {
            // POSTに失敗した場合，エラーログを出力
            Debug.Log ("error!!");
            Debug.Log (request.error);
        } else {
            // POSTに成功した場合，レスポンスコードを出力
            Debug.Log ("成功っす");
            Debug.Log (request.responseCode);
        }
    }
    
    
}
