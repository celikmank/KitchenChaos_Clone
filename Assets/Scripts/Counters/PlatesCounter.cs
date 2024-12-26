using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObjectSo;
    
    

    public event EventHandler  OnPlatesSpawned;
    public event EventHandler OnPlatesRemoved;
    
    private float SpawnPLateTimer;
    private float SpawnPlateTimerMax = 4f;
    private int PlatesSpawnedAmount;
    private int PlatesSpawnAmountMax=4;

    private void Update()
    {
        SpawnPLateTimer += Time.deltaTime;

        if (SpawnPLateTimer > SpawnPlateTimerMax)
        {
            SpawnPLateTimer = 0f;
            KitchenObject.SpawnKitchenObject(plateKitchenObjectSo,this);

            if (PlatesSpawnedAmount < PlatesSpawnAmountMax)
            {
                PlatesSpawnedAmount++;
                
                OnPlatesSpawned?.Invoke(this, EventArgs.Empty);
                
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player has empty handed

            if (PlatesSpawnedAmount > 0)
            {
                //there is at least one plate here
                PlatesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSo, player);
                OnPlatesRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
