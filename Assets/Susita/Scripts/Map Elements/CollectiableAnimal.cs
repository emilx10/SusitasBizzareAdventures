using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiableAnimal : MonoBehaviour
{
    [SerializeField] Animal animal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AnimalCollection.CollectAnimal(animal);
            Destroy(gameObject);
        }

        
    }
}
