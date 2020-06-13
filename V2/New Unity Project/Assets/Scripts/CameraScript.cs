using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

	public Transform followTarget;

	public float zDistance;

    // Start is called before the first frame update
    void Start()
    {
    	zDistance = followTarget.position.z - transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
    	transform.position = new Vector3(followTarget.position.x, transform.position.y, followTarget.position.z - zDistance);
        
    }
}
