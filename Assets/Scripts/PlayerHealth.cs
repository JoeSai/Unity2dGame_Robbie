using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject deathVFXPregab;
    [SerializeField] private LayerMask _layers = default;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if((1 << other.gameObject.layer & _layers) != 0)
        {
            Instantiate(deathVFXPregab, transform.position, transform.rotation);
            gameObject.SetActive(false);
            AudioManager.PlayDeathAudio();

            GameManager.PlayerDied();
        }
    }
}
