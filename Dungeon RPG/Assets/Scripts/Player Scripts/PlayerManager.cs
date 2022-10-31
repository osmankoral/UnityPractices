using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform startPos;
    //Prefs
    [SerializeField] private GameObject basicAttack;

    // Scriptable Objects
    [SerializeField] private PlayerStatScriptableObject playerStat;
    [SerializeField] private SkillDatabaseObject SkillDb;
    [SerializeField] private PhysicalSkillDatabase PhysicalSkillDb;

    // Private Objects
    private GameObject[] enemies;
    private Rigidbody2D rb;
    private Animator animator;


    // Scripts Referances
    PhysicalSkillManager physicalSkillManager;
    SkillManager skillManager;
    ItemManager itemManager;

    // Global Values
    public bool isGroundCheck;
    public bool isFaced;
    public bool isMove;
    public bool isFirstWeapon;
    public bool isPotion;

    public float maxHealth;
    public float health;
    public float mana;
    public float maxMana;
    public float stamina;
    public float maxStamina;
    public float exp;
    public float maxExp;
    public int level;
    public float attackDamage1;
    public float attackDamage2;
    public float attackDamage;
    

    // Private Values
    private int meterCount;
    private float attackRange;
    private float attackRange1;
    private float attackRange2;
    private bool isRanged;
    private bool isRanged1;
    private bool isRanged2;
    private float attackCooldown;
    private float attackSpeed;
    private int firstStartValue;
    private float speed = 5f;
    private bool inAttack;
    public int classType;
    private int classType1;
    private int classType2;


    public void StatUpdate(float attack1, float attack2, float attackRange1, float attackRange2, bool isRanged1, bool isRanged2, 
    float maxHealth, float maxMana, float maxStamina, int _classType1, int _classType2, float _exp, float _maxExp, int _level)
    {
        this.attackDamage1 = attack1;
        this.attackDamage2 = attack2;
        this.attackRange1 = attackRange1;
        this.attackRange2 =attackRange2;
        this.isRanged1 = isRanged1;
        this.isRanged2 = isRanged2;
        this.maxHealth = maxHealth;
        this.maxMana = maxMana;
        this.maxStamina = maxStamina;
        classType1 = _classType1;
        classType2 = _classType2;
        exp = _exp;
        maxExp = _maxExp;
        level = _level;

        if(firstStartValue < 1)
        {
            PlayerStat();
            firstStartValue = 2;
        }
    }
    private void PlayerStat()
    {
        attackDamage = attackDamage1;
        attackSpeed = 0.5f;
        health = maxHealth;
        mana = maxMana;
        stamina = maxStamina;
        attackRange = attackRange1;
        isRanged = isRanged1;
        classType = classType1;
    }

    void Awake()
    {
        itemManager = GameObject.FindObjectOfType<ItemManager>();
        physicalSkillManager = GameObject.FindObjectOfType<PhysicalSkillManager>();
        skillManager = GameObject.FindObjectOfType<SkillManager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        isFirstWeapon = true;

        InvokeRepeating("Regen", 0f, 0.5f);
    }

    void Update()
    {
        if(health <= 0)
        {
            transform.position = startPos.position;
            health = maxHealth;
            mana = maxMana;
            stamina = maxStamina;
        }
        if(attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        animator.SetBool("isMove", isMove);
        animator.SetBool("isJump", !isGroundCheck);
    }

    void FixedUpdate()
    {

    }
    public void Attack()
    {
        if (attackCooldown <= 0)
        {
            Debug.Log(attackDamage);
            inAttack = true;
            animator.SetBool("isAttack", true);

            foreach (var enemy in enemies)
            {
                Vector3 dis = enemy.transform.position - transform.position;
                float disEnemyCol = enemy.gameObject.GetComponent<BoxCollider2D>().size.x / 2;
                float disMyCol = gameObject.GetComponent<BoxCollider2D>().size.x / 2;
                float inRange = Mathf.Abs(dis.x) - disEnemyCol - disMyCol;
                

                // Enemy Out AttackRange Fire
                if(isRanged && inRange > attackRange)
                {
                    GameObject bullet = Instantiate(basicAttack, transform.GetChild(0).position, transform.GetChild(0).rotation);
                }
                // Enemy in Attack Range and I Not look Enemy and my weapon has ranged fire
                if (dis.x < 0 && !isFaced && inRange <= attackRange && isRanged)
                {
                    GameObject bullet = Instantiate(basicAttack, transform.GetChild(0).position, transform.GetChild(0).rotation);
                }
                // Enemy in Attack Range and I Not look Enemy and my weapon has ranged fire
                if (dis.x > 0 && isFaced && inRange <= attackRange && isRanged)
                {
                    GameObject bullet = Instantiate(basicAttack, transform.GetChild(0).position, transform.GetChild(0).rotation);
                }
                // Enemy in Attack Range attack
                if (dis.x > 0 && !isFaced && inRange <= attackRange)
                {
                    enemy.GetComponent<EnemyManager>().TakeDamage(attackDamage, -1);
                    PhysicalSkillDb.AttackGain();
                    if(classType == 1)
                        SkillDb.WarriorClassGain();
                    else if(classType == 2)
                        SkillDb.WizardClassGain();
                }

                if (dis.x < 0 && isFaced && inRange <= attackRange)
                {
                    enemy.GetComponent<EnemyManager>().TakeDamage(attackDamage, -1);
                    PhysicalSkillDb.AttackGain();
                    if(classType == 1)
                        SkillDb.warriorClassExp++;
                    else if(classType == 2)
                        SkillDb.wizardClassExp++;
                }
            }
                    
            StartCoroutine(DelayAttack());

            attackCooldown = attackSpeed;
        }
    }
    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.3f);
        inAttack = false;
        animator.SetBool("isAttack", false);
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color32(236, 70, 70, 60);
        Vector3 dir = transform.position;

        dir.y -= 0.8f;
        if (!isFaced)
        {
            dir.x += 1;
            Gizmos.DrawCube(dir, new Vector3(attackRange, 1, 0));
        }
        if (isFaced)
        {
            dir.x -= 1;
            Gizmos.DrawCube(dir, new Vector3(attackRange * -1, 1, 0));
        }

    }

    // Half Second Regeneration
    private void Regen()
    {
        if (health < maxHealth)
        {
            health += 1.5f;
            if(health > maxHealth)
                health = maxHealth;
        }
        if (mana < maxMana)
        {
            mana += 1;
            if(mana > maxMana)
                mana = maxMana;
        }
        if (stamina < maxStamina)
        {
            stamina += 1;
                if(stamina > maxStamina)
                    stamina = maxStamina;
        }

    }

    public void Skill(int skillNumber)
    {
        skillNumber--;
        skillManager.Skill(skillNumber, isFirstWeapon, classType);
    }


    public void Dash(int dis)
    {
        if (isGroundCheck && stamina >= 5)
        {
            physicalSkillManager.Dash(rb, dis, 2);
            stamina -= 5;
            PhysicalSkillDb.DashGain();
        }

    }

    public void MoveLeft()
    {
        if(!inAttack)
        {
            if (!isFaced)
            {
                Quaternion dir = transform.rotation;
                dir.y = 90;
                transform.rotation = dir;

                isFaced = !isFaced;
            }

            Vector2 a = rb.position;
            a.x -= speed * Time.deltaTime;
            rb.position = a;

            meterCount++;
            if (meterCount == 20)
            {
                PhysicalSkillDb.WalkGain();
                meterCount = 0;
            }
        }

    }
    public void MoveRight()
    {
        if(!inAttack)
        {
            if (isFaced)
            {
                Quaternion dir = transform.rotation;
                dir.y = 0;
                transform.rotation = dir;

                isFaced = !isFaced;
            }

            Vector2 a = rb.position;
            a.x += speed * Time.deltaTime;
            rb.position = a;

            // 20 repeat 1m equal;
            meterCount++;
            if (meterCount == 20)
            {
                PhysicalSkillDb.WalkGain();
                meterCount = 0;
            }
        }

    }
    public void MoveJump()
    {
        //animator.SetBool("isJump", !isGroundCheck);
        if (isGroundCheck)
        {
            rb.AddForce(transform.up * 250);
            PhysicalSkillDb.JumpGain();
        }

    }
    public void MoveBow()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void SwitchWeapon()
    {
        isFirstWeapon = !isFirstWeapon;
        if(!isFirstWeapon)
        {
            attackDamage = attackDamage2;
            attackRange = attackRange2;
            isRanged = isRanged2;
            classType = classType2;
        }
        if(isFirstWeapon)
        {
            attackDamage = attackDamage1;
            attackRange = attackRange1;
            isRanged = isRanged1;
            classType = classType1;
        }
    }

    public void GainExp(float _exp)
    {
        playerStat.ExpGain(_exp);
    }
}
