using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    private static string KEY_VOLUME = "Volumen";
    public AudioSource audioSourceBSO;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start(){
        RestoreVolume();
        GameObject.Find("ConfigPanel").SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void RestoreVolume(){
        if (PlayerPrefs.HasKey(KEY_VOLUME)){
            GameObject.Find("SliderVolume").GetComponent<Slider>().value=PlayerPrefs.GetFloat("Volumen");
        }
    }
    public void SaveAudioVolume(){
        PlayerPrefs.SetFloat(KEY_VOLUME,GameObject.Find("SliderVolume").GetComponent<Slider>().value);
        PlayerPrefs.Save();
    }

    public void ModifyVolume(){
        audioSourceBSO.volume=GameObject.Find("SliderVolume").GetComponent<Slider>().value;
}

}