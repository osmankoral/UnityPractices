using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyManagaer : MonoBehaviour
{
    //[SerializeField]
    //private Transform go;

    [SerializeField]
    private GameObject enemyHealtBar;

    [SerializeField]
    private Text enemyNameTxt, enemyHealthTxt;

    public float enemyMaxHealth;
    public float enemyHealth;
    private float enemyTakeDmg;
    private float enemyDmg;
    private float enemyMaxDmg;
    private float enemyMinDmg;
    public int exp;

    private string enemyName;

    Enemy enemySt;

    private void Awake()
    {
        enemySt = GameObject.FindObjectOfType<Enemy>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void TakeDamage(float enemyTakeDmg)
    {
        enemyHealth -= enemyTakeDmg;
        enemyHealtBar.transform.GetComponent<Slider>().value = enemyHealth;
        enemyHealthTxt.text = enemyHealth.ToString();
    }

    public float EnemyDamage()
    {
        return Random.Range(enemyMinDmg, enemyMaxDmg + 1);
    }

    public void EnemyTakeMove()
    {
        StartCoroutine(EnemyTakeAnimation());
    }

    public void EnemyAtackMove()
    {
        StartCoroutine(PlayerAtackAnimation());
    }

    public void EnemyStat(string enemyName, int enemyMaxHealth, int enemyMaxDmg, int enemyMinDmg, int _exp)
    {
       this.enemyName = enemyName;

        this.enemyMaxHealth = enemyMaxHealth;
        this.enemyMaxDmg = enemyMaxDmg;
        this.enemyMinDmg = enemyMinDmg;
        exp = _exp;

        enemyHealth = enemyMaxHealth;
        enemyHealtBar.transform.GetComponent<Slider>().maxValue = enemyMaxHealth;
        enemyHealtBar.transform.GetComponent<Slider>().value = enemyHealth;

        enemyNameTxt.text = enemyName;
        enemyHealthTxt.text = enemyHealth.ToString();
    }

    IEnumerator EnemyTakeAnimation()
    {
        //gameObject.transform.DOLocalMoveX(8, .2f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(.2f);
        gameObject.transform.DOLocalMoveX(5, 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(.2f);
        gameObject.transform.DOLocalMoveX(4, 1f).SetEase(Ease.InBounce);
    }

    IEnumerator PlayerAtackAnimation()
    {
        yield return new WaitForSeconds(.6f);
        gameObject.transform.DOLocalMoveX(-5, .2f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(.6f);

        gameObject.transform.DOLocalMoveX(4, 1f).SetEase(Ease.OutBack);
    }
}
