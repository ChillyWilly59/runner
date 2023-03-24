using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ground : MonoBehaviour 
{
   public int number;
   public float maxPosZ;
   public GameObject[] Variants;
   private string statusAcriveVariant = "";
   private string statusTurnOFFVariant = "";
   private int currentVariant;

   private void FixedUpdate()
   {
      if (transform.localPosition != new Vector3(0, 0, -40))
      {
         if (transform.localPosition.z < maxPosZ)
         {
            if (statusAcriveVariant == "")
            {
               currentVariant = Random.Range(0, Variants.Length);
               Variants[currentVariant].SetActive(true);
               statusAcriveVariant = "Active";
               statusTurnOFFVariant = "Check";
            }
         }
      }
      else if (transform.localPosition == new Vector3(0,0,-40))
      {
         if (statusTurnOFFVariant == "Check")
         {
            Variants[currentVariant].SetActive(false);
            statusTurnOFFVariant = "";
            statusAcriveVariant = "";
         }
      }
   }
}
