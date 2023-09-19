using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float _speed = 100f;
    
    void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime*_speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.name == "Wall")
            Destroy(gameObject);
    }
}
