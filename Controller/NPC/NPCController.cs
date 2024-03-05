using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; 
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float interactRange = 20f;
    public bool IsWithinInteractRange = false;



    void Start()
    {
        if (playerTransform == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                playerTransform = playerObject.transform;
            }
            else
            {
                Debug.Log("player gak nemu");
            }
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            InteractWithPlayer();

            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);


            if (distanceToPlayer <= interactRange)
            {
                IsWithinInteractRange = true;
              //itung arah dari npc ke player
              Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0f; 
            //Puter 
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            //slerp (Spherical linear interpolation)-> mempertahankan kecepatan selama rotasi, biar lebih smooth
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            }
            else
            {
                IsWithinInteractRange = false;
            }

            void InteractWithPlayer()
            {
                if (IsWithinInteractRange)
                {
                    Debug.Log("Lagi deket");
                }
            }

        }
    }
}
