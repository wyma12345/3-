using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inBox : MonoBehaviour
{
    [Header("Префаб объекта, который должен появится")]
    public GameObject obj;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    bool d = true;
    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if ((other.tag == "Bullet"|| other.tag == "Ax")&& d)//если это пуля или топор
        {
            d = false;
            if (obj != null)// если добавлен префаб
                Instantiate(obj, transform.position, transform.rotation);//добавляем элемент

            Die();
        }
    }

    void Die()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;//отключаем видимость объекта
        gameObject.GetComponent<SpriteRenderer>().enabled = false;//и колайдер

        for (int i = 0; i < transform.childCount; i++)//обращаемся ко всем дочерним объектам
        {
            transform.GetChild(i).gameObject.SetActive(true);//активируем
            transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3f, 3f), 5f);//задаем равндомное движение
            transform.GetChild(i).GetComponent<Rigidbody2D>().AddTorque(Random.Range(-50f, 50f));//и вращение
        }

        StartCoroutine(DieTime());//и удаляем эти объекты речерз некоторое время
    }


    int n=0;
    IEnumerator DieTime()  // карутина
    {
        yield return new WaitForSeconds(0.5f);//ждем 
        transform.GetChild(n++).gameObject.SetActive(false);//дезактивируем дочерние объекты
        if(n==8)//если это последний элемент, удаляем весь объект(box)
            Destroy(gameObject);
        StartCoroutine(DieTime());
    }


}
