using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int _maxHealth = 5;
    private int _currentHealth = 5;
    public GameObject deadScene;
    private bool hasDied;

    public PlayerController thePlayer;
    private float _invincibilityLength =1;
    private float _invincibilityCounter;

    public Renderer playerRenderer;
    private float _flashCounter;
    private float _flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;

    private float _respawnLength = 2f;

    void Start()
    {
        
       // thePlayer = FindObjectOfType<PlayerController>();

        respawnPoint = thePlayer.transform.position;
    }

   
    void Update()
    {
       if(_invincibilityCounter > 0)
        {
            _invincibilityCounter -= Time.deltaTime;
            _flashCounter -= Time.deltaTime;
            if(_flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                _flashCounter = _flashLength;
            }
            if(_invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        } 
    }
    public void HurtPlayer(int damage, Vector3 direction)
    {
        if(_invincibilityCounter <= 0)
        {

       
        _currentHealth -= damage;
            if(_currentHealth <= _maxHealth && _currentHealth >0) 
            {
                Respawn();
               // deadScene.SetActive(true);
               // hasDied = true;
            }
            else if(_currentHealth <= 0)
            {
                deadScene.SetActive(true);
                hasDied = true;
            }
           
            else
            {
                thePlayer.KnockBack(direction);
                _invincibilityCounter = _invincibilityLength;
                playerRenderer.enabled = false;
                _flashCounter = _flashLength;
            }
        }
       
    }
    public void Respawn()
    {
        if (!isRespawning)
        {
            StartCoroutine("RespawnCoroutine");
        }
    }

    public IEnumerator RespawnCoroutine()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);

        yield return new WaitForSeconds(_respawnLength);
        isRespawning = false;

        thePlayer.gameObject.SetActive(true);
        thePlayer.transform.position = respawnPoint;
        _currentHealth --;

        _invincibilityCounter = _invincibilityLength;
        playerRenderer.enabled = false;
        _flashCounter = _flashLength;
    }
    

    /*public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }*/
    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
}
