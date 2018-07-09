using System.Collections;
using UnityEngine;

public class TQExitApp : MonoBehaviour {
    
    public void ExitApp(){
        Application.Quit();
        Debug.Log("log out");
    }
}
