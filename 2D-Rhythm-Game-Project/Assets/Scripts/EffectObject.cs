using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    //how long the effect last for
    public float lifetime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //After it activates for 1 second, destroy it
        Destroy(gameObject, lifetime);
    }
}
