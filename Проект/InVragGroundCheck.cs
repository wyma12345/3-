using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InVragGroundCheck : MonoBehaviour
{
    private InVrag vrag;
    public sbyte nGround = 0;//контроль земли
    void Start()
    {
        vrag = gameObject.GetComponentInParent<InVrag>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            nGround++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            if (--nGround <= 0)
                vrag.Flip();
        }
    }

    void Update()
    {
        
    }
}
