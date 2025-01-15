using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private EnemyRowScript enemyRowScript;
    [SerializeField] private GameObject EnableShootUpperEnemy;
    [SerializeField] private GameObject EnemyOrangeBullet;
    [SerializeField] private GameObject EnemyGrayBullet;
    private AudioSource enemyAudioSource;
    [SerializeField] private AudioClip enemyDeadClip;
    [SerializeField] private AudioClip enemyShotClip;
    private float shotTime;
    private float maxShootTime;
    private float minShootTime;
    private float enemAnimSpeed;
    [SerializeField] private bool initialCanShoot;
    [SerializeField] private GameObject greenExplosion;
    [SerializeField] private GameObject blueExplosion;
    private bool canShoot;    
    private GameObject sceneController;
    private SceneControllerScript scScript;
    private Animator animator;
    private GameObject rowsContainer;
    private EnemyContainerScript rowsCon;
    private bool blackBackground;
    private int lives;
    private int enemyLives;
    private int enemyNumber;


    // Start is called before the first frame update
    void Start()
    {
        enemAnimSpeed = 0.3f;
        sceneController = GameObject.FindGameObjectWithTag("SceneController");
        scScript = sceneController.GetComponent<SceneControllerScript>();
        enemyAudioSource = GetComponent<AudioSource>();
        enemyRowScript = gameObject.GetComponentInParent<EnemyRowScript>();
        rowsContainer = GameObject.FindGameObjectWithTag("RowsContainer");
        rowsCon = rowsContainer.GetComponent<EnemyContainerScript>();
        shotTime = Random.Range(5.0f, 10.0f);
        animator = gameObject.GetComponent<Animator>();
        animator.speed = 0.3f;
        lives = 1;
        enemyLives = lives;
        canShoot = initialCanShoot;
        blackBackground = false;
        minShootTime = scScript.GetMinTimeShoot();
        maxShootTime = scScript.GetMaxTimeShoot();
    }

    // Update is called once per frame


    void Update()
    {
        if (canShoot)
        {
            if (scScript.GetEnemiesCanShoot())
            {
                shotTime -= Time.deltaTime;
                if (shotTime < 0)
                {

                    if (blackBackground == false)
                    {
                        Instantiate(EnemyOrangeBullet, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.85f), Quaternion.identity);
                    }
                    else if (blackBackground == true)
                    {
                        Instantiate(EnemyGrayBullet, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.85f), Quaternion.identity);
                    }
                    if (gameObject.activeSelf)
                    {
                        enemyAudioSource.PlayOneShot(enemyShotClip);
                    }

                    minShootTime = scScript.GetMinTimeShoot();
                    maxShootTime = scScript.GetMaxTimeShoot();
                    shotTime = Random.Range(minShootTime, maxShootTime);
                }
            }
        }
    }

    public void SetEnemyNumber(int enemNumberSetted)
    {
        enemyNumber = enemNumberSetted;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            rowsCon.ChangeDirectionToRight();
        }
        if (collision.gameObject.CompareTag("RightWall"))
        {
            rowsCon.ChangeDirectionToLeft();
        }


        if (collision.gameObject.CompareTag("MaxLowPos"))
        {
            enemyRowScript.RowAccSpeedPosReached();

        }
        if (collision.gameObject.CompareTag("RealMaxLowPos"))
        {
            enemyRowScript.RowAccShootingPosReached();
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            enemyLives--;
            if (enemyLives == 0)
            {
                canShoot = false;
                if (EnableShootUpperEnemy != null)
                {
                    EnableShootUpperEnemy.GetComponent<EnemyScript>().EnableShooting();
                }
                enemyAudioSource.PlayOneShot(enemyDeadClip);

                gameObject.SetActive(false);
                if (blackBackground == false)
                {
                    Instantiate(greenExplosion, transform.position, Quaternion.identity);
                }
                else if (blackBackground == true)
                {
                    Instantiate(blueExplosion, transform.position, Quaternion.identity);
                }
                enemyRowScript.EnemyDied(enemyNumber);
            }

        }
    }


    public void EnableShooting()
    {
        if (gameObject.activeSelf == true && canShoot == false)
        {
            minShootTime = scScript.GetMinTimeShoot();
            maxShootTime = scScript.GetMaxTimeShoot();
            shotTime = Random.Range(minShootTime, maxShootTime);
            canShoot = true;
        }
    }


    public void Restart()
    {
        minShootTime = scScript.GetMinTimeShoot();
        maxShootTime = scScript.GetMaxTimeShoot();
        shotTime = Random.Range(minShootTime, maxShootTime);
        enemyLives = lives;
        gameObject.SetActive(true);

        canShoot = initialCanShoot;
        if (blackBackground == true)
        {
            blackBackground = false;
            animator.SetBool("DarkBg", false);
        }
        else if (blackBackground == false)
        {
            blackBackground = true;
            animator.SetBool("DarkBg", true);
        }
        animator.speed = 0.3f;
    }



    public void ChangeAnimationSpeed()
    {
        if (gameObject.activeSelf == true)
        {
            enemAnimSpeed = scScript.GetAnimationSpeed();
            animator.speed = enemAnimSpeed;
        }
    }
}
