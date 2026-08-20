using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class PlayerControllerScript : MonoBehaviour
{
    private static readonly int DarkBgHash = Animator.StringToHash("DarkBg");
    InputAction moveAction;
    InputAction shootAction;
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
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
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
            move = moveAction.ReadValue<Vector2>().x;
            if (Arcade.ac.Button("j1_Right"))
            {
                move += 1;
                if(move > 1)
                {
                    move = 1;
                }
            }
            if (Arcade.ac.Button("j1_Left"))
            {
                move -= 1;
                if (move < -1)
                {
                    move = -1;
                }
            }
            if (move != 0)
            {
                float newX = gameObject.transform.position.x + move * speed * Time.deltaTime;
                if (-4.75f < newX && newX < 4.75f)
                {
                    gameObject.transform.position = new Vector2(newX, gameObject.transform.position.y);
                }
            }




            if ((shootAction.ReadValue<float>() > 0 && shootAction.WasPressedThisFrame()) || Arcade.ac.ButtonDown("la") || Arcade.ac.ButtonDown("l2"))
            {
                if (shootWaitTime < 0)
                {
                    if (!blackBackground)
                    {
                        Instantiate(orangeBullet, gameObject.transform.position, Quaternion.identity);
                    }
                    else
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
            playerAnimator.SetBool(DarkBgHash, true);
        }
        else if (blackBackground == false)
        {
            playerAnimator.SetBool(DarkBgHash, false);
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
            playerAnimator.SetBool(DarkBgHash, false);
        }
        else if (blackBackground == false)
        {
            blackBackground = true;
            playerAnimator.SetBool(DarkBgHash, true);
        }
    }
}
