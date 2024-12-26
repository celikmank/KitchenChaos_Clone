using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
     [Serializable]
     public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
     
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField]  private List<KitchenObjectSO_GameObject> KitchenObjectSoGameObjectsList;


    private void Start()
    {
        plateKitchenObject.OnIngradientAdded += plateKitchenObject_OnIngradientAdded;

        foreach (KitchenObjectSO_GameObject KitchenObjectSoGameObject in KitchenObjectSoGameObjectsList)
        {
            KitchenObjectSoGameObject.gameObject.SetActive(false);
        }

    }

    private void plateKitchenObject_OnIngradientAdded(object sender, PlateKitchenObject.OnIngradientAddedOnEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject KitchenObjectSoGameObject in KitchenObjectSoGameObjectsList)
        {

            if (KitchenObjectSoGameObject.kitchenObjectSO == e.kitchenObjectSo)
            {
                KitchenObjectSoGameObject.gameObject.SetActive(true);
            }
        }
    }
}
