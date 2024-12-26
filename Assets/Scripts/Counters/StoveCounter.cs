using System;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSoArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSoArray;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> onStateChanged;
    
    public class OnStateChangedEventArgs :EventArgs
    {
        public State State;
    }
    public enum  State
    {
        Idle,
        Frying,
        Fried,
        Burned,
        
    }

    private State state;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSo;
    private BurningRecipeSO burningRecipeSo;
    private float burningTimer;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle :
                    break; 
                case State.Frying :
                    
                    fryingTimer += Time.deltaTime; 
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = fryingTimer / fryingRecipeSo.FryingTimeMax
                    });;
                    
                    if (fryingTimer > fryingRecipeSo.FryingTimeMax)
                    {
                        //fried
                        
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipeSo.Output, this);
                        Debug.Log("Object fried");
                        
                        state = State.Fried;
                        
                        burningTimer = 0f; 
                        burningRecipeSo = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        
                        onStateChanged?.Invoke(this, new OnStateChangedEventArgs() { State = state });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                        {
                            progressNormalized = fryingTimer / fryingRecipeSo.FryingTimeMax
                        });
                    }
                    break;
                
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = burningTimer / burningRecipeSo.BurningTimeMax
                    });;
                    
                    if (burningTimer > burningRecipeSo.BurningTimeMax)
                    {
                        //fried
                        GetKitchenObject().DestroySelf();
                            
                        KitchenObject.SpawnKitchenObject(burningRecipeSo.Output, this);
                        Debug.Log("Object burned");

                        state = State.Burned;
                        
                        onStateChanged?.Invoke(this, new OnStateChangedEventArgs() { State = state });
                    }
                    
                    break;
                case State.Burned:
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = 0f
                    });;
                    break;
            }
        }
       
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carrying something that can be cut
                    
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    fryingRecipeSo = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    
                    state = State.Frying;
                    fryingTimer= 0f;
                    
                    onStateChanged?.Invoke(this, new OnStateChangedEventArgs() { State = state });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = fryingTimer / fryingRecipeSo.FryingTimeMax
                    });;
                }
            }
            //player not carrying
        }
        else
        {
            //there is no kitchen object here
            if (player.HasKitchenObject())
            {
                // player is carrying something
                
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // player is holding plate
                    plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;
                
                        onStateChanged?.Invoke(this, new OnStateChangedEventArgs() { State = state });
                
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                        {
                            progressNormalized = 0f
                        });;
                    }
                }
            }
            else
            {
                // player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
                
                onStateChanged?.Invoke(this, new OnStateChangedEventArgs() { State = state });
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                {
                    progressNormalized = 0f
                });;
            }
        }
        
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSo != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSo != null)
        {
            return fryingRecipeSo.Output;
        }

        return null;

    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSo in fryingRecipeSoArray)
        {
            if (fryingRecipeSo.Input == inputKitchenObjectSO)
            {
                return fryingRecipeSo;
            }
        }
            
        return null;
    }
    
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSo in burningRecipeSoArray)
        {
            if (burningRecipeSo.Input == inputKitchenObjectSO)
            {
                return burningRecipeSo;
            }
        }
            
        return null;
    }
}
