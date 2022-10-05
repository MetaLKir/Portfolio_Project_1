using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateHelper : MonoBehaviour
{
    public GameObject ObjectToInstantiate;
    
    public void HelpToInstantiate()
    {
        Instantiate(ObjectToInstantiate);
    }
}
