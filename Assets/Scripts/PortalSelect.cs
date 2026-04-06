using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSelect : MonoBehaviour
{
    private string portalName = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        portalName = transform.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.body.name == "Player")
        {
            //Open world 1
            switch (portalName)
            {
                default:
                    Debug.Log("Portal Without a Name");
                    break;
                case "Hub Portal":
                    SceneManager.LoadScene(0);
                    break;
                case "World1 Portal":
                    SceneManager.LoadScene(1);
                    break;
            }
        }
    }
}
