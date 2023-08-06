using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 2.5f);
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
