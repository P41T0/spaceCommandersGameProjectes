using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyContainerScript : MonoBehaviour
{
    private float speed;
    private float actualSpeed;
    [SerializeField] private Vector2 initialPos;
    private bool moving;
    private SceneControllerScript scscript;
    private GameObject sceneController;
    private bool movingRight;



    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        sceneController = GameObject.FindGameObjectWithTag("SceneController");
        scscript = sceneController.GetComponent<SceneControllerScript>();
        speed = scscript.GetSpeed();
        actualSpeed = speed;
        movingRight = true;
        moving = true;
        
    }



    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + actualSpeed * Time.deltaTime, transform.position.y);
        }

    }


    public void ChangeDirectionToRight()
    {
        if (movingRight == false)
        {
            movingRight = true;
            speed = scscript.GetSpeed();
            actualSpeed = speed;
            scscript.LateralHit();
        }

    }



    public void ChangeDirectionToLeft()
    {
        if (movingRight == true)
        {
            movingRight = false;
            speed = scscript.GetSpeed();
            actualSpeed = -speed;
            scscript.LateralHit();
        }
    }



    public void RestartContainer()
    {
        gameObject.transform.position = initialPos;
        speed = scscript.GetSpeed();
        actualSpeed = speed;

    }



    public void StopMovement()
    {
        moving = false;
    }



    public void RestartMovement()
    {
        moving = true;
    }
}
