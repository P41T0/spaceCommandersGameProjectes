using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusEnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    private int touchScore;
    private GameObject sceneController;
    private SceneControllerScript scScript;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject noSoundExplosion;


    void Start()
    {
        touchScore = 100;
        sceneController = GameObject.FindGameObjectWithTag("SceneController");
        scScript = sceneController.GetComponent<SceneControllerScript>();
    }


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            scScript.BonusEnemyDestroyed(touchScore);
            Destroy(gameObject);
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RightWall"))
        {
            scScript.BonusEnemyDestroyed(0);
            Destroy(gameObject);
        }
    }



    public void DestroyBonusEnem()
    {
        Instantiate(noSoundExplosion, transform.position, Quaternion.identity);
        scScript.BonusEnemyDestroyed(0);
        Destroy(gameObject);
    }
}
