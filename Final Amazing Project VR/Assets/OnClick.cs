using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
  public GameObject objectToDestroy;

     void OnMouseDown ()
     {
         Destroy(objectToDestroy);
     }
}
