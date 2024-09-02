using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalCollectionManager : MonoBehaviour
{

    [SerializeField] private Image[] animalslImages;
    void Awake()
    {
        for (int i = 0; i < animalslImages.Length; i++) 
        {
            if (AnimalCollection.CheckAnimal((Animal)i))
            {
                animalslImages[i].color = Color.white;
            }
            else
            {
                animalslImages[i].color = Color.black;
            }
        }
    }
}
