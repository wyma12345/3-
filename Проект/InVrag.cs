using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InVrag : MonoBehaviour
{
    #region Настраиваемые переменные
    [Header("Здоровье врага")]
    [SerializeField]
    private int healf;

    [Header("Урон, наносимый врагом")]
    public int damage;

    [Header("Префаб пули")]
    [SerializeField]
    private GameObject bullet;

    [Header("Позиция 2-х мест спавна пуль врага")]
    [SerializeField]
    private Transform posBullet1;
    [SerializeField]
    private Transform posBullet2;

    [Header("префаб метвого врага")]
    [SerializeField]
    private GameObject dieVrag;//префаб картинки метвого врага(выставляется после смерти)



    [Header("Префаб выпадающих предметов")]
    public GameObject[] bonuses= new GameObject[1];

    [Header("Процент выпадения предмета")]
    public float prosBonus;
    #endregion

    #region Флаги
    private bool atachFlag1 = true;//флаг, разрешающий атаку 1 пушки(ближней)
    private bool atachFlag2 = false;//флаг, разрешающий атаку 2 пушки(дальней)
    bool facingRight = false;//поворот в право
    bool stay = true;//Дивжется ли вграг
    #endregion

    #region Переменные
    private float move = -1f;//показывает направление(если игрок мертв то  должен быть равен 0)
    GameObject player;
    #endregion

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");// задаем для объекта player объект с тегом Player(игрока)
    }

    void Update()
    {

    }

    /// <summary>
    /// получение урона
    /// </summary>
    /// <param name="_damag"></param>
    public void Damage(int _damag)
    {
        healf -= _damag;//уменьшаем жизни
        if (healf <= 0)  
            Die(); //если жизни закончились, враг мертв    

        #region Изменение цвета
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())//перебираем все дочерние объекты
            child.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0.9f, 0.5f, 0.5f);//и задаем им красноватый оттенок

        StartCoroutine(ColorTransformBack());//ззапускаем картутину возвращение цвета
        #endregion

        #region Поворот в сторону игрока
        if (player.transform.position.x <= transform.position.x)//поворачиваем в сторону игрока
        {
            if (facingRight)
                Flip();
        }
        else
        {
            if (!facingRight)
                Flip();
        }
        #endregion

    }


    /// <summary>
    /// Смерть врага
    /// </summary>
    private void Die()
    {
        move = 0f;//запрещаем движение
        gameObject.GetComponent<Animator>().Play("Die");//проигрываем анимацию смерти(в конце которой установлен event на метод EndDie)
    }

    /// <summary>
    /// Уничтожение объекта
    /// </summary>
    void EndDie()
    {
        for(int i=0; i < bonuses.Length; i++)//перебираем все элементы в массиве бонусов
        {
            if(Random.Range(0f, 99f)< prosBonus)//если вероятность совпала с i бонусом
            {
                Instantiate(bonuses[i], transform.position, transform.rotation);//спавним этот бонус
                break;//прекращаем перебор
            }
        }

        Instantiate(dieVrag, transform.position, transform.rotation);//выставляем статичную картинку мертвого врага
        Destroy(gameObject);//уничтожаем объект
    }

    private void FixedUpdate()
    {
        try
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 60f && move != 0f)// если ГГ находится в зоне поражения и не мертв
            {
                RaycastHit2D hit = Physics2D.Raycast(posBullet1.position, (facingRight ? Vector2.right : Vector2.left), 60f, 7);//кидаем рейкаст по напровлению взгляда
                if (hit.collider.gameObject == player)//если он попал в игрока
                {
                    stay = false;//запрещаем двигаться дальше
                    GetComponent<Animator>().SetBool("animRunVrag", false);//прекращаем анимацию бега

                    if (atachFlag1)//если разрешена атака 1 пушки
                    {
                        GameObject Bullet1 = Instantiate(bullet, posBullet1.position, posBullet1.rotation);//создаем копию префаба "пули" на месте обозначенном posBullet1
                        Bullet1.GetComponent<Rigidbody2D>().velocity = new Vector2(move * 20f, Bullet1.GetComponent<Rigidbody2D>().velocity.y);//задаем "пуле скорость, с которой она будет двигаться"
                        Bullet1.GetComponent<InBullet>().damage = damage;//Передаем пуле урон
                        atachFlag1 = false;//запрещаем дальше атаковать
                        StartCoroutine(WaitAtach1());//запускаем перезарядку 1 пушки
                    }

                    if (atachFlag2)//если разрешена атака 2 пушки
                    {
                        GameObject Bullet2 = Instantiate(bullet, posBullet2.position, posBullet2.rotation);//создаем копию префаба "пули" на месте обозначенном posBullet2
                        Bullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(move * 20f, Bullet2.GetComponent<Rigidbody2D>().velocity.y);//задаем "пуле скорость, с которой она будет двигаться"
                        Bullet2.GetComponent<InBullet>().damage = damage;//Передаем пуле урон
                        atachFlag2 = false;//запрещаем дальше атаковать
                    }

                    GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);//останавливаем врага
                }       
            }
            else
                stay = true;//если игрока нет в зоне, продолжаем двигаться
        }
        catch
        {
        }


        if (stay)//если разрешено движение
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * 5f, GetComponent<Rigidbody2D>().velocity.y);//задаем ГГ скорость и направление для движения
            GetComponent<Animator>().SetBool("animRunVrag", true);//запускаем анимацию бега

            #region Отслеживание поворота
            if (move > 0 /*&& !Die*/&& !facingRight)//если враг двигается направо и повернут налево
            {
                Flip();
            }
            else if (move < 0 /*&& !Die*/&& facingRight)//елси враг двигается налево и повернут направо
            {
                Flip();
            }
            #endregion
        }

    }

    readonly string[] tagBadName = { "Bullet", "Player", "Untagged" };// Теги предметов, не требующие разворота

    private void OnTriggerEnter2D(Collider2D other)//если в тригер входит другой collider
    {
        if (!tagBadName.Contains(other.gameObject.tag))//если тег этого объекта вынуждает повернуться
        {
            Flip();//выполняем разворот
        }
    }

    public void Flip()//разворот врага
    {
        facingRight = !facingRight;//флаг поворота
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        move *= -1;
    }

    /// <summary>
    /// Перезарядка для 1 пушки
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitAtach1()   // карутина
    {
        StartCoroutine(WaitAtach2());//запускаем ожидание атаки 2 пушки
        yield return new WaitForSeconds(4f);//ждем 4 сек
        atachFlag1 = true;//разрешаем атаку 1 пушки
    }
    
    /// <summary>
    /// Перезарядка для 2 пушки
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitAtach2()   // карутина
    {
        yield return new WaitForSeconds(2f);//ждем 2 сек
        atachFlag2 = true;//разрешаем атаку 2 пушки
    }


    /// <summary>
    /// Возвращает цвет через 100мс
    /// </summary>
    /// <returns></returns>
    IEnumerator ColorTransformBack()   // карутина
    {
        yield return new WaitForSeconds(0.1f);//ждем 0.1 сек
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())//перебираем все дочерние объекты
            child.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f);//и возвращаем им стандартный цвет

    }
}