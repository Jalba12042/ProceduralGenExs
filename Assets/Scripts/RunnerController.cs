using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject thingPrefab;
    Vector3 pos;


    // Start is called before the first frame update
    void Start()
    {
        //GameObject go = Instantiate(thingPrefab);
        //go.transform.position = Vector3.one * 2.0f;
        //Instantiate(thingPrefab, 1.0f * Vector3.one, Quaternion.identity);
        pos = Vector3.up * 2.0f + Vector3.right * 2.0f + Vector3.forward *2.0f;
        Instantiate(thingPrefab, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
