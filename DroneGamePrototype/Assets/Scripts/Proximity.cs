using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Proximity : MonoBehaviour
{
    public string targetTag = "Wall";
    public Text proximityText;
    public float maxRange = 5f;

    private GameObject[] wallObjects;
    public GameObject[] proxyMeterUI;

    private void Start()
    {
        wallObjects = GameObject.FindGameObjectsWithTag(targetTag);
    }

    private void Update()
    {
        float closestDistance = Mathf.Infinity;
        
        foreach (GameObject wall in wallObjects)
        {
            float distance = Vector3.Distance(transform.position, wall.transform.position);
            
            if (distance < closestDistance)
            {
                closestDistance = distance;
            }
        }

        if (closestDistance <= maxRange)
        {
            float proximityValue = Mathf.Lerp(100, 0, closestDistance / maxRange);
            proximityText.text = Mathf.RoundToInt(proximityValue).ToString();
        }
        else
        {
            proximityText.text = "";
        }
    }
}
