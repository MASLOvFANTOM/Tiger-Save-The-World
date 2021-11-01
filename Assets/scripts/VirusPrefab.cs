using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class VirusPrefab : MonoBehaviour, IPointerDownHandler
{
    
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float xDash = 2f;
    [SerializeField] private float yDash = 30f;
    [SerializeField] private float torqueForce = 4f;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        AddForce();
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        if (transform.position.y < -9)
        {
            Escape();
        }
    }

    private void AddForce() // Add prefab force for fly to up 
    {
        Vector3 forceDirection = new Vector3(Random.Range(-xDash, xDash), Random.Range(yDash, yDash - 5), 0);
        Vector3 torqueDirection = new Vector3(Random.Range(-torqueForce, torqueForce), Random.Range(-torqueForce, torqueForce), Random.Range(-torqueForce, torqueForce));
        _rigidbody.AddForce(forceDirection, ForceMode.Impulse);
        _rigidbody.AddTorque(torqueDirection, ForceMode.Impulse);
    }

    private void Dead()
    {
        Game.Singletone.VirusDestroy(transform);
        Destroy(gameObject);
    }
    
    private void Escape()
    {
        Game.Singletone.VirusEscape();
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Dead();
    }
}