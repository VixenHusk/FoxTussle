using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour
{
    // Delegado para los métodos de manejo de clic
    public delegate void ClickAction();

    // Evento que se dispara al hacer clic en el botón
    public event ClickAction OnClick;

    // Método que se llama cuando se hace clic en el botón
    public void ButtonClicked()
    {
        // Si hay suscriptores al evento OnClick, se ejecutan
        OnClick?.Invoke();
    }
}