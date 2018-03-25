using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonsController : MonoBehaviour {
	
	public Button btn;
	private TouchScreenKeyboard keyboard;
	private InputField hidden_input;
	private Text timerText;


	void Start () {
		ObjectManager.BtnAddListener ("Canvas","btn_menu",gotoMenu);
		hidden_input = ObjectManager.getInputFieldByName ("input_user");
		timerText = ObjectManager.getTextByName ("time");

	}
	
	// Update is called once per frame
	void Update () {
		if (timerText && timerText.text == "00:00")
			ObjectManager.EnabledChildrenButton ("Numbers_Panel",false);
	}
		
	void gotoMenu(){
		SceneManager.LoadScene ("menu");
	}

	public void NumberClick(int num){

		if ((hidden_input.text.Length == 6) && (num != -1))
			return;

		if (!controller._sound_mute) {
			AudioSource _but_sound = ObjectManager.getGameObjectByName ("buttonSound" + num.ToString ()).GetComponent<AudioSource> () as AudioSource;
			if (_but_sound) {
				_but_sound.Stop ();
				_but_sound.Play ();
			}
		}
		if (num == -1) {
			if(hidden_input.text.Length > 0)
				hidden_input.text = hidden_input.text.Substring (0, hidden_input.text.Length - 1);
		} else {
			hidden_input.text += num.ToString ();
		}

	}


}
