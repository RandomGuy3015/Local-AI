using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player1" && gameObject.transform.tag == "Player0")
        {
            other.GetComponent<UnitController>().health--;
            gameObject.GetComponent<UnitController>().health--;
        }
        else if (other.transform.tag == "Player1" && gameObject.transform.tag == "Player0")
        {
            other.GetComponent<UnitController>().health--;
            gameObject.GetComponent<UnitController>().health--;
        } 
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}