using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DefenseScript : MonoBehaviour
{
    private int numLives;
    [SerializeField] private Sprite[] orangeTowerSprites;
    [SerializeField] private Sprite[] blackTowerSprites;
    private SpriteRenderer defenseRenderer;
    private bool blackBackground;



    // Start is called before the first frame update
    void Start()
    {
        numLives = 5;
        defenseRenderer = GetComponent<SpriteRenderer>();
        defenseRenderer.sprite = orangeTowerSprites[numLives];
        blackBackground = false;
    }


    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            numLives--;
            Destroy(collision.gameObject);
            if (numLives <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                if (blackBackground == false)
                {
                    defenseRenderer.sprite = orangeTowerSprites[numLives];
                }
                else if (blackBackground == true)
                {
                    defenseRenderer.sprite = blackTowerSprites[numLives];
                }

            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            numLives = 0;
            gameObject.SetActive(false);
        }
    }



    public void RestartDefense()
    {
        gameObject.SetActive(true);
        numLives = 5;

        if (blackBackground == false)
        {
            defenseRenderer.sprite = blackTowerSprites[numLives];
            blackBackground = true;
        }
        else if (blackBackground == true)
        {
            defenseRenderer.sprite = orangeTowerSprites[numLives];
            blackBackground = false;
        }
    }
}
