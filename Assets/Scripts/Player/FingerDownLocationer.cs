using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerDownLocationer : MonoBehaviour
{
    public static bool altAim;
    SpriteRenderer sr;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Text alt;

    public Vector2 mousePos, AimDir;
    public float maxDistance;
    public bool isActive;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (PlayerPrefs.GetInt("AltAim", 0) == 0)
            altAim = false;
        else
            altAim = true;
        alt.text = "altAim = " + altAim;
    }


    void Update()
    {
        if (cam == null) cam = Camera.main;

        if (Input.GetMouseButtonDown(0))
        {
            transform.position = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
            sr.enabled = true;
            isActive = true;
            cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isActive = false;
            sr.enabled = false;
        }

        if (Input.GetMouseButton(0) && sr.enabled == true)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(mousePos, transform.position) > maxDistance)
            {
                Vector2 dir = HelpfulFuncs.Norm1((Vector2)transform.position - mousePos);
                transform.position = mousePos + dir * maxDistance;
            }

            AimDir = HelpfulFuncs.Norm1((Vector2)transform.position - mousePos);

            if (altAim)
                AimDir *= -1;
        }
    }
    public void ToggleAltAim()
    {
        altAim = !altAim;
        if (altAim)
            PlayerPrefs.SetInt("AltAim", 1);
        else PlayerPrefs.SetInt("AltAim", 0);
        alt.text = "altAim = " + altAim;
    }
}
