using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(50 * Time.deltaTime, 0.0f, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("BigCoin") && other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().playSound("pickupCound");
            PlayerManager.coinsCollected += 5;
            PlayerManager.bigCoinSel = true;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().playSound("pickupCoin");
            PlayerManager.coinsCollected++;
        }
    }
}
