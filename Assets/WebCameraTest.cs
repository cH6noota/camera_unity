using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;


public class WebCameraTest : MonoBehaviour
{
    public RawImage rawImage;
    //public RenderTexture RenderTextureRef;
    
     WebCamTexture webCamTexture;
     bool flag=true;
    
    void Start () {
    
       webCamTexture = new WebCamTexture();
        rawImage.texture = webCamTexture;
        webCamTexture.Play();
    }
    
    void Update (){
    }
    public void TakePhoto()
    {
        StartCoroutine ( func1() );
       // webCamTexture = new WebCamTexture();
       // webCamTexture.Play();
        
        //Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height , TextureFormat.RGB24, false);
        
    //    photo.SetPixels(webCamTexture.GetPixels());
    //    photo.Apply();
        
   //     byte[] bytes = photo.EncodeToPNG();
        //string text = System.Text.Encoding.ASCII.GetString(bytes);
        //Debug.Log(text);
      //  File.WriteAllBytes("./Assets/photo2.png", bytes);
   //     webCamTexture.Stop();
        // it's a rare case where the Unity doco is pretty clear,
        
        // be sure to scroll down to the SECOND long example on that doco page
        
       

       
       
        //StartCoroutine ( func1() );
    }
    // コルーチン
    private IEnumerator func1(){
        webCamTexture = new WebCamTexture();
        webCamTexture.Play();
        var width=webCamTexture.width;
        var height=webCamTexture.height;
        var photo = new Texture2D(width, height ,TextureFormat.ARGB32, false);
        yield return new WaitForEndOfFrame();
        
        //Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height ,TextureFormat.ARGB32, false);
        photo.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        //photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();
        Debug.Log(webCamTexture.width);
        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        webCamTexture.Stop();
       File.WriteAllBytes("./Assets/sabbba.png", bytes);
       
        
    }
    
}
