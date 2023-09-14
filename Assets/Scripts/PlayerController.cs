using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    [Header("Movement Settings")]
    [SerializeField] private float m_movementTime;
    [SerializeField] private AnimationCurve m_movementCurve;

    [Header("Jump Settings")]
    [SerializeField] private float m_jumpTime = 0.5f;
    [SerializeField] private AnimationCurve m_jumpCurve;

    // Ojala tuviera una lista de strings que me hicieran reir...

    public List<string> lista_JAJA = new List<string>();

    [Header("References")]
    [SerializeField] private EnemyManager m_enemyManager;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        // Me gustaria poder comparar la magnitud de mi acelerometro...
        // Y si este fuera mayor que un numero en especifico, que me ejecutara una funcion...


    }


    /// <summary>
    /// Intentar añadir un string desde la lista a la queue.
    /// </summary>
    public void TryAddJumpString()
    {
        if (m_isInCooldown) return;

        // Hmm, me gustaria añadir un texto aleatorio de mi lista chistosa a una Queue...


        StartCoroutine(Cooldown());
    }

    /// <summary>
    /// Moverse haciendo tween a una direccion especifica.
    /// </summary>
    public void MoveTo(Vector2 direction)
    {


        // Comparar si es el turno del jugador y si se puede mover
        if (!m_isPlayerTurn) return;
        if (m_isMoving) return;

        // Me gustaria que no se pudiera salir de la grilla...



        // Hacer que se mueva bonito.
        if (m_movementCoroutine != null) StopCoroutine(m_movementCoroutine);
        m_movementCoroutine = StartCoroutine(MovementAnimation(direction));
    }

    /// <summary>
    /// Preparar el salto y revisar si puede saltar.
    /// </summary>
    public void PrepareJump()
    {
        // Comparar si es el turno del jugador y si se puede mover
        if (!m_isPlayerTurn) return;
        if (m_isMoving) return;

        // Me gustaria comparar si existe mas de una cadena de texto en mi queue...


        // Y si fuera asi activar un booleano para saltar...


    }

    /// <summary>
    /// Saltar al punto de destino.
    /// </summary>
    public void Jump(Vector2 worldPosition)
    {
        // Me gustaria comprobar si puedo saltar reemplazando true por un booleano...
        if (true)
        {
            worldPosition.x = Mathf.Round(worldPosition.x);
            worldPosition.y = Mathf.Round(worldPosition.y);

            // Esto hace que el jugador no pueda salirse de la grilla.
            if (Mathf.Abs(worldPosition.x) > 5) return;
            if (Mathf.Abs(worldPosition.y) > 5) return;

            StartCoroutine(JumpAnimation(worldPosition));
        }
    }

    /// <summary>
    /// Funcion para detectar si existe algo en la posicion al terminar de moverse / saltar.
    /// </summary>
    public void CheckPosition(Vector2 position)
    {
        // Me gustaria comparar si existe un enemigo en la posicion donde estoy para ver si me muero...
        // Ojala tuviera una lista con todos los enemigos...

    }











    // Variables
    public static Vector2 position;

    private Coroutine m_movementCoroutine;
    private bool m_isMoving = false;
    private bool m_isInCooldown = false;
    private bool m_isPlayerTurn = false;

    // Methods
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        TurnManager.OnTurnChange += HandleTurn;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        TurnManager.OnTurnChange -= HandleTurn;
    }

    /// <summary>
    /// Set if is the player turn.
    /// </summary>
    private void HandleTurn(TurnManager.Turn turn) => m_isPlayerTurn = turn == TurnManager.Turn.Player;

    // Animations
    private IEnumerator MovementAnimation(Vector2 direction)
    {
        m_isMoving = true;

        Vector2 a = transform.position;
        Vector2 b = a + direction;

        for (float i = 0; i < m_movementTime; i += Time.deltaTime)
        {
            transform.position = Vector2.LerpUnclamped(a, b, m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
        }

        m_isMoving = false;
        transform.position = b;
        CheckPosition(b);

        position = b;

        TurnManager.EndTurn();
    }

    private IEnumerator JumpAnimation(Vector2 worldPosition)
    {
        m_isMoving = true;

        Vector2 a = transform.position;
        Vector2 b = worldPosition;

        for (float i = 0; i < m_jumpTime; i += Time.deltaTime)
        {
            transform.position = Vector2.LerpUnclamped(a, b, m_jumpCurve.Evaluate(i / m_jumpTime));
            yield return null;
        }

        m_isMoving = false;
        transform.position = b;

        position = b;

        CheckPosition(b);

        TurnManager.EndTurn();
    }

    private IEnumerator Cooldown()
    {
        m_isInCooldown = true;
        yield return new WaitForSeconds(0.5f);
        m_isInCooldown = false;
    }
}
