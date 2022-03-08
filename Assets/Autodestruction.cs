using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruction : MonoBehaviour
{

    public float life_time = 5;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, life_time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
