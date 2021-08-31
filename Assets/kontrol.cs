using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class kontrol : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite []KusSprite;
    SpriteRenderer spriteRenderer;
    bool ileriGeriKontrol = true;
    int kusSayac = 0;
    float kusAnimasyonZaman = 0;

    Rigidbody2D fizik;

    int puan = 0;
    public Text puanText;
    bool oyunbitti = true;
    OyunKontrol oyunKontrol;
    AudioSource []sesler;
    int enYuksekPuan = 0;
   
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        oyunKontrol=GameObject.FindGameObjectWithTag("oyunkontrol").GetComponent<OyunKontrol>();    //bir objeyi tagi ile bulma
        sesler = GetComponents<AudioSource>();
        
        enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuankayıt");

        Debug.Log("en yüksek puan= "+enYuksekPuan);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && oyunbitti) //Mause tıklama
        {
            fizik.velocity=new Vector2(0,0); //hızı sıfır yaptık
            fizik.AddForce(new Vector2(0,200));//sonra AddForce kuvvet
            sesler[0].Play();
            
        }
        if(fizik.velocity.y>0) //velocity hızımız
        {
            transform.eulerAngles = new Vector3(0,0,45);
        }
        else
        {
            transform.eulerAngles = new Vector3(0,0,-45);
        }
        Animasyon();

     }
  void Animasyon()
  {
      kusAnimasyonZaman += Time.deltaTime; // 0.11 0.2 1 old. if girer
        if(kusAnimasyonZaman>0.2f)
        {
            kusAnimasyonZaman = 0;
             if(ileriGeriKontrol)
            {
                 spriteRenderer.sprite = KusSprite[kusSayac];
                 kusSayac++; // 0 1 2 3
                if (kusSayac==KusSprite.Length)
                {
                    kusSayac--;
                     ileriGeriKontrol = false;
                }
             }
        else
        {
            kusSayac--;
            spriteRenderer.sprite = KusSprite[kusSayac];
            if(kusSayac == 0)
            {
                kusSayac++;
                ileriGeriKontrol = true;
             }
         }
     }
  }

  void OnTriggerEnter2D(Collider2D col)
   {
       if(col.gameObject.tag == "puan")
       {
           puan++;
           puanText.text="puan = "+ puan;
           sesler[1].Play();
           Debug.Log(puan);
       }
       if(col.gameObject.tag=="engel")
       {
           oyunbitti = false;
           sesler[2].Play();
           oyunKontrol.oyunbitti();
           //GetComponents<CircleCollider2D>().enabled = false;
           if (puan > enYuksekPuan)
           {
               enYuksekPuan = puan;
               PlayerPrefs.SetInt("enYuksekPuankayıt",enYuksekPuan);
           }
           Invoke("anaMenuyeDon",2); //2sn sonra anamenu çağırılması(oyundayandığında)
       }
  }  
  void anaMenuyeDon()
  {
      PlayerPrefs.SetInt("puanKayit",puan);
      SceneManager.LoadScene("anaMenu");

  }
} 
