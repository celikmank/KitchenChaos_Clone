using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject StoveOnGameObject;
    [SerializeField] private GameObject ParticleGameObject;

    private void Start()
    {
        stoveCounter.onStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual =e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;
        StoveOnGameObject.SetActive(showVisual);
        ParticleGameObject.SetActive(showVisual);
    }
}