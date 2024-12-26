using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public EventHandler OnRecipeSpawned;
    public EventHandler OnRecipeCompleted;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSo;

    private List<RecipeSO> WaitingrecipeSoList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    

    private void Awake()
    {
        Instance = this;

        WaitingrecipeSoList = new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (WaitingrecipeSoList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSo.recipeSoList[Random.Range(0, recipeListSo.recipeSoList.Count)];
                WaitingrecipeSoList.Add(waitingRecipeSO);
                
                OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < WaitingrecipeSoList.Count; i++)
        {
            RecipeSO waitingRecipeSO = WaitingrecipeSoList[i];


            if (waitingRecipeSO.kitchenObjectsSoList.Count == plateKitchenObject.GetKitchenObjectSoList().Count)
            {
                //has same number of ingradients
                bool PlateContentMatchRecipe = true;

                foreach (KitchenObjectSO RecipeKitchenObjectSo in waitingRecipeSO.kitchenObjectsSoList)
                {
                    //cycling through all ingradients in the recipe
                    bool ingradientFound = false;
                    foreach (KitchenObjectSO PlateKitchenObjectSo in plateKitchenObject.GetKitchenObjectSoList())
                    {
                        //cycling through all ingradients in the plate
                        if (PlateKitchenObjectSo == RecipeKitchenObjectSo)
                        {
                            //ingradient matches
                            ingradientFound = true;
                            break;
                        }
                    }

                    if (!ingradientFound)
                    {
                        //recipe ıngradient was not found on the plate
                        PlateContentMatchRecipe = false;


                    }
                }

                if (PlateContentMatchRecipe)
                {
                    //player delivered the correct recipe
                    // no matches found
                    // player did not give the correct recipe
                    
                    WaitingrecipeSoList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                    return;
                }
            }

            Debug.Log("Player did not deliver a correct recipe");
        }
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return WaitingrecipeSoList;
    }
}
