using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    
    public Vector3 Velocity { get; private set; }
    public float visionAngle = 270f;
    public float radius;
    public float forwardSpeed;
    public float turnSpeed;

    private float ratio = 2f;
    [SerializeField] private ListBoidVariable boids;
    [SerializeField] private float rateOfSeparation;
    [SerializeField] private float rateOfAligment;
    [SerializeField] private float rateOfCohesion;
    [SerializeField] private Boundery boundery;

    private void FixedUpdate()
    {
        Velocity = Vector2.Lerp(Velocity, CalculateVelocity(), turnSpeed / 2 * Time.fixedDeltaTime);
        LimitBoundery();
        transform.position += Velocity * Time.fixedDeltaTime;
        LookRotation();
    }

    private Vector2 CalculateVelocity()
    {
        var boidInRange = BoidsInRange();
        var boidToAvoid = ProtectedArea();
        Vector2 velocity = ((Vector2)transform.forward + rateOfSeparation * Separation(boidToAvoid)
                                                       + rateOfAligment * Aligment(boidInRange) 
                                                       + rateOfCohesion * Cohesion(boidInRange)).normalized * forwardSpeed;
        return velocity;
    }

    private void LimitBoundery()
    {
        Vector3 direction = Velocity.normalized;
        Vector3 position = transform.position;

        if (Mathf.Abs(position.x) > boundery.XLimit)
        {
            direction.x = - direction.x;
        }

        if (Mathf.Abs(position.y) > boundery.YLimit)
        {
            direction.y = - direction.y;
        }

        Velocity = direction * Velocity.magnitude;
    }

    private void LookRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(Velocity), turnSpeed * Time.fixedDeltaTime);
    }
    
    // list cac boid nanm trong pham vi anh huong
    private List<BoidMovement> BoidsInRange()
    {
        var listBoid = boids.boidMovements.FindAll(boid => boid != this
            && (boid.transform.position - transform.position).magnitude <= radius
            && InVisionCone(boid.transform.position));
        return listBoid;
    }

    private List<BoidMovement> ProtectedArea()
    {
        var listBoid = boids.boidMovements.FindAll(boid => boid != this
            && (boid.transform.position - transform.position).magnitude <= ratio);
        return listBoid;
    }

    private bool InVisionCone(Vector2 position)
    {
        Vector2 directionToPosition = position - (Vector2)transform.position;
        float dotProduct = Vector2.Dot(transform.forward, position);
        float cosHalfVisonAngle = Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad);
        return dotProduct >= cosHalfVisonAngle;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        var boidsRange = BoidsInRange();
        foreach (var boid in boidsRange)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, boid.transform.position);

        }
    }


    // Cac quy tac di chuyen
    private Vector2 Separation(List<BoidMovement> boidMovements)
    {
        Vector2 direction = Vector2.zero;
        foreach (var boid in boidMovements)
        {
            // tinh ti le khoang cach tu boid hien
            // tai den boid trong list so voi ban kinh quy dinh trong [0, 1]
            //float ratio = Mathf.Clamp01((boid.transform.position - transform.position).magnitude / radius);
            direction -= ratio * (Vector2)(boid.transform.position - transform.position);
        }

        // Chuan hoa de co do lon la 1, dam bao rang chi huong vector duoc tra ve chu khong phai do lon cua no
        return direction.normalized;
    }

    private Vector2 Aligment(List<BoidMovement> boidMovements)
    {
        Vector2 direction = Vector2.zero; ;
        
        foreach (var boid in boidMovements)
        {
            direction += (Vector2)boid.Velocity;
        }

        if (boidMovements.Count != 0) 
        {
            // tinh trung binh van toc 
            direction /= boidMovements.Count;
        }
        else
        {
            direction = Velocity;
        }
        return direction.normalized;
    }

    private Vector2 Cohesion(List<BoidMovement> boidMovements)
    {
        Vector2 direction;
        Vector2 center = Vector2.zero;
        foreach (var boid in boidMovements)
        {
            center += (Vector2)boid.transform.position;
        }

        if (boidMovements.Count != 0)
        {
            center /= boidMovements.Count;
        }
        else
        {
            center = transform.position;
        }

        direction = center - (Vector2)transform.position; // tinh vector tu vi tri hien tai den boid trung tam 
        return direction.normalized;
    }
   
}
