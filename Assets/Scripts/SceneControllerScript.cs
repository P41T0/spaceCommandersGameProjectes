using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneControllerScript : MonoBehaviour
{
    private int numHitsToGoDown;
    [SerializeField] private GameObject[] enemyRows;
    [SerializeField] private GameObject enemyRowContainer;
    private bool enemiesCanShoot;
    private EnemyContainerScript enemyRowContScript;
    [SerializeField] private GameObject[] defenses;
    [SerializeField] private GameObject bonusEnemySpawn;
    [SerializeField] private GameObject redBonusEnemy;
    [SerializeField] private GameObject purpleBonusEnemy;
    [SerializeField] private GameObject[] livesObject;
    [SerializeField] private GameObject player;
    private PlayerControllerScript playerScript;
    private GameObject bonusEnemySpawned;
    private bool bonusEnemyActive;
    private List<bool> rowsHaveEnemsAlive;
    [SerializeField] private TMP_Text roundTextNum;
    [SerializeField] private TMP_Text roundText;
    [SerializeField] private TMP_Text scoreTextNum;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private AudioClip l1Clip;
    [SerializeField] private AudioClip l2Clip;
    [SerializeField] private AudioClip changeClip;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource effectsAudioSource;
    private float bonusEnemySpawnTime;
    private bool maxPosition;
    private bool blackBackgroundBool;
    private float initialEnemySpeed;
    private float EnemySpeed;
    private float initialMinTimeShoot;
    private float initialMaxTimeShoot;
    private float minTimeShoot;
    private float maxTimeShoot;
    private int numRowsDefeteated;
    private float animSpeed;
    private float levelMusicWait;
    private bool levelMusicStarted;
    private float animInitialSpeed;
    private int lives;
    private int score;
    private int round;
    private bool speedAugmented;
    private Color blueBackground;
    private Color blackBackground;
    private Color OrangeTextColor;
    private Color redBackground;
    private Color yellowBackground;
    private Color GrayTextColor;
    [SerializeField] private Camera mainCamera;
    private float playerDied;
    private bool playerDiedBool;



    // Start is called before the first frame update
    void Start()
    {
        effectsAudioSource.PlayOneShot(changeClip);
        initialEnemySpeed = 2.0f;
        EnemySpeed = initialEnemySpeed;
        numHitsToGoDown = 8;
        maxPosition = false;
        bonusEnemyActive = false;
        speedAugmented = false;
        blackBackgroundBool = false;
        initialMinTimeShoot = 5.5f;
        initialMaxTimeShoot = 11.0f;
        bonusEnemySpawnTime = Random.Range(20.0f, 50.0f);
        minTimeShoot = initialMinTimeShoot;
        maxTimeShoot = initialMaxTimeShoot;
        numRowsDefeteated = enemyRows.Length;
        rowsHaveEnemsAlive = new List<bool> { };
        for (int i = 0; i < enemyRows.Length; i++)
        {
            rowsHaveEnemsAlive.Add(true);
            enemyRows[i].GetComponent<EnemyRowScript>().SetRowNumber(i);
        }
        round = 1;
        score = 0;
        scoreTextNum.text = score.ToString("D5");
        roundTextNum.text = round.ToString("D3");
        animInitialSpeed = 0.3f;
        animSpeed = animInitialSpeed;
        lives = livesObject.Length;
        playerScript = player.GetComponent<PlayerControllerScript>();
        enemyRowContScript = enemyRowContainer.GetComponent<EnemyContainerScript>();
        enemiesCanShoot = true;
        blackBackground = new Color(0, 0, 0, 1);
        blueBackground = new Color(0.01f, 0, 0.63f, 1);
        redBackground = Color.red;
        yellowBackground = Color.yellow;
        OrangeTextColor = new Color(0.9803922f, 0.6156863f, 0.2745098f, 1.0f);
        GrayTextColor = new Color(0.9411765f, 0.9411765f, 0.9411765f, 1.0f);
        mainCamera.backgroundColor = blueBackground;
        playerDied = 0.0f;
        playerDiedBool = false;
        levelMusicWait = changeClip.length;
        levelMusicStarted = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (levelMusicStarted == false)
        {
            levelMusicWait -= Time.deltaTime;
            if (levelMusicWait < 0.0f)
            {
                if (blackBackgroundBool == false)
                {
                    musicAudioSource.clip = l2Clip;
                }
                else if (blackBackgroundBool == true)
                {
                    musicAudioSource.clip = l1Clip;
                }
                musicAudioSource.Play();
                levelMusicStarted = true;
            }
        }
        if (playerDiedBool == true)
        {
            playerDied -= Time.deltaTime;

            if (blackBackgroundBool == true)
            {
                if (playerDied > 2.0f)
                {
                    mainCamera.backgroundColor = GrayTextColor;
                }
                else if (playerDied > 1.8f)
                {
                    mainCamera.backgroundColor = Color.magenta;
                }
                else if (playerDied > 1.6f)
                {
                    mainCamera.backgroundColor = GrayTextColor;
                }
                else if (playerDied > 1.4f)
                {
                    mainCamera.backgroundColor = Color.magenta;
                }
                else if (playerDied > 1.2f)
                {
                    mainCamera.backgroundColor = GrayTextColor;
                }
                else if (playerDied > 1.0f)
                {
                    mainCamera.backgroundColor = Color.magenta;
                }
                else if (playerDied > 0.8f)
                {
                    mainCamera.backgroundColor = GrayTextColor;
                }
                else if (playerDied > 0.6f)
                {
                    mainCamera.backgroundColor = Color.magenta;
                }
                else if (playerDied > 0.4f)
                {
                    mainCamera.backgroundColor = GrayTextColor;
                }
                else if (playerDied > 0.2f)
                {
                    mainCamera.backgroundColor = Color.magenta;
                }
                else
                {
                    playerDiedBool = false;
                    mainCamera.backgroundColor = blackBackground;
                }
            }
            else if (blackBackgroundBool == false)
            {
                if (playerDied > 2.0f)
                {
                    mainCamera.backgroundColor = yellowBackground;
                }
                else if (playerDied > 1.8f)
                {
                    mainCamera.backgroundColor = redBackground;
                }
                else if (playerDied > 1.6f)
                {
                    mainCamera.backgroundColor = yellowBackground;
                }
                else if (playerDied > 1.4f)
                {
                    mainCamera.backgroundColor = redBackground;
                }
                else if (playerDied > 1.20f)
                {
                    mainCamera.backgroundColor = yellowBackground;
                }
                else if (playerDied > 1.0f)
                {
                    mainCamera.backgroundColor = redBackground;
                }
                else if (playerDied > 0.8f)
                {
                    mainCamera.backgroundColor = yellowBackground;
                }
                else if (playerDied > 0.6f)
                {
                    mainCamera.backgroundColor = redBackground;
                }
                else if (playerDied > 0.4f)
                {
                    mainCamera.backgroundColor = yellowBackground;
                }
                else if (playerDied > 0.2f)
                {
                    mainCamera.backgroundColor = redBackground;
                }
                else if (playerDied <= 0)
                {
                    playerDiedBool = false;
                    mainCamera.backgroundColor = blueBackground;
                }
            }

        }

        if (bonusEnemyActive == false)
        {
            if (enemiesCanShoot == true)
            {
                bonusEnemySpawnTime -= Time.deltaTime;
                if (bonusEnemySpawnTime <= 0)
                {
                    if (blackBackgroundBool == false)
                    {
                        bonusEnemySpawned = Instantiate(redBonusEnemy, bonusEnemySpawn.transform.position, Quaternion.identity);
                    }
                    else if (blackBackgroundBool == true)
                    {
                        bonusEnemySpawned = Instantiate(purpleBonusEnemy, bonusEnemySpawn.transform.position, Quaternion.identity);
                    }

                    bonusEnemyActive = true;
                }
            }
        }

    }



    public float GetSpeed()
    {
        if (EnemySpeed <= 0)
        {
            return 2.0f;
        }
        else
        {
            return EnemySpeed;
        }

    }



    public float GetMaxTimeShoot()
    {
        return maxTimeShoot;
    }



    public float GetMinTimeShoot()
    {
        return minTimeShoot;
    }



    public void EnemySCDied(int scoreToIncrease)
    {
        score += scoreToIncrease;
        scoreTextNum.text = score.ToString("D5");
        if (minTimeShoot > 0.7f)
        {
            minTimeShoot -= 0.1f;
        }
        if (maxTimeShoot > 1.4f)
        {
            maxTimeShoot -= 0.2f;
        }
        if (EnemySpeed < 10.0f)
        {
            EnemySpeed += 0.10f;
        }




    }



    public void LateralHit()
    {
        if (!maxPosition)
        {
            numHitsToGoDown--;
            if (numHitsToGoDown <= 0)
            {
                if (EnemySpeed < 10.0f)
                {
                    EnemySpeed += 0.20f;
                }

                if (animSpeed < 1.6f)
                {
                    animSpeed += 0.2f;
                    for (int i = 0; i < enemyRows.Length; i++)
                    {
                        if (rowsHaveEnemsAlive[i] == true)
                        {
                            enemyRows[i].GetComponent<EnemyRowScript>().IncrementAnimSpeed();
                        }
                    }
                }
                enemyRowContainer.transform.position = new Vector2(enemyRowContainer.transform.position.x, enemyRowContainer.transform.position.y - 0.25f);
                numHitsToGoDown = numRowsDefeteated + 1;
            }
        }

    }



    public void BonusEnemyDestroyed(int scoreToIncrease)
    {
        score += scoreToIncrease;
        scoreTextNum.text = score.ToString("D5");
        bonusEnemyActive = false;
        bonusEnemySpawnTime = Random.Range(15.0f, 40.0f);
    }



    public void EnemyRowDestroyed(bool continueDescending, int destroyedRowNumber)
    {
        numRowsDefeteated--;
        if (rowsHaveEnemsAlive[destroyedRowNumber] == true)
        {
            rowsHaveEnemsAlive[destroyedRowNumber] = false;
        }
        if (numRowsDefeteated > 0)
        {
            if (numHitsToGoDown > 0)
            {
                numHitsToGoDown = 0;
            }

            if (continueDescending)
            {
                maxPosition = false;

            }
        }
        else
        {
            round++;
            roundTextNum.text = round.ToString("D3");

            foreach (GameObject live in livesObject)
            {
                if (live != null)
                {
                    live.GetComponent<LivesScript>().ChangeSprite();
                }

            }

            musicAudioSource.Stop();
            effectsAudioSource.PlayOneShot(changeClip);
            levelMusicWait = changeClip.length;
            levelMusicStarted = false;
            if (blackBackgroundBool == false)
            {
                mainCamera.backgroundColor = blackBackground;
                scoreTextNum.color = GrayTextColor;
                scoreText.color = GrayTextColor;
                roundTextNum.color = GrayTextColor;
                roundText.color = GrayTextColor;
                blackBackgroundBool = true;

            }
            else if (blackBackgroundBool == true)
            {
                mainCamera.backgroundColor = blueBackground;
                scoreTextNum.color = OrangeTextColor;
                scoreText.color = OrangeTextColor;
                roundTextNum.color = OrangeTextColor;
                roundText.color = OrangeTextColor;
                blackBackgroundBool = false;
            }


            if (initialEnemySpeed < 8.4f)
            {
                initialEnemySpeed += 0.2f;
            }

            EnemySpeed = initialEnemySpeed;
            if (initialMaxTimeShoot > 2.5f)
            {
                initialMaxTimeShoot -= 0.5f;
            }
            if (initialMinTimeShoot > 1.25f)
            {
                initialMinTimeShoot -= 0.25f;
            }

            maxTimeShoot = initialMaxTimeShoot;
            minTimeShoot = initialMinTimeShoot;
            numRowsDefeteated = enemyRows.Length;
            animSpeed = animInitialSpeed;
            numHitsToGoDown = 8;
            maxPosition = false;
            speedAugmented = false;
            playerScript.ChangePlayerSprites();
            GameObject[] bullet = GameObject.FindGameObjectsWithTag("Bullet");
            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            if (bullet.Length > 0)
            {
                foreach (GameObject bull in bullet)
                {
                    Destroy(bull);
                }
            }
            if (enemyBullets.Length > 0)
            {
                foreach (GameObject enemBull in enemyBullets)
                {
                    Destroy(enemBull);
                }
            }
            if (bonusEnemyActive == true)
            {
                if (bonusEnemySpawned != null)
                {
                    bonusEnemySpawned.GetComponent<BonusEnemyScript>().DestroyBonusEnem();
                }
            }
            foreach (GameObject def in defenses)
            {
                def.GetComponent<DefenseScript>().RestartDefense();
            }
            for (int i = 0; i < enemyRows.Length; i++)
            {
                rowsHaveEnemsAlive[i] = true;
                enemyRows[i].GetComponent<EnemyRowScript>().RestartRow();
            }
            enemyRowContScript.RestartContainer();
            playerScript.DisableShoot();

        }
    }







    public void AccSpeedPosReached()
    {
        if (speedAugmented == true)
        {
            speedAugmented = true;
            EnemySpeed += 0.3f;
            animSpeed += 2;
        }

    }



    public void AccShootingPosReached()
    {
        if (maxPosition == false)
        {
            maxPosition = true;
            minTimeShoot = 0.1f;
            maxTimeShoot = 0.5f;
        }
    }



    public void RevivePlayer()
    {
        musicAudioSource.Play();
        playerScript.PlayerRevive();
        enemyRowContScript.RestartMovement();
        enemiesCanShoot = true;
    }



    public bool GetEnemiesCanShoot()
    {
        return enemiesCanShoot;
    }



    public void LifeLost()
    {

        if (lives == 0)
        {
            PlayerPrefs.SetInt("PlayerScore", score);
            SceneManager.LoadScene("GameOverScene");
        }
        else
        {
            playerDiedBool = true;
            playerDied = 2.2f;
            musicAudioSource.Pause();
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            enemiesCanShoot = false;
            numHitsToGoDown = 10;
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }

            foreach (GameObject enemyBullet in enemyBullets)
            {
                Destroy(enemyBullet);
            }
            enemyRowContScript.StopMovement();
            if (bonusEnemyActive == true)
            {
                if (bonusEnemySpawned != null)
                {
                    bonusEnemySpawned.GetComponent<BonusEnemyScript>().DestroyBonusEnem();
                }
            }
            lives--;
            livesObject[lives].GetComponent<Animator>().enabled = true;
            if (enemyRowContainer.transform.position.x < 0)
            {
                livesObject[lives].GetComponent<Animator>().SetBool("MoveLeft", false);
            }
            else
            {
                livesObject[lives].GetComponent<Animator>().SetBool("MoveLeft", true);
            }

        }

    }
    public float GetAnimationSpeed()
    {
        return animSpeed;
    }
}
