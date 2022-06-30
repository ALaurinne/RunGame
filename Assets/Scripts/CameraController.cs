using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject target;

    float offsetX;
    // Start is called before the first frame update
    void Start()
    {
        offsetX = transform.position.x - target.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x + offsetX, transform.position.y, transform.position.z);
    }
}
