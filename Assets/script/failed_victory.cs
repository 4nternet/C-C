using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class failed_victory : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text t = ObjectManager.getTextByName ("credit_txt");
		t.text = controller.correct_number;

		t = ObjectManager.getTextByName ("player_score");
		t.text = controller.player_score;

		t = ObjectManager.getTextByName ("player_credit");
		t.text = controller.player_credit;



	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
