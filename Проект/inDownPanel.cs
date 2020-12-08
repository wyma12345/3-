using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inDownPanel : MonoBehaviour
{
    [Header("Время до падения панели")]
    public float waitDownTime;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)//если коллайдер соприкасется с объектом
    {
        if (other.tag == "Player")//если это игрок
        {
            StartCoroutine(DownTime());//запускаем таймер падения панели
        }

        if (other.tag == "DieZone")//если это дестрой зона
        {
            Destroy(gameObject);//уничтожаем панель
        }
    }


    IEnumerator DownTime()   // время до падения панели
    {
        yield return new WaitForSeconds(waitDownTime);//засекаем 5 сек
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;//убираем все заморозки Rigidbody2D
        GetComponent<Rigidbody2D>().gravityScale = 3f;
        StartCoroutine(DestroyTime());//запускаем таймер самоуничтожения
    }

    IEnumerator DestroyTime()   // время до уничтожения панели
    {
        yield return new WaitForSeconds(7f);//засекаем 7 сек
        Destroy(gameObject);//самоуничтожаем объект
    }
}
