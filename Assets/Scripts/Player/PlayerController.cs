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
    public float VelocidadActual { get { return velocidadActual; } }
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

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift))
        {
            velocidadActual = velocidadCorrer;
        }
        else if (movimientoH == 0 && movimientoV == 0)
        {
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
            // Calcular la nueva posición sin cambiar la componente Y
            Vector3 newPosition = rb.position + movimientoDirection * velocidadActual * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
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

        Camera mainCamera = Camera.main;
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movimiento = cameraForward * movimientoV + cameraRight * movimientoH;

        return movimiento.normalized;
    }
}