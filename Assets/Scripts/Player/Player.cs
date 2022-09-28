using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 aimDir, currentVel, preCollVel, p2;
    public bool isAttacking, walled, fingerDowned;
    public delegate void Action();
    public delegate void ActionGamgeObject(GameObject gameObject);
    public Action onTakeDamage, onFingerDown, onReleaseFinger, whileAiming, onGrounded, onUnWall, onWalled, whileAttaking;
    public ActionGamgeObject onHit;
    Vector3 scale;
    [SerializeField]
    GameObject sprite, trajectorySprite;
    [SerializeField]
    SpriteRenderer sr;
    public static Player playerInstence;
    [SerializeField]
    FingerDownLocationer finger;
    [SerializeField]
    Color normalColor, dashingColor;


    public int damage, baseDamage;

    public Rigidbody2D rig;
    int timesDone = 0;
    public int maxTimesDone = 2;

    Vector2 tiltDir;

    public Vector2 wallNormal;
    public bool canStick = true;

    private void Awake()
    {
        playerInstence = this;
        onGrounded += delegate { };
    }
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
    }

    void Update()
    {
        if (PauseMenu.paused) return;
        currentVel = rig.velocity;

        if (isAttacking)
        {
            SetSize();
            whileAttaking();
            print(BasicHit.singelton.curDamage);
            if (sr.color != Color.clear)
                sr.color = Color.Lerp(normalColor, dashingColor, rig.velocity.magnitude / 150);
            /* rig.mass = BasicHit.singelton.curDamage / 2;
             if (rig.mass < 1) rig.mass = 1;*/
        }
        else
        {
            if (sr.color != Color.clear)
                sr.color = Color.Lerp(sr.color, normalColor, 0.07f);
            //rig.mass = 1;
        }

        if (timesDone >= maxTimesDone) return;

        if (Input.GetMouseButtonDown(0))
        {
            onFingerDown();
            fingerDowned = true;
            //***********************************   finger down     *******************************
        }
        else
        {
            if (fingerDowned)
            {
                aimDir = finger.AimDir;
                if (Input.GetMouseButtonUp(0))
                {
                    aimDir = finger.AimDir;

                    if ((Vector2)finger.transform.position != finger.mousePos)
                    {
                        timesDone++;
                        isAttacking = true;
                        onReleaseFinger();
                        if (walled) onUnWall();
                        trajectorySprite.SetActive(false);

                        //***********************************   release     *******************************
                    }
                    fingerDowned = false;
                }
                else
                {
                    if ((Vector2)finger.transform.position != finger.mousePos && trajectorySprite.activeInHierarchy == false)
                    {
                        tiltDir = -aimDir;
                        sprite.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(tiltDir.x, tiltDir.y) * Mathf.Rad2Deg * -1);
                        sprite.transform.localScale = new Vector3(0.7f, 1.3f);
                        trajectorySprite.SetActive(true);
                    }

                    tiltDir = HelpfulFuncs.Norm1(-aimDir);
                    sprite.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(tiltDir.x, tiltDir.y) * Mathf.Rad2Deg * -1);
                    whileAiming();
                    //***********************************   aim     *******************************
                }
            }

        }
        /* Vector2 tiltDir = HelpfulFuncs.Norm1(t.position - downPos);
         transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(tiltDir.x, tiltDir.y) * Mathf.Rad2Deg * -1);*/
    }

    void SetSize()
    {
        tiltDir = HelpfulFuncs.Norm1(rig.velocity);
        sprite.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(tiltDir.x, tiltDir.y) * Mathf.Rad2Deg * -1);
        float magnitude = rig.velocity.magnitude / 50;
        sprite.transform.localScale = new Vector3(scale.x - magnitude * 0.3f, scale.y + magnitude * 0.3f);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {

        if (isAttacking)
        {
            sprite.transform.localScale = scale;
            Hp hitHp = coll.collider.GetComponent<Hp>();
            if (hitHp)
            {
                onHit(coll.gameObject);
                //if (hitHp.hp > 0)
                    rig.velocity = Vector2.Reflect(rig.velocity, coll.GetContact(0).normal) * (15 / rig.velocity.magnitude);
            }
            else
            {
                float deg = Vector2.Angle(-currentVel, coll.GetContact(0).normal);
                if (canStick)
                {
                    if (coll.GetContact(0).normal == Vector2.down || coll.GetContact(0).normal == Vector2.left || coll.GetContact(0).normal == Vector2.right || coll.collider.tag == "Stuckable")
                    {
                        if (deg < 70)
                        {
                            ResetTimesDone();
                            preCollVel = currentVel;
                            Invoke("ResetPreWallVel", 0.5F);
                            //***********************************   walled     *******************************
                            onWalled();
                            wallNormal = coll.GetContact(0).normal;
                            isAttacking = false;
                        }
                    }

                }
                if (coll.GetContact(0).normal == Vector2.up)
                {
                    if (deg < 50)
                    {
                        isAttacking = false;
                    }

                    ResetTimesDone();
                    onGrounded();
                }
            }

        }
        //***********************************   grounded     *******************************
        else
        if (coll.GetContact(0).normal == Vector2.up)
        {
            float deg = Vector2.Angle(-currentVel, coll.GetContact(0).normal);
            ResetTimesDone();
            onGrounded();
        }

    }

    void ResetPreWallVel()
    {
        preCollVel *= 0;
    }
    public void ResetTimesDone()
    {
        timesDone = 0;
    }
}
