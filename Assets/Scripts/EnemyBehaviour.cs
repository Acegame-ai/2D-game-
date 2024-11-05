using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    GameManager _gameManager;
    GameObject _player;

    float _enemyHealth = 100f;
    float _enemyMoveSpeed = 2f;
    Quaternion _targetRotation;
    bool _disableEnemy = false;
    Vector2 _moveDirection;
    private int _noofenemy;

    GameManager gameManager;

   

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>();
    }
     void OnDestroy()
    {
        gameManager.AddKill();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameManager._gameOver && !_disableEnemy)
        {
            MoveEnemy();
            RotateEnemy();
        }
    }

    void MoveEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _enemyMoveSpeed * Time.deltaTime);
    }

    void RotateEnemy()
    {
        _moveDirection = _player.transform.position - transform.position;
        _moveDirection.Normalize();

        _targetRotation = Quaternion.LookRotation(Vector3.forward, _moveDirection);

        if (transform.rotation != _targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, 200 * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(Damaged());

            _enemyHealth -= 40f;
            if (_enemyHealth <= 0)
            {
                _noofenemy ++;
                Destroy(gameObject);
                Debug.Log("you have destroyed " + _noofenemy + " enemies");
            }
            Destroy(collision.gameObject);
        }   
        else if (collision.gameObject.CompareTag("Player"))
        {
            _gameManager._gameOver = true;
            collision.gameObject.SetActive(false);
        }
    }

    IEnumerator Damaged()
    {
        _disableEnemy = true;
        yield return new WaitForSeconds(0.5f);
        _disableEnemy = false;
    }

    
}
