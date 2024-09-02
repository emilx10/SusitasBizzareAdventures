using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Animal
{
    Camel, Chameleon, Hippo, Ostrich, Giraffe, Zebra, Lemur, Rhyno, Lion
}

public static class AnimalCollection
{
    private static Dictionary<Animal,bool> AnimalStatus = new Dictionary<Animal, bool>() { 
        { Animal.Camel, false },
        { Animal.Chameleon, false },
        { Animal.Hippo, false },
        { Animal.Ostrich, false },
        { Animal.Giraffe, false },
        { Animal.Zebra, false },
        { Animal.Lemur, false },
        { Animal.Rhyno, false },
        { Animal.Lion, false }
    };


    public static void Fill()
    {
        for (int i = 0; i < AnimalStatus.Count; i++)
        {
            AnimalStatus[(Animal)i] = true;
        }
       
    }

    public static void CollectAnimal(Animal animal)
    {
        AnimalStatus[animal] = true;
    }
    public static bool CheckAnimal(Animal animal)
    {
        return AnimalStatus[animal];
    }
}
