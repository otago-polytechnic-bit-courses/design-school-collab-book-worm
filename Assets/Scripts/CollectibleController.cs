using System;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
  
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectibleManager.Instance.Collect();
            Destroy(gameObject);
        }
    }
}


