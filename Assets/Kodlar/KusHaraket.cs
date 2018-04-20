using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KusHaraket : MonoBehaviour {

    public Sprite[] kusSprite;
    SpriteRenderer spriteRengerer;
    Rigidbody2D fizik;
    GameObject oyunYoneticisi;
    bool ileriKontrol=true;
    byte kusSayac=1;
    float zaman=0;
    bool oyunBitti = false;
    int puan = 0;
    int yuksekPuan = 0;
    public Text puanText;
    public Text yuksekPuanText;
    AudioSource []sesler;
    bool oyunBasladi = false;
    int reklamSayac = 0;

	void Start ()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        oyunYoneticisi = GameObject.FindGameObjectWithTag("OyunYoneticisiTag");
        oyunYoneticisi.GetComponent<OyunYoneticisi>().enabled = false;

        yuksekPuan = PlayerPrefs.GetInt("yuksekPuan");
        yuksekPuanText.text = "High Score\n" + yuksekPuan;

        spriteRengerer = GetComponent<SpriteRenderer>();
        spriteRengerer.sprite = kusSprite[0];        
        fizik = GetComponent<Rigidbody2D>();
            
        sesler = GetComponents<AudioSource>();
        reklamSayac = PlayerPrefs.GetInt("reklamSayac");
        reklamSayac++;
        PlayerPrefs.SetInt("reklamSayac", reklamSayac);
        if (reklamSayac % 5==0)
        {        
            reklamSayac = 0;
            PlayerPrefs.SetInt("reklamSayac", reklamSayac);
            GameObject.FindGameObjectWithTag("ReklamTag").GetComponent<Reklam>().ReklamGoster();
        }



    }
	
	void Update ()
    {
        if(Input.GetButtonDown("Fire1") && !oyunBitti)
        {
            if(!oyunBasladi)
            {
                GetComponent<Rigidbody2D>().gravityScale = 1;
                oyunYoneticisi.GetComponent<OyunYoneticisi>().enabled = true;
                oyunBasladi = true;
                yuksekPuanText.text = "";
            }
            sesler[2].Play();
            fizik.velocity = new Vector2(0,0);
            fizik.AddForce(new Vector2(0, 220));
        }
        if(oyunBasladi)
        {
            if (fizik.velocity.y > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 45);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, -45);
            }
            Animasyon();
        }

    }

    void Animasyon()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.1f)
        {
            if (ileriKontrol)
            {
                spriteRengerer.sprite = kusSprite[kusSayac];
                kusSayac++;
                if (kusSayac == 3)
                {
                    kusSayac--;
                    ileriKontrol = false;
                }
            }
            else
            {
                kusSayac--;
                spriteRengerer.sprite = kusSprite[kusSayac];
                if (kusSayac == -0)
                {
                    kusSayac = 1;
                    ileriKontrol = true;
                }
            }
            zaman = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.gameObject.tag=="PuanTag" && !oyunBitti)
        {
            puan++;
            sesler[0].Play();
            puanText.text = puan.ToString();

        }
        if(col.gameObject.tag=="EngelTag" && !oyunBitti)
        {
            oyunBitti = true;
            yuksekPuanText.text = "High Score\n" + yuksekPuan;
            if (puan>yuksekPuan)
            {
                yuksekPuan = puan;
                PlayerPrefs.SetInt("yuksekPuan", yuksekPuan);
                yuksekPuanText.text = "New\nHigh Score\n" + yuksekPuan;
            }
            
            sesler[1].Play();
            oyunYoneticisi.GetComponent<OyunYoneticisi>().OyunBitti();
            
            Invoke("Yenile", 2);
        }
    }

    void Yenile()
    {       
        SceneManager.LoadScene("oyun");
    }

}
