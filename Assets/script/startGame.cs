using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startGame : MonoBehaviour {

	private InputField hidden_input;


	void Start () {
		ObjectManager.HideChildren ("NextLevel_Panel");
		//ObjectManager.HideChildren ("Restart_Panel");

		GameObject o = ObjectManager.getGameObjectByName ("credit");
		if (o) {
			o.SetActive (false);
	
			for (int i = 1; i < 4; i++) {
				o = ObjectManager.getGameObjectByName ("Level" + i.ToString ());
				o.SetActive (false);
			}
		}
			
		hidden_input = ObjectManager.getInputFieldByName ("input_user");

		if (!controller._WEB_GL) {
			if (hidden_input)
				hidden_input.enabled = false;
		}

		for (int i = 1; i < 7; i++) 
			ObjectManager.HideChildren ("Step" + i);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}