using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RedCoin : MonoBehaviour
{
    public AudioClip coinSound;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            other.GetComponent<PlayerScript>().points--;

            Destroy(gameObject);

            AudioSource.PlayClipAtPoint(coinSound, transform.position);
        }
    }
}