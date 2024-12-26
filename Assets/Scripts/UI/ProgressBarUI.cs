using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject HasProgressGameObject;
    [SerializeField] private IHasProgress hasProgress;
    [SerializeField] private Image barImage;

    private void Start()
    {
        hasProgress = HasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            
            Debug.LogError("No IHasProgress component attached");
        }
        
        hasProgress.OnProgressChanged+= HasProgress_OnProgressChanged;
        
        barImage.fillAmount = 0f;
        
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f  )
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
