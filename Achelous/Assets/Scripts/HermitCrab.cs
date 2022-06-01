using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitCrab : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    ParticleSystem waveParticles;

    GameObject player;


    enum AttackPhase { Idle, Phase1, Phase2, Phase3, Defeated }
    [SerializeField]
    AttackPhase attackPhase = AttackPhase.Idle;

    [SerializeField]
    List<Transform> weakSpots = new List<Transform>();

    [SerializeField]
    Collider[] armHitTriggers;
    Coroutine idleRoutine;

    bool leftArmAttack = false;
    bool attackStarted = false;


    public List<GameObject> activeShootables = new List<GameObject>();
    public GameObject shootable;
    public GameObject projectile;

    public float idleTime = 3;
    public float armAttackDelay = 5;
    public float slamAttackDelay = 10;
    public float slamParticleDelay = 0.5f;
    


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        waveParticles = gameObject.GetComponentInChildren<ParticleSystem>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SetPhase();
        }
    }

    
    //Spawn the shootable areas depending on the current phase.
    public void SpawnShootables()
    {
        activeShootables.Clear();
        switch (attackPhase)
        {
            case AttackPhase.Phase1:
                Debug.Log("Phase 1");
                activeShootables.Add(Instantiate(shootable, weakSpots[0].position, weakSpots[0].rotation, transform));

                idleRoutine = StartCoroutine(IdleTime());
                break;

            case AttackPhase.Phase2:
                Debug.Log("Phase 2");
                activeShootables.Add(Instantiate(shootable, weakSpots[1].position, weakSpots[1].rotation, transform));
                activeShootables.Add(Instantiate(shootable, weakSpots[2].position, weakSpots[2].rotation, transform));

                idleRoutine = StartCoroutine(IdleTime());
                break;

            case AttackPhase.Phase3:
                Debug.Log("Phase 3");
                activeShootables.Add(Instantiate(shootable, weakSpots[3].position, weakSpots[3].rotation, transform));

                idleRoutine = StartCoroutine(IdleTime());
                break;
        }
    }

    IEnumerator IdleTime()
    {
        yield return new WaitForSeconds(idleTime);

        if (!attackStarted)
        {
            attackStarted = true;
            Attack();
        }
    }

    public void Attack()
    {
        //Throw trash
        if (attackPhase == AttackPhase.Phase1)
        {
            StartCoroutine(ThrowAttack());
        }

        //Swing arms
        else if(attackPhase == AttackPhase.Phase2)
        {
            if (leftArmAttack)
            {

                animator.SetBool("RightAttack", false);
                animator.SetTrigger("ArmAttack");
            }
            else
            {
                animator.SetBool("RightAttack", true);
                animator.SetTrigger("ArmAttack");
            }

            StartCoroutine(ArmAttack());
        }

        //Slam Attack (Wave of trash)
        else if (attackPhase == AttackPhase.Phase3)
        {
            animator.SetTrigger("SlamAttack");
            StartCoroutine(SlamAttack());
        }
    }

    IEnumerator ThrowAttack()
    {
        yield return new WaitForSeconds(1);
        GameObject thrownObject = Instantiate(projectile);
        thrownObject.GetComponent<ThrashBall>().targetPosition = player.transform.position;
        thrownObject.GetComponent<ThrashBall>().player = player;


        yield return new WaitForSeconds(2);
        Attack();
    }

    //Time between arm attacks
    IEnumerator ArmAttack()
    {
        //Make arms lethal
        float startTime;
        float endedTime;

        if (leftArmAttack)
        {
            startTime = 1f;
            endedTime = 1f;
        } else
        {
            startTime = 1f;
            endedTime = 0.8f;
        }

        yield return new WaitForSeconds(startTime);

        if (leftArmAttack)
            armHitTriggers[0].enabled = true;
        else
            armHitTriggers[1].enabled = true;


        //Make arms non-lethal
        yield return new WaitForSeconds(endedTime);

        foreach (Collider arm in armHitTriggers)
        {
            arm.enabled = false;
        }

        leftArmAttack = !leftArmAttack;


        //Start next attack
        yield return new WaitForSeconds(armAttackDelay);
        Attack();
    }

    IEnumerator SlamAttack()
    {
        yield return new WaitForSeconds(slamParticleDelay);
        waveParticles.Play();

        yield return new WaitForSeconds(slamAttackDelay);
        Attack();
    }


    //What happens when the boss has been hit
    public void Hit()
    {
        activeShootables.RemoveAll(item => item == null);

        if (activeShootables.Count <= 1)
        {
            SetPhase();
        }
    }

    //Progress to the new Phase and do anything else that needs to happen at the start of the Phase
    void SetPhase()
    {
        activeShootables.RemoveAll(item => item == null);
        attackStarted = false;

        switch (attackPhase)
        {
            case AttackPhase.Idle:
                attackPhase = AttackPhase.Phase1;
                animator.SetTrigger("StartNewPhase");
                SpawnShootables();
                break;

            case AttackPhase.Phase1:
                attackPhase = AttackPhase.Phase2;
                animator.SetTrigger("StartNewPhase");
                SpawnShootables();
                break;

            case AttackPhase.Phase2:
                attackPhase = AttackPhase.Phase3;
                animator.SetTrigger("StartNewPhase");
                SpawnShootables();
                break;

            case AttackPhase.Phase3:
                attackPhase = AttackPhase.Defeated;
                break;

            //This one is for testing (Delete before publish)
            case AttackPhase.Defeated:
                attackPhase = AttackPhase.Idle;
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Hit the player
        if(other.gameObject.CompareTag("Player"))
            Debug.Log("Hit the Player");
    }
}