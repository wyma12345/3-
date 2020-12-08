using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTurel : MonoBehaviour
{
    [Header("Вращ. часть турели")]
    public Transform turTransf;
    [Header("Позиция из которой выл. пуля")]
    public Transform firePosition;
    [Header("Префаб пули")]
    public GameObject bullet;
    [Header("Время перезарядки")]
    public float timeAtach=4f;
    [Header("Урон")]
    public int damage;
    [Header("Cкорость вращения")]
    public float rotationSpeed = 1F;
    [Header("Мертвая зона вращения (чтобы турель не дергалась при x=0)")]
    public float deadZone = 0.1F;//Мертвая зона вращения (чтобы турель не дергалась при x=0)
    [Header("Здоровье")]
    public int healf;
    [Header("префаб мертвой турели")]
    public GameObject dieTurel;

    private bool attack = false;
    private float rotateDirection = 0;//направление вращения ( "0" - не вращать, "1" - вправо и "-1" - влево)
    private Transform player;
    void Start()
    {
        StartCoroutine(SearchPlayer());
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//если это collider игрока
        {
            attack = true;//разрешаем отслеживание
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")//если это collider игрока
        {
            attack = false;//запрещаем атаку
        }
    }

    void LateUpdate()
    {
        if (attack)
        {
            if (turTransf.InverseTransformPoint(player.position).x > deadZone / 2)//если ГГ левее турели, с учетом поворота, вращаем против часовой стрелки
                rotateDirection = -1F;
            else if (turTransf.InverseTransformPoint(player.position).x < -deadZone / 2)//если ГГ правее турели, с учетом поворота, вращаем по часовой стрелки
                rotateDirection = 1F;
            else
            {
                if (turTransf.InverseTransformPoint(player.position).y < 0) 
                    rotateDirection = 1F;
                else 
                    rotateDirection = 0;
            }

            turTransf.rotation *= Quaternion.Euler(0, 0, rotationSpeed * rotateDirection);//вращаем турель
        }
    }


    IEnumerator SearchPlayer()   // карутина
    {
        yield return new WaitForSeconds(timeAtach);//ждем 2 сек
        if(attack)
        {
            RaycastHit2D hit = Physics2D.Raycast(firePosition.position, firePosition.up, 100f, 9);//кидаем рейкаст по напровлению взгляда
            if (hit.collider != null && hit.collider.gameObject.tag == "Player")//если он попал в игрока
            {
                GameObject bulletCopy = Instantiate(bullet, firePosition.position, firePosition.rotation);//создаем копию префаба "пули" на месте обозначенном posBullet1
                bulletCopy.GetComponent<Rigidbody2D>().velocity = firePosition.up * 20f; //задаем "пуле скорость, с которой она будет двигаться"
                bulletCopy.GetComponent<InBullet>().damage = damage;//Передаем пуле урон            
            }
        }
        StartCoroutine(SearchPlayer());
    }

    public void Damage(int _damage)
    {
        healf -= _damage;
        if (healf <= 0)
        {
            attack = false;
            StartCoroutine(Die());   
        }

    }

    IEnumerator Die()   // карутина
    {
        yield return new WaitForEndOfFrame();//ждем конца кадра
        turTransf.rotation *= Quaternion.Euler(0, 0, 0.4f);//вращаем турель

        if(turTransf.rotation.eulerAngles.z >= 179)
        {
            Instantiate(dieTurel, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        StartCoroutine(Die());
    }


}
