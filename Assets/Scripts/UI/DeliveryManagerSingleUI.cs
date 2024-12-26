using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeNameText;
    [SerializeField] private  Transform iconContainer;
    [SerializeField] private  Transform iconTemplate;
    
    public void SetRecipeSo(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;
    }
    
    
}
