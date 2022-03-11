using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_test : MonoBehaviour
{
    GameObject player;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + pos;
    }
}
