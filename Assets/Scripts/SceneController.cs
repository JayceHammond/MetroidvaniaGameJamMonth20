using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Canvas pauseCanvas;
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(paused == false){
                paused = true;
                pauseCanvas.gameObject.SetActive(paused);
                Time.timeScale = 0;
            }else{
                paused = false;
                pauseCanvas.gameObject.SetActive(paused);
                Time.timeScale = 1;
            }
            
        }
    }
}
