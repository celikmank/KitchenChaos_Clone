using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisiual : MonoBehaviour
{
    [SerializeField] private Transform CounterTopPoint;
    [SerializeField] private Transform PlateVisualPrefab;
    [SerializeField] private PlatesCounter platesCounter;

    private List<GameObject> plateVisualsGameObjectsList;

    private void Awake()
    {
        plateVisualsGameObjectsList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlatesSpawned  += PlatesCounterOnOnPlatesSpawned;
        platesCounter.OnPlatesRemoved += PlatesCounterOnOnPlatesRemoved;
    }

    private void PlatesCounterOnOnPlatesRemoved(object sender, EventArgs e)
    {
        GameObject PlateGameObject = plateVisualsGameObjectsList[plateVisualsGameObjectsList.Count - 1];
        plateVisualsGameObjectsList.Remove(PlateGameObject);
        Destroy(PlateGameObject);
    }

    private void PlatesCounterOnOnPlatesSpawned(object sender, EventArgs e)
    {
        Transform platesVisualTransform = Instantiate(PlateVisualPrefab, CounterTopPoint);

        float plateOfsetY = .1f;
        platesVisualTransform.localPosition=new Vector3(0f,plateOfsetY *plateVisualsGameObjectsList.Count,0f);
        plateVisualsGameObjectsList.Add(platesVisualTransform.gameObject);
    }
} 
