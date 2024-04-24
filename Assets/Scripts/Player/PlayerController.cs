using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [Range(1, 100)]
    public float velocidadCaminar = 6f;
    [Range(1, 100)]
    public float velocidadCorrer = 12f;
    private float velocidadParado = 0f;
    private float velocidadActual;
    // Variable para almacenar la velocidad actual
    public float VelocidadActual { get { return velocidadActual; } }

    // Variable para controlar la suavidad del movimiento
    public float suavidadRotacion = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocidadActual = velocidadCaminar; // Al inicio, el jugador camina a velocidad normal
    }

    void FixedUpdate()
    {
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        // Obtener la dirección del movimiento horizontal
        Vector3 movimientoDirection = GetMovementDirection();

        // Verificar si el jugador está corriendo (Shift está presionado)
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift))
        {
            velocidadActual = velocidadCorrer;
        }
        else if (movimientoH == 0 && movimientoV == 0)
        {
            // Si no hay entrada de teclado, el jugador está parado
            velocidadActual = velocidadParado;
        }
        else
        {
            velocidadActual = velocidadCaminar;
        }
        
        // Verificar si no se está presionando ninguna tecla de movimiento
        if (Mathf.Approximately(movimientoDirection.magnitude, 0.0f))
        {
            // Detener el movimiento estableciendo la velocidad a cero
            rb.velocity = Vector3.zero;
        }
        else
        {
            // Aplicar la velocidad directamente sin inercia
            rb.velocity = movimientoDirection * velocidadActual;
        }

        // Orientar suavemente el jugador hacia la dirección del movimiento
        if (movimientoDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movimientoDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, suavidadRotacion * Time.deltaTime);
        }
    }

    public Vector3 GetMovementDirection()
    {
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        // Obtener la cámara principal para ajustar el movimiento a la perspectiva isométrica
        Camera mainCamera = Camera.main;
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0f; // Asegurar que no haya movimiento vertical
        cameraRight.y = 0f; // Asegurar que no haya movimiento vertical
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calcular el vector de movimiento en función de la vista isométrica de la cámara
        Vector3 movimiento = cameraForward * movimientoV + cameraRight * movimientoH;

        // Devolver solo la dirección horizontal del movimiento
        return movimiento.normalized;
    }
}
