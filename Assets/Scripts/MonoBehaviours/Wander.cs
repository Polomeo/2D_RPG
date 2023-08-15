using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    public float pursiutSpeed;
    public float wanderSpeed;
    float currentSpeed;

    public float directionChangeInterval;

    public bool followPlayer;

    Coroutine moveCoroutine;

    Rigidbody2D rb;
    Animator animator;

    Transform targetTransform = null;
    Vector3 endPosition;
    float currentAngle = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        currentSpeed = wanderSpeed;
        StartCoroutine(WanderRoutine());
    }

    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndPoint();
            
            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move(rb, currentSpeed));
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    private void ChooseNewEndPoint()
    {
        // Elegimos un ángulo random y lo sumamos a currentAngle
        currentAngle += UnityEngine.Random.Range(0, 360);

        // Si se pasa de 360, le resta 360 para que quede entre 0 y 360
        // (loopea)
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endPosition += Vector3FromAngle(currentAngle);
    }

    private Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        // Convertimos el ángulo a Radianes
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        // Usamos Coseno y Seno para orientar el vector
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    private IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;

        while(remainingDistance > float.Epsilon)
        {
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }

            if (rigidbodyToMove != null)
            {
                animator.SetBool("b_isWalking", true);

                Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endPosition, speed * Time.deltaTime);
                rb.MovePosition(newPosition);

                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }

            yield return new WaitForFixedUpdate();
        }

        animator.SetBool("b_isWalking", false);
    }
}
