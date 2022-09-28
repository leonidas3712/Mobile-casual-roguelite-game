using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{

    public int maxHp,hp = 1;
    public GameObject hitParticalsPrefab, deathParticlsPrefabs;
    [SerializeField]
    public SpriteRenderer sr;
    public delegate void StuffTahtHappens();
    public StuffTahtHappens onTakeDamage;
    int deathEffectMultiplier;
    private void Awake()
    {
        onTakeDamage += delegate { };
    }
    public virtual void Start()
    {
        hp = maxHp;
    }

    public virtual void Die()
    {
        if (deathParticlsPrefabs&&sr)
        {
            ParticleSystem ps = Instantiate(deathParticlsPrefabs, transform.position, transform.rotation).GetComponent<ParticleSystem>();
            var main = ps.main;
            main.startColor = sr.color;
            main.startSpeedMultiplier = deathEffectMultiplier;
            ps.transform.parent = Room.currentRoom.transform;
        }
        if (Room.currentRoom.cameraAnimator)
            Room.currentRoom.cameraAnimator.SetTrigger("Shake");
        Destroy(gameObject);
    }
    public virtual void TakeDamage(int damage)
    {
        onTakeDamage();
        hp -= damage;
        if (hp <= 0)
        {
            deathEffectMultiplier = damage*5;
            if (deathEffectMultiplier < 1) deathEffectMultiplier = 1;
            Die();
        }
    }
}
