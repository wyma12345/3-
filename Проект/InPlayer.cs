using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InPlayer : MonoBehaviour
{

    #region Настраиваемые характеристики
    [Header("Cкорость ходьбы")]
    public float maxWalkSpead = 10f;
    
    [Header("Коэф нанесения дамага")]
    public int kofDamage;

    [Header("Начальное Здоровье")]
    public int maxHealf;

    [Header("Объект, задающий место спавна пули")]
    public GameObject pBullet;//Объект, задающий место спавна "пули"

    [Header("Объект, на котором висит Animator, отвечающий за оружие")]
    public GameObject shelder;//Объект, на котором "висит" Animator, отвечающий за оружие

    [Header("префаб пули")]
    public GameObject bullet;//префаб пули

    [Header("Урон от топора")]
    public int axZoneDamag;

    [Header("Урон от шипов")]
    public int thonseZoneDamag;

    [Header("Картинка жизней")]
    [SerializeField]
    Image healmage;


    #endregion

    #region Переменные
    private GameObject convas;

    private int healf;

    readonly string[] trueJumpTag= {"Ground","Vrag", "Object" };//теги с котором разрешено взаимодейтивие прыжка

    private float move=1;//показывает направление(если игрок мертв то  должен быть равен 0)
    #endregion

    #region Флаги
    
    public bool stopMovingFlag=true;//отключить передвижение

    bool replaceFlag = true;//флаг, контролирующий завершение перезарядки

    private sbyte jumpFlag =0;//флаг, разрешающий прыжок

    bool facingRight = true;//поворот в право

    bool DamageFlag = true;//поворот в право
    #endregion

    /// <summary>
    /// При запуске скрипта
    /// </summary>
    void Start()
    {
        healf = maxHealf;// заполняем шкалу здоровья
        transform.localScale = new Vector2(0.1f, 0.1f);
        StartCoroutine(StartTransport());
        convas = GameObject.FindGameObjectWithTag("Convas");
    }

    /// <summary>
    /// Обновление происходит каждый кадр
    /// </summary>
    void Update()
    {
        #region Атака
        if (Input.GetKeyDown(KeyCode.Space))//если нажат пробел
        {
            shelder.GetComponent<Animator>().SetBool("shootBool", true);//запускаем анимацию увеличения пули
        }

        if (Input.GetKeyUp(KeyCode.Space)&& replaceFlag)// Если пробел отпущен, и перезарядка завершена
        {
            GameObject Bullet = Instantiate(bullet, pBullet.transform.position, pBullet.transform.rotation);//создаем копию префаба "пули" на месте обозначенном pBullet
            
            Bullet.transform.localScale = new Vector2(pBullet.transform.localScale.x, pBullet.transform.localScale.y);//задаем ему размер, соответствующий размеру пули в анимации "зарядки"


            Bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(facingRight ? 100f : -100f, Bullet.GetComponent<Rigidbody2D>().velocity.y);//задаем "пуле скорость, с которой она будет двигаться"
            
            Bullet.GetComponent<InBullet>().damage = kofDamage * (int)(pBullet.transform.localScale.x* pBullet.transform.localScale.x * pBullet.transform.localScale.x);// увеличивае урон, передываемый пуле
            
            shelder.GetComponent<Animator>().SetBool("shootBool", false);//запускаем анимацию перезарядки
            
            replaceFlag = false;//поднимаем флаг начала перезарядки
            StartCoroutine(Replace());//запускаем карутину отмеряющую время перезарядки
        }
        #endregion

        #region Прекращение движения
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || stopMovingFlag)//в момент отпускания кнопок движения
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);// останавливаем ГГ
            GetComponent<Animator>().SetBool("animRunBool", false);//прекращаем анимацию бега
        }
        #endregion

        #region Начало прыжка
        if (Input.GetKeyDown(KeyCode.W)&&jumpFlag>0&& !stopMovingFlag)//если нажата кнопка и разрешен прыжок
        {
            //jumpFlag=0;// запрещаем прыжок
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 20f);// задаем силу прыжка
            GetComponent<Animator>().SetBool("animJumpBool", true);// запускаем анимацию начала прыжка
        }
        #endregion

        #region Отслеживание падения
        if (GetComponent<Rigidbody2D>().velocity.y < 0)//если ГГ падает
        {
            GetComponent<Animator>().SetFloat("SpeedAnim", -1);//воспроизводим анимация прыжка назад
        }
        else
        {
            GetComponent<Animator>().SetFloat("SpeedAnim", 1);//если нет падения, то ставим скорость нормальной
        }
        #endregion
    }

    private void FixedUpdate()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))&&!stopMovingFlag)//если зажата клавиша движения
        {
            move = Input.GetAxis("Horizontal");
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxWalkSpead, GetComponent<Rigidbody2D>().velocity.y);//задаем ГГ скорость и направление для движения
            GetComponent<Animator>().SetBool("animRunBool", true);//запускаем анимацию бега

            #region Отслеживание поворота
            if (move > 0 /*&& !Die*/&& !facingRight)//если ГГ двигается направо и повернут налево
            {
                Flip();
            }
            else if (move < 0 /*&& !Die*/&& facingRight)//елси ГГ двигается налево и повернут направо
            {
                Flip();
            }
            #endregion
        }
    }

    private void OnTriggerEnter2D(Collider2D other)//если в тригер входит другой collider
    {
        if (trueJumpTag.Contains(other.gameObject.tag) && !other.isTrigger)//если это земля
        {
            jumpFlag++;//разрешаем выполнить прыжок
            GetComponent<Animator>().SetBool("animJumpBool", false);//воспроизводим выход из анимации прыжка
        }

        if (other.gameObject.tag == "Ax"&& DamageFlag)//если это шипы
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(facingRight ? (-20f*Mathf.Cos(other.transform.rotation.z)) : ( 20f * Mathf.Cos(other.transform.rotation.z)), Mathf.Abs(50f * Mathf.Sin(other.transform.rotation.z)));// откидываем от шипов
            Damage(axZoneDamag);// наносим урон
            DamageFlag = false;// на время запрещаем наносить урон
            StartCoroutine(DamageTrue());//отчитывыем время, во время которого нельзя наносить урон
        }

        if (other.gameObject.tag == "Thonse" && DamageFlag)//если это шипы
        {
            jumpFlag=0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 15f);// откидываем от шипов
            Damage(thonseZoneDamag);// наносим урон
            DamageFlag = false;// на время запрещаем наносить урон
            StartCoroutine(DamageTrue());//отчитывыем время, во время которого нельзя наносить урон
        }

        if (other.gameObject.tag == "Portal")//если это финиш
        {
            StartCoroutine(FinishTransport());
            stopMovingFlag = true;
        }

        if (other.gameObject.tag == "Lift" && other.isTrigger)//если это финиш
        {
            stopMovingFlag = true;
            GetComponent<Animator>().SetBool("animJumpBool", false);//воспроизводим выход из анимации прыжка
        }

        if (other.gameObject.tag == "DieZone")//если это зона смерти
        {
            Die();
        }

        if (other.gameObject.tag == "Heart"&& healf<maxHealf)//если это жизнь
        {
            healf = maxHealf;//востанавливаем здоровье
            UpdateCrossBar();//обнавляем полоску жизней
            Destroy(other.gameObject);//уничтожаем аптечку
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (trueJumpTag.Contains(other.gameObject.tag)&& !other.isTrigger)//если это земля(если оторвались от земля, то прыгать нельзя)
        {
            jumpFlag--;//запрещаем выполнять прыжок
        }

        if (other.gameObject.tag == "Lift"&&other.isTrigger)//если это финиш
        {
            stopMovingFlag = false;
        }
    }

    /// <summary>
    /// Переворот ГГ
    /// </summary>
    void Flip()//переворот ГГ
    {
        facingRight = !facingRight;//флаг поворота
        Vector2 theScale = transform.localScale;//сохраняем размеры
        theScale.x *= -1;//отражаем объект
        transform.localScale = theScale;//задаем новый размер(отраженный по х)
    }

    /// <summary>
    /// Получение урона
    /// </summary>
    /// <param name="_damag"></param>
    public void Damage(int _damag)
    {
        healf -= _damag;//наносим урон
        if (healf <= 0)//если жизни закончились
            Die();//функция начала смерти

        UpdateCrossBar();//обновляем полоску жизней
        
        #region Смена цвета
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())//перебираем все дочерние объекты
            child.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0.9f, 0.5f, 0.5f);//и задаем им красноватый оттенок

        StartCoroutine(ColorTransformBack());//ззапускаем картутину возвращение цвета
        #endregion
    }
    
    /// <summary>
    /// Смерть
    /// </summary>
    void Die()
    {
        stopMovingFlag = true;
        GetComponent<Animator>().Play("Die");
    }

    public void EndDie()
    {
        convas.GetComponent<PauseMenu>().OnRestartPressed();
    }

    /// <summary>
    /// Обновление полоски жизней
    /// </summary>
    private void UpdateCrossBar()
    {
        healmage.fillAmount = (float)((float)healf / (float)maxHealf);//уменьшаем размер спрайта
        if (healf < 3 * maxHealf / 4)//если она < 3/4 красим ее в желтый цвет
            healmage.color = Color.yellow;
        else if (healf < maxHealf / 3)//если она < 1/3 красим ее в красный цвет
            healmage.color = Color.red;
            else
            healmage.color = Color.green;//иначе она зеленная (> 3/4)
    }

    /// <summary>
    /// Карутина задающ. время на перезарядку
    /// </summary>
    /// <returns></returns>
    IEnumerator Replace()   // карутина
    {
        yield return new WaitForSeconds(0.7f);//ждем 0.7 сек
        replaceFlag = true;//разрешаем повторный выстрел
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

    /// <summary>
    /// карутина, разрешающая дамаг(от объектов)
    /// </summary>
    /// <returns></returns>
    IEnumerator DamageTrue()   // карутина
    {
        yield return new WaitForSeconds(0.5f);//ждем 0.5 сек
        DamageFlag = true;//разрешаем нанесение урона
    }

    IEnumerator FinishTransport()   // карутина
    {
        yield return new WaitForFixedUpdate();
        transform.localScale = new Vector2(transform.localScale.x- 0.005f, transform.localScale.y- 0.005f);
        if (transform.localScale.x < 0.1)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            convas.GetComponent<PauseMenu>().GoNextLVL();
        }
        else
            StartCoroutine(FinishTransport());
    }

    IEnumerator StartTransport()   // карутина
    {
        yield return new WaitForFixedUpdate();
        transform.localScale = new Vector2(transform.localScale.x + 0.02f, transform.localScale.y + 0.02f);
        if (transform.localScale.x < 1f)
            StartCoroutine(StartTransport());
        else
        {
            transform.localScale = new Vector2(1f, 1f);
            stopMovingFlag = false;
        }

    }
}
