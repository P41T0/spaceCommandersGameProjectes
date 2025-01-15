using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private float move;
    private AudioSource playerAudioSource;
    [SerializeField] private AudioClip playerShootClip;
    [SerializeField] private AudioClip playerDeadClip;
    [SerializeField] private GameObject orangeBullet;
    [SerializeField] private GameObject blackBullet;
    [SerializeField] private GameObject whiteExplosion;
    [SerializeField] private GameObject orangeExplosion;
    private float shootWaitTime;
    private bool alive;
    private float speed;
    private bool blackBackground;
    private GameObject sceneController;
    private Animator playerAnimator;
    private SceneControllerScript scScript;
    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        move = 0;
        shootWaitTime = 5.0f;
        speed = 6.5f;
        alive = true;
        playerAnimator = gameObject.GetComponent<Animator>();
        sceneController = GameObject.FindGameObjectWithTag("SceneController");
        scScript = sceneController.GetComponent<SceneControllerScript>();
        blackBackground = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (shootWaitTime >= 0)
        {
            shootWaitTime -= Time.deltaTime;
        }
        if (alive)
        {
            move = 0;
            if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow)) ||  Arcade.ac.Button("j1_Right"))
            {
                move += 1;
            }
            if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow)) || Arcade.ac.Button("j1_Left"))
            {
                move -= 1;
            }
            if (move != 0)
            {
                if (-4.75f < gameObject.transform.position.x + move * speed * Time.deltaTime && gameObject.transform.position.x + move * speed * Time.deltaTime < 4.75f)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x + move * speed * Time.deltaTime, gameObject.transform.position.y);
                }
            }




            if (Input.GetKeyDown(KeyCode.Space) || Arcade.ac.ButtonDown("l1") || Arcade.ac.ButtonDown("l2"))
            {
                if (shootWaitTime < 0)
                {

                    if (blackBackground == false)
                    {
                        Instantiate(orangeBullet, gameObject.transform.position, Quaternion.identity);
                    }
                    else if (blackBackground == true)
                    {
                        Instantiate(blackBullet, gameObject.transform.position, Quaternion.identity);
                    }
                    if (gameObject.activeSelf)
                    {
                        playerAudioSource.PlayOneShot(playerShootClip);
                    }
                    shootWaitTime = 0.4f;
                }

            }

        }

    }



    public void PlayerRevive()
    {
        
        gameObject.SetActive(true);
        if (blackBackground == true)
        {
            playerAnimator.SetBool("DarkBg", true);
        }
        else if (blackBackground == false)
        {
            playerAnimator.SetBool("DarkBg", false);
        }
        alive = true;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            if (blackBackground == true)
            {
                Instantiate(whiteExplosion, transform.position, transform.rotation);
            }
            else if (blackBackground == false)
            {
                Instantiate(orangeExplosion, transform.position, transform.rotation);
            }
            gameObject.SetActive(false);
            alive = false;
            gameObject.transform.position = new Vector2(0, transform.position.y);
            scScript.LifeLost();
        }
    }



    public void DisableShoot()
    {
        shootWaitTime = scScript.GetMinTimeShoot()+0.25f;
    }



    public void ChangePlayerSprites()
    {
        if (blackBackground == true)
        {
            blackBackground = false;
            playerAnimator.SetBool("DarkBg", false);
        }
        else if (blackBackground == false)
        {
            blackBackground = true;
            playerAnimator.SetBool("DarkBg", true);
        }
    }
}
