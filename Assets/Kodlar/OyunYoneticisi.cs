using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunYoneticisi : MonoBehaviour {

    public GameObject gokYuzu1;
    public GameObject gokYuzu2;
    Rigidbody2D gY1Fizik;
    Rigidbody2D gY2Fizik;

    public GameObject engel;
    GameObject[] engeller;
    int engelSayisi = 5;
    byte sayac=0;

    float zaman = 0;
    public float hiz = 1.5f;
    float uzunluk;
    bool oyunBitti = false;

	void Start ()
    {
        gY1Fizik = gokYuzu1.GetComponent<Rigidbody2D>();
        gY2Fizik = gokYuzu2.GetComponent<Rigidbody2D>();
        uzunluk = gokYuzu1.GetComponent<BoxCollider2D>().size.x;
        gY1Fizik.velocity = new Vector2(-hiz, 0);
        gY2Fizik.velocity = new Vector2(-hiz, 0);
        engeller = new GameObject[engelSayisi];
        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i] = Instantiate(engel, new Vector2(-20, -20),Quaternion.identity);
            engeller[i].GetComponent<Rigidbody2D>().velocity = new Vector2(-hiz, 0);
        }

	}
	
	
	void Update ()
    {
        
            if (gokYuzu1.transform.position.x <= -uzunluk)
            {
                gokYuzu1.transform.position = new Vector3(gokYuzu2.transform.position.x + uzunluk, 0);
            }
            if (gokYuzu2.transform.position.x <= -uzunluk)
            {
                gokYuzu2.transform.position = new Vector3(gokYuzu1.transform.position.x + uzunluk, 0);
            }
            //----------------------------------//
            zaman += Time.deltaTime;
            if (zaman >= 2.1f && !oyunBitti)
            {
                zaman = 0;
                engeller[sayac].transform.position = new Vector2(5, Random.Range(-2, 3));
                sayac++;
                if (sayac == engeller.Length)
                    sayac = 0;
            }	
    }

    public void OyunBitti()
    {
        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        gY1Fizik.velocity = Vector2.zero;
        gY2Fizik.velocity = Vector2.zero;
        oyunBitti = true;
    }
}
