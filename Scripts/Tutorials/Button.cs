using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //I have to add it to make SceneManager work!!

public class Button : MonoBehaviour
{

    public UnityEngine.UI.Button buttonContinue;
    private float elapsedTime; //tempo che passa dall'inizio

    void Start()
    {

      buttonContinue = FindObjectOfType<UnityEngine.UI.Button>();
      buttonContinue.interactable = false;
    
    }

    private void Update() 
    {

        elapsedTime += Time.deltaTime; //a ogni update sommo il tempo passato da quel momento

        if (elapsedTime > 5f)
        {
            buttonContinue.interactable = true;
        }
        
    }
    

    
    [SerializeField] int levelNumber;
    public void UseButton()
    {
        string nextSceneName = GetSceneNameFromBuildIndex(levelNumber);

        if (nextSceneName == "Experiment")
        {
            ExperimentManager.Instance.StartCoroutine(ExperimentManager.Instance.LoadLevel());
        }
        else
        {
            SceneManager.LoadScene(levelNumber); //SceneManager.LoadScene is used to lead the current scene, in this case I want that when it reloads it goes to the newGameLevel
            // l'1 tra parentesi indica la scena 1 nel build settings, in questo caso la successiva alla scena del livello1
        }
    }
    
    public string GetSceneNameFromBuildIndex(int index)
     {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        return sceneName;
     }
}




