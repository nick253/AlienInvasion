using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAnimationController : MonoBehaviour
{

    private Animator animator;
    private bool isAnimating = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAnimating)
        {
            animator.SetBool("isAnimating", true);
            isAnimating = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isAnimating)
        {
            animator.SetBool("isAnimating", false);
            isAnimating = false;
        }
    }
}
