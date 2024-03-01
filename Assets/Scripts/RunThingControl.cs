using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunThingControl : MonoBehaviour
{
    float theta;
    public float thingRange;
    public float spinRate;
    Vector3 pos, pos0, dPos;
    // Start is called before the first frame update
    void Start()
    {
        thingRange = 3.0f;
        spinRate = 0.5f;
        pos0 = transform.position + Vector3.right * 20.0f + Vector3.forward * 20.0f ;
        dPos = Vector3.forward;
        pos += thingRange * dPos.normalized;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        theta = spinRate * 2.0f * Mathf.PI * Time.time;
        dPos = Vector3.right * Mathf.Sin(theta) + Vector3.forward * Mathf.Cos(theta);
        pos = pos0 + thingRange * dPos.normalized;
        transform.position = pos;
        Debug.Log(pos);
    }
}
