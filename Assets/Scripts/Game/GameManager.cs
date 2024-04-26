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
    public int nivel = 1;
    public float experiencia = 0;
    public float experienciaParaSiguienteNivel = 100;

    //Puntuacion
    public int puntuacion = 0;
    public int puntuacionMaxima = 0;


    //ESTO SE PUEDE HACER CON EVENTOS

    //Interfaz
    public Image imageVida;
    public Image imageExp;
    public TextMeshProUGUI textoPuntuacion;
    public TextMeshProUGUI textoPuntuacionMaxima;
    public TextMeshProUGUI textoNivel;
    public TextMeshProUGUI textoExperiencia;


    public GameObject levelPanel;

    public List<GameObject> objetosAActivarCuandoGameOver;

    private static string KEY_HIGHSCORE = "HIGHSCORE";

    private void Awake()
    {
        //InicializarPuntuacion();
        ActualizarBarraDeSalud();
        ActualizarBarraDeExperiencia();
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

    private void ActualizarBarraDeExperiencia()
    {
        while (experiencia >= experienciaParaSiguienteNivel)
        {
            SubirNivel(); // Subir de nivel mientras haya suficiente experiencia para el siguiente nivel
        }

        imageExp.fillAmount = experiencia / experienciaParaSiguienteNivel;
        textoNivel.text = "Level " + nivel.ToString();
        textoExperiencia.text = experiencia.ToString() + " / " + experienciaParaSiguienteNivel.ToString();
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

    public void GanarExperiencia(int cantidad)
    {
        experiencia += cantidad;
        if (experiencia >= experienciaParaSiguienteNivel)
        {
            SubirNivel();
        }
        ActualizarBarraDeExperiencia();
    }

    private void SubirNivel()
    {
        PauseGame();
        nivel++;
        experiencia -= experienciaParaSiguienteNivel;
        experienciaParaSiguienteNivel *= 2; // Ajusta esta fórmula según tus necesidades
        // Aquí podrías implementar aumentos en salud máxima u otras estadísticas del jugador al subir de nivel
    }

    
    void PauseGame()
    {
        levelPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        levelPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OptionSelected()
    {
        ResumeGame();
    }


    public void PowerUpHealthMax()
    {
        saludMaxima = saludMaxima + 50;
        salud = salud + 50;
        ActualizarBarraDeSalud();
    }

        public void PowerUpHealing()
    {
        salud = salud + 25;
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
