using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBullet : MonoBehaviour
{
    public int damage { get; set; }

    void Start()
    {
        StartCoroutine(DestroyBullet());//запускаем отсчет времени жизни пули
    }

    void Update()
    {

    }
    /// <summary>
    /// Если пуля входит в тригер
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Vrag")//если это враг
        {
            other.gameObject.GetComponent<InVrag>().Damage(damage);//наносим дамаг врагу через фун-ию
            Destroy(gameObject);//уничотжение пули
        }
        if (other.tag == "Player"&&other.isTrigger)//если это collider игрока
        {
            other.gameObject.GetComponent<InPlayer>().Damage(damage);//наносим дамаг игроку через фун-ию
            Destroy(gameObject);//уничотжение пули
        }

        if (other.tag == "Turel" && !other.isTrigger)//если это collider турели
        {
            other.gameObject.GetComponentInParent<InTurel>().Damage(damage);//наносим дамаг турели через фун-ию
            Destroy(gameObject);//уничотжение пули
        }

        if (!other.isTrigger)//если это другой collider
        {
            Destroy(gameObject);//уничотжение пули
        }


    }
/// <summary>
/// Корутина уничтожения пули, если она не во что не попала
/// </summary>
/// <returns></returns>
    IEnumerator DestroyBullet()   // дедлайн уничтожения "пули"
    {
        yield return new WaitForSeconds(5f);//засекаем 5 сек
        Destroy(gameObject);//уничтожаем объект(пулю)
    }
}
