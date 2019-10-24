using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustscale : MonoBehaviour
{
    public float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        x = z = 1f;
        y = 0.5f;
    }
    private void Update()
    {
        transform.localScale = new Vector3(x, y, z);
    }

}
