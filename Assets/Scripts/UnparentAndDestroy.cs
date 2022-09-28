using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentAndDestroy : MonoBehaviour
{
    
    void Start()
    {
        foreach(Transform t in GetComponentsInChildren<Transform>())
        {
            t.parent = transform.parent;
        }
        Destroy(gameObject);
    }
    

}
