using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalkeeper : MonoBehaviour
{
    public Transform anchor;
    public float speed;
    private float target;
    // Start is called before the first frame update
    void Start()
    {
        target = Random.Range(0, 8) + anchor.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - target) < 0.2)
        {
            target = Random.Range(0, 7) + anchor.position.x;
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target, transform.position.y, transform.position.z), 0.01f * speed);
    }
}
