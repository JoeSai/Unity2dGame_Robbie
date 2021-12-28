using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;

    [SerializeField] private GameObject explosionVFXPre;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((1 << other.gameObject.layer & _playerLayer) != 0)
        {
            Instantiate(explosionVFXPre, transform.position, transform.rotation);
            gameObject.SetActive(false);
            
            AudioManager.PlayOrbAudio();
        }
    }
}
