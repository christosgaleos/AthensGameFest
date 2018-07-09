using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour{

    public void ChangeToScene (int changeTheScene){
        SceneManager.LoadScene (changeTheScene);
        Debug.Log("pressed");      
    }

}
