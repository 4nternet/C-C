using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ObjectManager.BtnAddListener ("Canvas","play_btn",Play);
		ObjectManager.BtnAddListener ("Canvas","exit_btn",Exit);
	}
	
	// Update is called once per frame
	void Play () {
		SceneManager.LoadScene ("play_scene");
	}

	void Exit() {
		Application.Quit ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}
}
