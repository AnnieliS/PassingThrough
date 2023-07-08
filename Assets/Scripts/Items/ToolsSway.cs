using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsSway : MonoBehaviour
{

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("offset", Random.Range(0,1f));
        anim.SetFloat("multiply", Random.Range(0.7f, 1.2f));
    }
}
