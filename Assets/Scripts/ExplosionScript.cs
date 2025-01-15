using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private SpriteRenderer explosionRenderer;
    private AudioSource source;
    [SerializeField] private AudioClip clip;
    private float clipLength;



    // Start is called before the first frame update
    void Start()
    {
        explosionRenderer = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        source.clip = clip;
        clipLength = clip.length;
        source.Play();
    }



    // Update is called once per frame
    void Update()
    {
        clipLength -= Time.deltaTime;
        if (clipLength < 0)
        {
            Destroy(gameObject);
        }
    }



    public void ObjectExploded()
    {
        explosionRenderer.enabled = false;
    }
}
