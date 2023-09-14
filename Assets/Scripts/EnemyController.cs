using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    // Methods
    /// <summary>
    /// Funcion pensada para hacerle daño al enemigo y eliminarlo.
    /// </summary>
    public void DoDamage()
    {
        FindObjectOfType<EnemyManager>().RemoveEnemy(gameObject);

        // No recuerdo como se destruia un enemigo...

    }










    // Variables
    [Header("Movement Settings")]
    [SerializeField] private float m_movementTime;
    [SerializeField] private AnimationCurve m_movementCurve;

    /// <summary>
    /// Random movement.
    /// </summary>
    public void RandomMove()
    {
        Vector2 direction = Vector2.zero;
        switch (Random.Range(0, 5))
        {
            case 0: direction = Vector2.zero; break;
            case 1: direction = Vector2.left; break;
            case 2: direction = Vector2.right; break;
            case 3: direction = Vector2.down; break;
            case 4: direction = Vector2.up; break;
        }

        StartCoroutine(MovementAnimation(direction));
    }
    // Animations
    private IEnumerator MovementAnimation(Vector2 direction)
    {
        Vector2 a = transform.position;
        Vector2 b = a + direction;

        for (float i = 0; i < m_movementTime; i += Time.deltaTime)
        {
            transform.position = Vector2.LerpUnclamped(a, b, m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
        }

        transform.position = b;

        if (b == PlayerController.position) SceneManager.LoadScene(0);
    }
}
