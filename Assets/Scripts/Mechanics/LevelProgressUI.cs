using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelProgressUI : MonoBehaviour
{
    [Header("UI references:")] 
    [SerializeField] private Image uiFillImage;
    [SerializeField] private Image icon;
    
    // Start is called before the first frame update
    [Header("Player & Endline references:")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform endLineTransform;
    //[SerializeField] private Transform iconTransform;

    private Vector3 endlinePosition;
    private Vector3 iconPosition;
    private float fullDistance;
    void Start()
    {
        iconPosition = icon.transform.position;
        endlinePosition = endLineTransform.position;
        fullDistance = GetDistance();
    }

    private float GetDistance()
    {
        return Vector3.Distance(playerTransform.position, endlinePosition);
    }

    private void UpdateProgressFill(float value)
    {
        uiFillImage.fillAmount = value;
        
    }

    // Update is called once per frame
    void Update()
    {
        float newDistance = GetDistance();
        //Debug.Log(newDistance);
        float progressValue = Mathf.InverseLerp(fullDistance, 0f, newDistance);
        UpdateProgressFill(progressValue);
            GameObject.Find("icon").GetComponent<Transform>().position= new Vector3(progressValue*1920, 30, 0);
    }
}
