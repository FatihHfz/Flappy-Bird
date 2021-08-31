using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anaMenu : MonoBehaviour
{
    
    public Text puanText;
    public Text puan;
    void Start()
    {    
        int enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuankayıt");
        int puanGelen = PlayerPrefs.GetInt("puanKayit");
        puanText.text = "en Yüksek Puan =   " + enYuksekPuan;
        puan.text = "puan = "+ puanGelen;
    }

    // Update is called once per frame
    void Update()
    {      
    }

    public void oyunaGit()
    {
        SceneManager.LoadScene("Level1");
    }
    public void oyundanCik()
    {
        Application.Quit();
    }
}
