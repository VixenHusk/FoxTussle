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
    public float ratioDefensa = 1f;
    public int nivel = 1;
    public float experiencia = 0;
    public float experienciaParaSiguienteNivel = 100;
    public float distanciaAtraccion = 0.5f;
    public int vidaExtra = 0;

    public float multiplicadorEXP = 1;

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

    public GameObject prefabSistemaParticulas;
    public GameObject foxGameObject;

    // Referencia a CargaEscenas
    public CargaEscenas cargaEscenas;
    // POWER UPS - que agregan efectos
    private float cantidadRegeneracion = 0.75f;
    public GameObject prefabRevivir;

    //FireOrbs
    public fireOrbs fireOrbsScript;

    //Shop
    public GameObject shrineDoor;
    public Vector3 doorOffset; // El offset para que aparezca la puerta

    //VidaExtra
    public GameObject textoVidaExtra;

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
        imageVida.fillAmount = salud / saludMaxima;
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
    // Asegúrate de que el ratio de defensa sea mayor que 0 para evitar divisiones por cero.
    if (ratioDefensa <= 0)
    {
        Debug.LogError("El ratio de defensa debe ser mayor que 0.");
        return;
    }

    // Calcula el daño efectivo basado en el ratio de defensa.
    float dañoEfectivo = decrementoSalud / ratioDefensa;

    // Reduce la salud por el daño efectivo.
    salud -= dañoEfectivo;

    // Asegúrate de que la salud no supere el máximo permitido.
    if (salud > saludMaxima)
    {
        salud = saludMaxima;
    }

    if (salud <= 0)
    {
        if (vidaExtra > 0)
        {
            UsarVidaExtra();
        }
        else
        {
            salud = 0;
            print("Se acabo el juego");
            cargaEscenas.JugadorHaMuerto();
            TerminarJuego();
        }
    }

    // Actualiza la barra de salud.
    ActualizarBarraDeSalud();
}

    private void UsarVidaExtra()
    {
        vidaExtra--;
        salud = saludMaxima;
        ActualizarBarraDeSalud();

        if (prefabRevivir != null)
        {
            GameObject reviveEffect = Instantiate(prefabRevivir, foxGameObject.transform);
            reviveEffect.transform.localPosition = Vector3.zero;
        }

        if (textoVidaExtra != null)
        {
            StartCoroutine(ActivateTextoVidaExtraForSeconds(textoVidaExtra, 1.5f));
        }
    }

    private IEnumerator ActivateTextoVidaExtraForSeconds(GameObject textoVidaExtra, float seconds)
    {
        textoVidaExtra.SetActive(true);
        yield return new WaitForSeconds(seconds);
        textoVidaExtra.SetActive(false);
    }

    public void GanarExperiencia(int cantidad)
    {
        float experienciaTotal = cantidad * multiplicadorEXP;
        experiencia += experienciaTotal;
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
        experienciaParaSiguienteNivel *= 2f; // Ajusta esta fórmula según tus necesidades
        // Instanciar el prefab del sistema de partículas
        GameObject particulas = Instantiate(prefabSistemaParticulas, foxGameObject.transform.position, Quaternion.identity);

    }

    
    void PauseGame()
    {
        Cursor.visible = true;
        levelPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
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

    public void PowerUpFireOrb(){
        // Llamar al método FireOrb del script fireOrbs
        if (fireOrbsScript != null)
        {
            fireOrbsScript.FireOrb();
            print("ha entrado"); 
            ResumeGame();
        }
        else
        {
            Debug.LogError("No se ha asignado el script fireOrbs en el Inspector.");
        }
    }

    public void PowerUpShop()
    {
        // Verifica si el prefab de la puerta y el objeto del jugador están asignados
        if (shrineDoor != null && foxGameObject != null)
        {
            // Calcula la posición de spawn de la puerta sumando el offset a la posición del jugador
            Vector3 spawnPosition = foxGameObject.transform.position + doorOffset;
            // Instancia la puerta en la posición calculada
            Instantiate(shrineDoor, spawnPosition, Quaternion.identity);
            // Reanuda el juego después de instanciar la puerta
            ResumeGame();
        }
        else
        {
            // Imprime un mensaje de error si no se han asignado los objetos necesarios
            Debug.LogError("shrineDoor o foxGameObject no están asignados en el Inspector.");
        }
    }

    public void PowerUpRegeneration()
    {
        cantidadRegeneracion *= 2;

        // Cancelar cualquier invocación previa de RegenerarSalud
        CancelInvoke("RegenerarSalud");

        // Iniciar la regeneración de salud
        InvokeRepeating("RegenerarSalud", 0f, 2f);
        
        ResumeGame();
    }

    private void RegenerarSalud()
    {
        // Añade la cantidad de regeneración a la salud
        salud += cantidadRegeneracion;

        // Asegúrate de que la salud no supere el límite máximo
        salud = Mathf.Min(salud, saludMaxima);

        // Actualiza la barra de salud
        ActualizarBarraDeSalud();
    }
    //seccion de Items

    public void GetItemRabbit()
    {
        saludMaxima = saludMaxima + 150;
        salud = saludMaxima;
        ActualizarBarraDeSalud();
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
