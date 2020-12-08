using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;// объект самого меню pause
    public GameObject RestartMenu;// объект самого меню restart
    public GameObject healfImage;// полоска жизней

    private bool pauseBool = false;


    void Start()
    {
        Cursor.visible = false;

        // задаем размер и расположение полоски жизней и меню паузы, пропорционально размеру экрана
        pauseMenu.transform.localScale = new Vector2(gameObject.GetComponent<RectTransform>().rect.width / 1326f, gameObject.GetComponent<RectTransform>().rect.height / 694f);
        pauseMenu.transform.position = new Vector2(gameObject.GetComponent<RectTransform>().rect.width / 2, gameObject.GetComponent<RectTransform>().rect.height / 2);


        healfImage.transform.localScale = new Vector2(gameObject.GetComponent<RectTransform>().rect.width / 1326f, gameObject.GetComponent<RectTransform>().rect.height / 694f);
        healfImage.transform.position= new Vector2((healfImage.GetComponent<RectTransform>().rect.width / 10f * healfImage.transform.localScale.x) + (healfImage.GetComponent<RectTransform>().rect.width / 2f * healfImage.transform.localScale.x), healfImage.GetComponent<RectTransform>().rect.height *3 * healfImage.transform.localScale.y);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//если нажат Esc
        {
            if (!pauseBool)//если игра не на паузе
            {
                OnPausePressed();// ставим на паузу
            }
            else//если игра  на паузе
            {
                OffPausePressed();// снимаем с паузы
            }

        }

        if (Input.GetKeyDown(KeyCode.R))//если нажат R идет рестарт уровня
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//пере загружаем данную сцену
            Time.timeScale = 1;
        }
    }

    public void OnRestartPressed()//при смерти включение панели рестарта
    {
        Cursor.visible = true;
        RestartMenu.SetActive(true);
        Time.timeScale = 0.5f;// останавливаем время
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//перезагружаем данную сцену
        Time.timeScale = 1;// останавливаем время
    }

    private void OnPausePressed()//включение паузы
    {
        Cursor.visible = true;
        pauseBool = true;// объявляем, что игра на паузе
        pauseMenu.SetActive(true);//активируем меню паузы
        Time.timeScale = 0;// останавливаем время
    }

    public void OffPausePressed()// выключение паузы
    {
        Cursor.visible = false;
        pauseBool = false;// объявляем, что игра больше не на паузе
        pauseMenu.SetActive(false);//скрываем меню паузы
        Time.timeScale = 1;// востанавливаем ход времени
    }

    public void ExitMenuPressed()
    {
        SceneManager.LoadScene(0);//запускаем сцену под индексом 0 ( меню )
        Time.timeScale = 1;// востанавливаем ход времени
    }

    public void GoNextLVL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
