using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    // Variables
    [Header("References")]
    [SerializeField] private GameObject m_enemyPrefab;
    public List<GameObject> m_instantiatedEnemies = new List<GameObject>();

    // Methods
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        // Hmm, no recuerdo como spawnear un numero determinado de enemigos a la vez...
        for(int i = 0; i < 5; i++)
        {
            SpawnEnemy();
        }
        

    }

    /// <summary>
    /// Funcion para spawnear un enemigo (no implementada)
    /// </summary>
    private void SpawnEnemy()
    {
        int x = Random.Range(-5, 6);
        int y = Random.Range(-5, 6);

        Vector3 SpawnPosition = new Vector3 (x, y, 0);
        
        // Y ahora no recuerdo commo se spawnean...
        GameObject enemy = Instantiate(m_enemyPrefab, SpawnPosition, Quaternion.identity);

        // Tambien me gustaria añadirlos a una lista ya creada...
        m_instantiatedEnemies.Add(enemy);

    }

    /// <summary>
    /// Funcion para remover un enemigo de la lista y comparar si el juego termino
    /// (Es importante remover el enemigo antes de destruirlo)
    /// </summary>
    public void RemoveEnemy(GameObject enemy)
    {
        // Tengo que avisar que el enemigo ya no existe...
       m_instantiatedEnemies.Remove(enemy);

        // Y comparar si no hay ninguno...
        if (m_instantiatedEnemies.Count == 0)
        {
           SceneManager.LoadScene(0);
        }


    }







    // Methods
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        TurnManager.OnTurnChange += HandleTurnChange;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        TurnManager.OnTurnChange -= HandleTurnChange;
    }

    /// <summary>
    /// Mover a todos los enemigos al ser el turno de ellos.
    /// </summary>
    private void HandleTurnChange(TurnManager.Turn turn)
    {
        if (turn == TurnManager.Turn.Player) return;

        foreach (GameObject instance in m_instantiatedEnemies)
        {
            if (instance.TryGetComponent(out EnemyController enemy))
            {
                enemy.RandomMove();
            }
        }

        Invoke("EndTurn", 0.5f);
    }

    /// <summary>
    /// Terminar el turno del enemigo.
    /// </summary>
    private void EndTurn()
    {
        TurnManager.EndTurn();
    }
}
