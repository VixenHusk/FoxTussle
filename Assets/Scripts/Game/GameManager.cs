using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Atributos del juego
    public float salud;
    public int saludMaxima;
    public int puntuacion = 0;
    public int puntuacionMaxima = 0;

    //ESTO SE PUEDE HACER CON EVENTOS
    public Image imageVida;
    public TextMeshProUGUI textoPuntuacion;
    public TextMeshProUGUI textoPuntuacionMaxima;

    public List<GameObject> objetosAActivarCuandoGameOver;

    private static string KEY_HIGHSCORE = "HIGHSCORE";

    private void Awake()
    {
        //InicializarPuntuacion();
        ActualizarBarraDeSalud();
    }
    private void Update()
    {
        if (salud == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ReiniciarJuego();
            }
        }
    }
    /*private void InicializarPuntuacion()
    {
        puntuacion = 0;
        if (PlayerPrefs.HasKey(KEY_HIGHSCORE))
        {
            puntuacionMaxima = PlayerPrefs.GetInt(KEY_HIGHSCORE);
        }
        else
        {
            puntuacionMaxima = 0;
        }
        textoPuntuacion.text = puntuacion.ToString();
        textoPuntuacionMaxima.text = puntuacionMaxima.ToString();
    }*/
    private void ActualizarBarraDeSalud()
    {
        imageVida.fillAmount = salud / 100;
    }

   /* public void Puntuar(int puntuacion)
    {
        this.puntuacion += puntuacion;
        this.textoPuntuacion.text = this.puntuacion.ToString();
    }*/

    public void DecrementarSalud(int decrementoSalud)
    {
        salud = salud - decrementoSalud;
        if (salud >= saludMaxima)
        {
            salud = saludMaxima;
        }

        if (salud <= 0)
        {
            salud = 0;
            print("Se acabo el juego");
            //TerminarJuego();
        }
        ActualizarBarraDeSalud();
    }
/*
    public void TerminarJuego()
    {
        foreach (GameObject objeto in objetosAActivarCuandoGameOver)
        {
            objeto.SetActive(true);
        }
        if (puntuacion > puntuacionMaxima)
        {
            PlayerPrefs.SetInt(KEY_HIGHSCORE, puntuacion);
            PlayerPrefs.Save();
        }
    }*/
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(0);
    }
}
