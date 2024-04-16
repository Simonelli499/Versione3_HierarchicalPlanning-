using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D coll;
    [SerializeField] GameObject checkmarkPrefab;
    public AudioManager audioManager;
    
    public MapManager mapManager;

    public bool isGoal = false;
    public bool isFirstGoal = false;
    public int color;
    public int shape;
    public int positionInLegend;
    public int x;
    public int y;
    public bool isGoalReal = false;
    public GameObject outline;


    public void SetGoalStatus()
    {
        isGoal = true;
        positionInLegend = mapManager.goalIndex;
        
        if (mapManager.goalIndex < 4)
        {   
            if (mapManager.goalIndex == 0)
            {
                isFirstGoal = true;
                CreateFirstGoalOutline();
            }

            if (mapManager.goalIndex > 0)
            {
                CreateGoalOutiline();
            }
        }
            
    }

    public void CreateFirstGoalOutline()
    {
        MapManager mapManager = FindObjectOfType<MapManager>();
        outline = Instantiate(mapManager.firstGoalBorder , transform.position, Quaternion.identity);
        outline.tag = "Outline";

        for (int i = 0; i < outline.transform.childCount; i++)
            outline.transform.GetChild(i).GetComponent<SpriteRenderer>().color = mapManager.colors[color];
    }

    

    public void CreateGoalOutiline()
    {
        MapManager mapManager = FindObjectOfType<MapManager>();
        outline = Instantiate(mapManager.otherGoalBorder, transform.position, Quaternion.identity);
        outline.tag = "Outline";
        //outline.transform.GetComponent<SpriteRenderer>().color = mapManager.colors[color];
        outline.transform.GetComponent<SpriteRenderer>().color = spriteRenderer.color;

    }


    public void Darken()
    {
        Color color = Color.black;
        spriteRenderer.color = color;
        AudioManager audioManager = AudioManager.GetInstance();
        if (audioManager != null)
        {
            audioManager.PlaySound("success");
        }

        outline.gameObject.SetActive(false);
    }

    public void ChangeColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }



    void OnTriggerEnter2D(Collider2D other)
    {

        if (mapManager.firstGoalTaken == true && !isFirstGoal && isGoal)
        {
            isGoal = false;
            DataCollector.Instance.AddEvent("GoalTaken");

            Darken();
            ExperimentManager.instance.CollectGoal();

        }
        else if (isFirstGoal && isGoal)
        {
            isFirstGoal = false;
            DataCollector.Instance.AddEvent("GoalTaken");
            Darken();

        //    GameObject[] otherOutlines = GameObject.FindGameObjectsWithTag("Outline");
       //     for (int i = 0; i < otherOutlines.Length; i++) otherOutlines[i].GetComponent<SpriteRenderer>().enabled = true;

            mapManager.firstGoalTaken = true;
            ExperimentManager.instance.CollectGoal();
        }
    }

    internal void SetActive(bool v)
    {
        throw new NotImplementedException();
    }
}


