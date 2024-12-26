using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlateIconUI : MonoBehaviour
{
   
   [SerializeField] private PlateKitchenObject plateKitchenObject;
   [SerializeField] private Transform IconTemplate;

   private void Awake()
   {
      IconTemplate.gameObject.SetActive(false);
   }

   private void Start()
   {
      plateKitchenObject.OnIngradientAdded += plateKitchenObject_OnIngradientAdded;
   }

   private void plateKitchenObject_OnIngradientAdded(object sender, PlateKitchenObject.OnIngradientAddedOnEventArgs e)
   {
      updateVisual();
   }

   private void updateVisual()
   {
      foreach (Transform child in transform)
      {
         if (child==IconTemplate) continue;
         {
            Destroy(child.gameObject);
         }
      }
      foreach (KitchenObjectSO kitchenObjectSo in plateKitchenObject.GetKitchenObjectSoList())
      {
        Transform iconTransform = Instantiate(IconTemplate, transform);
        iconTransform.gameObject.SetActive(true);
        iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSo(kitchenObjectSo);
        
      }
   }
}

