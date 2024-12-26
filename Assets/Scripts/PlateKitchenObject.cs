using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngradientAddedOnEventArgs> OnIngradientAdded;
    public class OnIngradientAddedOnEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSo;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSo= new List<KitchenObjectSO>(); 
    
    public List<KitchenObjectSO> kitchenObjectsSOList = new List<KitchenObjectSO>();
    
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSo)
    {
        if (!validKitchenObjectSo.Contains(kitchenObjectSo))
        {
            return false;
        }
        if (kitchenObjectsSOList.Contains(kitchenObjectSo))
        {
            return false;
        }
        else
        {
            kitchenObjectsSOList.Add(kitchenObjectSo);
            OnIngradientAdded?.Invoke(this, new OnIngradientAddedOnEventArgs()
            {  
                kitchenObjectSo = kitchenObjectSo
            });
             return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSoList()
    {
        return kitchenObjectsSOList;
    }
}

