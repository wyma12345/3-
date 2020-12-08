using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Панель меню")]
    public GameObject menuImage;
    [Header("Панель настроек")]
    public GameObject settingsImage;

    private bool activeFullScreen = true;

    void Start()
    {
        // задаем размер и распложение меню, пропорционально размеру экрана
        menuImage.transform.localScale = new Vector2(gameObject.GetComponent<RectTransform>().rect.width / 1326f, gameObject.GetComponent<RectTransform>().rect.width / 1326f);
        menuImage.transform.position = new Vector2(gameObject.GetComponent<RectTransform>().rect.width / 4, gameObject.GetComponent<RectTransform>().rect.height / 2);
    }

    void Update()
    {
        
    }

    public void PlayPressed()
    {
        //SceneManager.LoadScene("Scene_1");//запускаем сцену под именем 1
        SceneManager.LoadScene(1);//запускаем сцену под индексом 1
    }

    public void ExitPressed()
    {
        Application.Quit();//выход из приложения
    }

    public void OpenSettingsPressed()// открытие настроек
    {
        menuImage.SetActive(false);//убираем основное меню
        settingsImage.SetActive(true);// активируем панель настроек
    }
    public void CloseSettingsPressed()// закрытие настроек
    {
        menuImage.SetActive(true);//активируем основное меню
        settingsImage.SetActive(false);// убираем панель настроек
    }

    public void ActivateFullScreen()// полноэкранный режим
    {
        activeFullScreen = !activeFullScreen;//меняем флаг полноэкранного режима
        Screen.fullScreen = activeFullScreen;//в зависимости от флага, выбираем опцию экрана
    }

}
