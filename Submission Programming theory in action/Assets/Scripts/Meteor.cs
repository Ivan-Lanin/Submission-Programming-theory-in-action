using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private float time;

    private void Start()
    {
        time = Time.time;
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 2.5f);
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        time = Time.time - time;
    }
}
