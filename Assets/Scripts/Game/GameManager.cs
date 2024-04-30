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
    public float distanciaAtraccion = 8;

    //Valor monedas
    public int valor;
    public int puntuacionMaxima = 0;


    //ESTO SE PUEDE HACER CON EVENTOS

    //Interfaz
    public Image imageVida;
    public Image imageExp;
    public TextMeshProUGUI textoMonedas;
    public TextMeshProUGUI textoPuntuacionMaxima;
    public TextMeshProUGUI textoNivel;
    public TextMeshProUGUI textoExperiencia;

    public PlayerController playerController;
    public GameObject levelPanel;

    public List<GameObject> objetosAActivarCuandoGameOver;

    private static string KEY_COINS = "COINS";

    private void Awake()
    {
        InicializarMonedas();
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
    
    private void InicializarMonedas()
    {
        if (PlayerPrefs.HasKey(KEY_COINS))
        {

            valor = PlayerPrefs.GetInt(KEY_COINS);
        }
        else
        {

            valor = 0;
        }
        textoMonedas.text = valor.ToString();
    }
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

    public void Monedas(int valor)
    {
        this.valor += valor;
        this.textoMonedas.text = this.valor.ToString();
    }

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
            TerminarJuego();
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

    public void PowerUpHealthMax()
    {
        saludMaxima = saludMaxima + 50;
        salud = salud + 50;
        ActualizarBarraDeSalud();
        ResumeGame();
    }

    public void PowerUpHealing()
    {
        salud = salud + 25;
        ActualizarBarraDeSalud();
        ResumeGame();
    }

    public void PowerUpSpeed()
    {
        // Modificar la velocidad de correr en el PlayerController
        playerController.velocidadCaminar += playerController.velocidadCaminar *0.05f;
        playerController.velocidadCorrer += playerController.velocidadCorrer *0.05f;
        ResumeGame();
    }

    public void PowerUpAttraction()
    {
        print(distanciaAtraccion);
        distanciaAtraccion += distanciaAtraccion * 0.5f;
        print(distanciaAtraccion);
        ResumeGame();
    }
    public void TerminarJuego()
    {
        foreach (GameObject objeto in objetosAActivarCuandoGameOver)
        {
            objeto.SetActive(true);
        }

            PlayerPrefs.SetInt(KEY_COINS, valor);

            PlayerPrefs.Save();

    }

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(0);
    }
}
