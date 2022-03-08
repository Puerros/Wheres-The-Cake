using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearEnd : MonoBehaviour
{
    public GameObject canv;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        canv.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        canv.SetActive(false);
    }
}
