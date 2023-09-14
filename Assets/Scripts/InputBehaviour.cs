using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBehaviour : MonoBehaviour
{
    // Variables
    [Header("References")]
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] private RaycastBehaviour m_raycastBehaviour;

    // Methods
    /// <summary>
    /// Manda una señal con la posicion en pixeles del dedo
    /// </summary>
    private void Tap(Vector2 position)
    {
        if (Input.touchCount > 0)
        {
            Touch finger = Input.GetTouch(0);
            if(finger.phase == TouchPhase.Began)
            {
                position = finger.position;
            }
        }
    }

    /// <summary>
    /// Devuelve true si la pantalla esta siendo presionada y false si se dejo de presionar.
    /// Tambien devuelve la posicion del dedo al presionar y la posicion del dedo al soltar.
    /// </summary>
    private void Press(bool pressed, Vector2 position)
    {
        if(pressed)
        {
            
        }
    }

    /// <summary>
    /// Swipe hacia la izquierda
    /// </summary>
    private void SwipeLeft()
    {
        transform.position-= Vector3.right;
    }

    /// <summary>
    /// Swipe hacia la derecha
    /// </summary>
    private void SwipeRight()
    {
        transform.position += Vector3.right;
    }

    /// <summary>
    /// Swipe hacia arriba
    /// </summary>
    private void SwipeUp()
    {
        transform.position += Vector3.up;
    }

    /// <summary>
    /// Swipe hacia abajo
    /// </summary>
    private void SwipeDown()
    {
        transform.position -= Vector3.up;
    }













    // Variables
    [Header("Settings")]
    [SerializeField] private float maxTapTime = 0.5f;

    private Vector2 startPos;
    private bool pressed;
    private bool pressWasTriggered;
    private float tapTime;

    // Methods 
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch dedo = Input.GetTouch(0);

            if (dedo.phase == TouchPhase.Began)
            {
                tapTime = 0;
                pressed = true;
                startPos = dedo.position;
                pressWasTriggered = false;
            }

            else if (dedo.phase == TouchPhase.Ended)
            {
                pressed = false;

                if (pressWasTriggered)
                {
                    Press(false, dedo.position);
                }

                if (tapTime < maxTapTime)
                {
                    Vector2 deltaPos = dedo.position - startPos;

                    if (deltaPos.magnitude < 10)
                    {
                        Tap(dedo.position);
                    }
                    else
                    {
                        if (Mathf.Abs(deltaPos.x) > Mathf.Abs(deltaPos.y))
                        {
                            if (deltaPos.x > 0) SwipeRight();
                            else SwipeLeft();
                        }
                        else
                        {
                            if (deltaPos.y > 0) SwipeUp();
                            else SwipeDown();
                        }

                    }
                }
            }

            if (pressed)
            {
                tapTime += Time.deltaTime;

                if (tapTime >= maxTapTime && !pressWasTriggered)
                {
                    pressWasTriggered = true;
                    Press(true, dedo.position);
                }
            }
        }
    }
}
