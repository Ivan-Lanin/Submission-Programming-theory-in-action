using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + "Col");
        if (collision.gameObject.GetComponent<Meteor>())
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "Trig");
        if (other.gameObject.GetComponent<Meteor>())
        {
            Destroy(other.gameObject);
        }
    }
}
