using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Alarm : MonoBehaviour
{
    [SerializeField] public string Scence;
    private void Start()
    {
        print("ahskdj");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            print("jsakdl");
        }
    }

}
