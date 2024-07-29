using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //[SerializeField] private ListBoidVariable boids;
    [SerializeField] private GameObject boidsPrefab;
    [SerializeField] private float boidCount;

    private void Start()
    {
        //if(boids.boidMovements.Count > 0) 
        //    boids.boidMovements.Clear();  // chuan bi cho viec tao moi
        boidCount = Random.Range(60, 100);
        for (int i = 0; i  < boidCount; i++)
        {
            float direction = Random.Range(0f, 360f); // huong ngau nhien

            Vector3 position = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
            // Sinh ra moi boid co vi tri va huong ngau nhien
            GameObject boid = Instantiate(boidsPrefab, position, Quaternion.Euler( Vector3.forward * direction) * boidsPrefab.transform.localRotation); 
            boid.transform.SetParent(transform);
            //boids.boidMovements.Add(boid.GetComponent<BoidMovement>()); 
            
            transform.position = position;
        }
    }
}
