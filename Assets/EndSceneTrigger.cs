using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class EndSceneTrigger : MonoBehaviour {
    public VideoPlayer videoPlayer;
    
	// Use this for initialization
	void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            videoPlayer.Play();
            print("playing");
        }
    }
}
