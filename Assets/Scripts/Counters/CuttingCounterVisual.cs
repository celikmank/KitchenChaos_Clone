using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string Cut = "Cut";

    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += cuttingCounter_OnCut;

    }

    private void cuttingCounter_OnCut(object sender, EventArgs e)
    { 
        animator.SetTrigger(Cut);
    }
}
