using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesScript : MonoBehaviour
{
    private GameObject sceneController;
    private SceneControllerScript scScript;
    private SpriteRenderer liveRenderer;
    private bool blackBackground;
    [SerializeField] private Sprite orangeSprite;
    [SerializeField] private Sprite blackSprite;
    private float waitLiveDespawn;



    // Start is called before the first frame update
    void Start()
    {
        liveRenderer = gameObject.GetComponent<SpriteRenderer>();
        sceneController = GameObject.FindGameObjectWithTag("SceneController");
        scScript = sceneController.GetComponent<SceneControllerScript>();
        blackBackground = false;
        waitLiveDespawn = -1.0f;
    }


    // Update is called once per frame
    void Update()
    {
        if (waitLiveDespawn >= -0.5f)
        {
            gameObject.transform.position = new Vector2(0, 0);
            waitLiveDespawn -= Time.deltaTime;
            if (waitLiveDespawn <= 0)
            {
                Destroy(gameObject);
            }
        }
    }



    public void RevivePlayer()
    {
        scScript.RevivePlayer();
        waitLiveDespawn = 0.025f;
    }



    public void ChangeSprite()
    {
        if (blackBackground == true)
        {
            blackBackground = false;
            liveRenderer.sprite = orangeSprite;
        }
        else if (blackBackground == false)
        {
            blackBackground = true;
            liveRenderer.sprite = blackSprite;
        }
    }
}
