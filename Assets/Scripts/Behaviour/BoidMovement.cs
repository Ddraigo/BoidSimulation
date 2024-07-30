using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoidMovement : MonoBehaviour
{
    
    public Vector3 Velocity { get; private set; }
    [Range(0f, 360f)]
    public float visionAngle;
    public float radius;
    public float forwardSpeed;
    public float turnSpeed;

    private float ratio = 2f;

    [Range(0f, 10f)]
    [SerializeField] private float rateOfSeparation;
    [Range(0f, 10f)]
    [SerializeField] private float rateOfAligment;
    [Range(0f, 10f)]
    [SerializeField] private float rateOfCohesion;
    [SerializeField] private Boundery boundery;
    [SerializeField] private LayerMask boidLayerMask;

    private List<BoidMovement> boidsInRange = new List<BoidMovement>();
    private List<BoidMovement> boidsToAvoid = new List<BoidMovement>();


    private void FixedUpdate()
    {
        Velocity = Vector2.Lerp(Velocity, CalculateVelocity(), turnSpeed / 2 * Time.fixedDeltaTime);
        LimitBoundery();
        transform.position += Velocity * Time.fixedDeltaTime;
        LookRotation();
        Debug.Log(boidsInRange.Count);
        Debug.Log("AVOID: " + boidsToAvoid.Count);
    }

   

    private Vector2 CalculateVelocity()
    {
        var boidInRange = FindBoidsInRange();
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

    //private List<BoidMovement> FindBoidsInRange()
    //{
    //    boidsInRange.Clear();
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, radius, boidLayerMask);

    //    foreach (var collider in colliders)
    //    {
    //        BoidMovement boid = collider.GetComponent<BoidMovement>();
    //        if (boid != null && InVisionCone(boid.transform.position))
    //        {
    //            boidsInRange.Add(boid);
    //        }
    //    }

    //    return boidsInRange;
    //}

    //private List<BoidMovement> ProtectedArea()
    //{
    //    boidsToAvoid.Clear();

    //    foreach (var boid in boidsInRange)
    //    {
    //        if (boid != null && (boid.transform.position - transform.position).magnitude <= ratio)
    //        {
    //            boidsToAvoid.Add(boid);
    //        }
    //    }

    //    return boidsToAvoid;
    //}

    private List<BoidMovement> FindBoidsInRange()
    {
        if (boidsInRange.Count > 0)
            boidsInRange.Clear();

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.forward, radius, boidLayerMask);

        foreach (RaycastHit hit in hits)
        {
            BoidMovement boid = hit.collider.GetComponent<BoidMovement>();
            if (boid != null && InVisionCone(boid.transform.position))
            {
                boidsInRange.Add(boid);
            }
        }

        return boidsInRange;
    }


    private List<BoidMovement> ProtectedArea()
    {
        if (boidsToAvoid.Count > 0)
            boidsToAvoid.Clear();

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, ratio, transform.forward, ratio, boidLayerMask);

        foreach (RaycastHit hit in hits)
        {
            BoidMovement boid = hit.collider.GetComponent<BoidMovement>();
            if (boid != null && InVisionCone(boid.transform.position))
            {
                boidsToAvoid.Add(boid);
            }
        }
        return boidsToAvoid;
    }

    private bool InVisionCone(Vector2 position)
    {
        Vector2 directionToPosition = position - (Vector2)transform.position;
        float dotProduct = Vector2.Dot(transform.forward, directionToPosition);
        float cosHalfVisonAngle = Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad);
        return dotProduct >= cosHalfVisonAngle;
    }


    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ratio);


        Vector3 forward = transform.forward;
        Vector3 right = Quaternion.Euler(0, visionAngle * 0.5f, 0) * forward;
        Vector3 left = Quaternion.Euler(0, -visionAngle * 0.5f, 0) * forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + right * radius);
        Gizmos.DrawLine(transform.position, transform.position + left * radius);

        Gizmos.DrawLine(transform.position + right * radius, transform.position + left * radius);

        foreach (var boid in FindBoidsInRange())
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
        Vector3 direction = Vector3.zero;

        foreach (var boid in boidMovements)
        {
            direction += boid.Velocity;
        }

        return boidMovements.Count > 0 ? (direction / boidMovements.Count).normalized : transform.forward;
    }

    private Vector2 Cohesion(List<BoidMovement> boidMovements)
{
    Vector3 center = Vector3.zero;

    foreach (var boid in boidMovements)
    {
        center += boid.transform.position;
    }

    center = boidMovements.Count > 0 ? center / boidMovements.Count : transform.position;
    return (center - transform.position).normalized;
}
   
}
