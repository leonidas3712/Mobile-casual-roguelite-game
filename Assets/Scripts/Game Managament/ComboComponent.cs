using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboComponent : MonoBehaviour
{
    [SerializeField]
    float inlargingFactor,endSize,time,movmentSpeed;

    [SerializeField]
    TextMeshPro text;

    Vector2 dir;

    public void SetNumber(int num,Vector2 dir)
    {
        text.SetText( "combo "+num);
        Destroy(gameObject, time);
        this.dir = dir;
    }
    void Update()
    {
        if (transform.localScale.x < endSize) transform.localScale +=new Vector3(1,1,0) * inlargingFactor*Time.deltaTime;
        transform.position += (Vector3)dir * movmentSpeed * Time.deltaTime;
    }
}
