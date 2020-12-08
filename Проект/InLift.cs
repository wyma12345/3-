using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLift : MonoBehaviour
{
    public Transform NewRopePosition;
    public Transform EndRopePosition;
    public Transform NewLiftPltformPosition;
    public Transform EndLiftPltformPosition;
    public GameObject Rope;
    public GameObject OffCollision;
    public float speedUpper;
    void Start()
    {
        
    }
    bool st = false;
    void Update()
    {
        //if (st)
        //{
        //    transform.position = new Vector2(transform.position.x, transform.position.y + speedUpper);
        //    Rope.GetComponent<SpriteRenderer>().size = new Vector2(Rope.GetComponent<SpriteRenderer>().size.x, (EndRopePosition.position.y - NewRopePosition.position.y));
        //    Rope.transform.position = new Vector2(Rope.transform.position.x, Rope.transform.position.y + speedUpper / 2);

        //    if (NewLiftPltformPosition.position.y >= EndLiftPltformPosition.position.y)
        //    {
        //        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //        OffCollision.SetActive(true);
        //        st = false;
        //    }
        //}
    }

    private void FixedUpdate()
    {
        if (st)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speedUpper);
            Rope.GetComponent<SpriteRenderer>().size = new Vector2(Rope.GetComponent<SpriteRenderer>().size.x, (EndRopePosition.position.y - NewRopePosition.position.y));
            Rope.transform.position = new Vector2(Rope.transform.position.x, Rope.transform.position.y + speedUpper / 2);

            if (NewLiftPltformPosition.position.y >= EndLiftPltformPosition.position.y)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                OffCollision.SetActive(true);
                st = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //StartCoroutine(Upping());
            st = true;
        }
    }

    //IEnumerator Upping()  // карутина
    //{
    //    yield return new WaitForFixedUpdate();
    //    transform.position = new Vector2(transform.position.x, transform.position.y + speedUpper);
    //    Rope.GetComponent<SpriteRenderer>().size = new Vector2(Rope.GetComponent<SpriteRenderer>().size.x, (EndRopePosition.position.y - NewRopePosition.position.y));
    //    Rope.transform.position = new Vector2(Rope.transform.position.x, Rope.transform.position.y + speedUpper / 2);

    //    if (NewLiftPltformPosition.position.y < EndLiftPltformPosition.position.y)
    //        StartCoroutine(Upping());
    //    else
    //    {
    //        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    //        OffCollision.SetActive(true);
    //    }

    //}
}
