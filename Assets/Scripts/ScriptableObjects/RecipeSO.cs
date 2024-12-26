
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
  public List<KitchenObjectSO> kitchenObjectsSoList;
  public string recipeName;
}
