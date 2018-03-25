using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using System.Diagnostics;

public class controller : MonoBehaviour {

	public static bool _WEB_GL = false;

	private const float TIME_INIT = 70.0f;
	private const int SCORE_MAX = 50000;
	private  int CREDIT_MAX = (int)(SCORE_MAX * 0.1);
	private float timer = TIME_INIT;
	private int score = 0;
	private int credit = 0;
	private int score_levels = 0;

	public static bool _sound_mute = false;

	public static string correct_number="";
	public static string player_score="0";
	public static string player_credit="0";
	private string user_input = "";
	private string user_input_ = "";
	private InputField hidden_input;
	private int step = 1;
	private int level = 1;
	private int LEVEL_MAX = 1;
	private bool start = false;

	private Sprite _correct;
	private Sprite _exits;
	private Sprite _wrong;

	private Image _time_img;


	private bool checkPosition(int _position){
		return correct_number[_position-1].Equals(user_input[_position-1]);
	}
	private bool checkExists(string _input, int _position){
		return _input.IndexOf (user_input[_position-1]) != -1;
	}

	private bool check(){
		return user_input.Equals(correct_number);
	}
		
	private void CorrectNumber(){
		for (int i = 1; i < 7; i++) 
			ObjectManager.setTextByName ("n" + i , "");
		//UnityEngine.Debug.Log ("CorrectNumber() executed");
		GenerateNumber ();
	}

	private void Lost(){

		//start = false;
		
		for (int i = 1; i < 7; i++) 
				ObjectManager.setTextByName ("n" + i , "");
	
		hidden_input.enabled = false;
	
		player_score = "0";
		player_credit = credit.ToString ();
		SceneManager.LoadScene ("failed_scene");
	
	}

	private void Won(){

		for (int i = 1; i < 7; i++) 
			ObjectManager.setTextByName ("n" + i, "");
		
		hidden_input.enabled = false;
		player_credit = credit.ToString ();
		player_score = score.ToString ();
		SceneManager.LoadScene ("victory_scene");
	}


	public void SetLevelMax(string _max){
		LEVEL_MAX = int.Parse (_max);
	}

	public void NextLevel(){
		if (level == LEVEL_MAX) {
			ObjectManager.HideChildren ("NextLevel_Panel");
			return;
		}
		if(_WEB_GL && hidden_input)
			hidden_input.enabled = false;
		hidden_input.text="";
		step = 1;
		timer = TIME_INIT;
		setTime (timer/70);
		credit = 0;
		score_levels = score;
		ObjectManager.HideChildren ("NextLevel_Panel");
		ObjectManager.setTextByName ("time", toTime(timer));
		ObjectManager.setTextByName ("Credit", credit.ToString());
		ObjectManager.setTextByName ("score", score.ToString());

		for (int i = 1; i < 7; i++) {
			ObjectManager.HideChildren ("Step" + i);
			ObjectManager.setTextByName ("n" + i , "");
		}
		level++;
		ObjectManager.EnabledChildrenButton ("Numbers_Panel",true);

		if(_WEB_GL && hidden_input)
			hidden_input.enabled = true;
	}

	public void Exit(){
		Application.Quit ();
	}

	void PlaySound(string n){
		if (!_sound_mute) {
			AudioSource _but_sound = ObjectManager.getGameObjectByName ("buttonSound" + n).GetComponent<AudioSource> () as AudioSource;
			if (_but_sound) {
				_but_sound.Stop ();
				_but_sound.Play ();
			}
		}
	}

	public void Restart(){

		start = false;

		level=1;
		if (hidden_input)
			hidden_input.text = "";
		else
			hidden_input = null;
	
		step = 1;
		timer = TIME_INIT;
		if(_time_img)
			setTime (timer/70);
		credit = 0;
		score_levels = 0;
		score = SCORE_MAX;

		ObjectManager.setTextByName ("time", toTime(timer));
		ObjectManager.setTextByName ("Credit", credit.ToString());
		ObjectManager.setTextByName ("score", score.ToString());

		for (int i = 1; i < 7; i++) {
			ObjectManager.HideChildren ("Step" + i);
			ObjectManager.setTextByName ("n" + i , "");

		}
		ObjectManager.EnabledChildrenButton ("Numbers_Panel",true);

		if (SceneManager.GetActiveScene ().name=="failed_scene" || (SceneManager.GetActiveScene ().name == "victory_scene")) {
			SceneManager.LoadScene ("play_scene");
		}
		if(_WEB_GL && hidden_input)
			hidden_input.enabled = true;

	}

	public void GenerateNumber(){
		correct_number = "";
		for (int i = 0; i < 6; i++)
			correct_number += Random.Range (0, 9).ToString();
	}

	public void Validate(){
		user_input = hidden_input.text;
		if (user_input.Length != 6)
			return;
		hidden_input.text = "";
		if(_WEB_GL)
			hidden_input.enabled = false;
		

		ObjectManager.EnabledChildrenButton ("Numbers_Panel",false);
		
		if (step == 1) 
			start = true;

		if (!_sound_mute ) {
			AudioSource _but_sound = ObjectManager.getGameObjectByName ("validate").GetComponent<AudioSource> () as AudioSource;
			if (_but_sound) {
				_but_sound.Stop ();
				_but_sound.Play ();
			}
		}

		string _tmp = correct_number;

		for (int i = 1; i < 7; i++)
			if(checkPosition(i))
				_tmp = strReplace(_tmp,_tmp[i-1],'#');
		for (int i = 1; i < 7; i++) {
			if (checkPosition (i)) {
				ObjectManager.setImage ("Step" + step , "s" + step + "_n" + i + "_img" , "correct_sprite" );
			} else {
				if (checkExists (_tmp, i)) {
					_tmp = strReplace(_tmp,user_input[i-1],'#');
					ObjectManager.setImage ("Step" + step , "s" + step + "_n" + i + "_img" , "exists_sprite" );
				} else {
					ObjectManager.setImage ("Step" + step , "s" + step + "_n" + i + "_img" , "wrong_sprite" );
				}
			}
			ObjectManager.setTextByNameInactive("Step" + step , "s" + step + "_n" + i , user_input.Substring (i - 1, 1));
		}

		ObjectManager.ShowChildren ("Step" + step);
		if (check ()) {
			start = false;

			if (level == LEVEL_MAX) {
				Won ();
				return;
			}
			CorrectNumber ();
			ObjectManager.ShowChildren ("NextLevel_Panel");
			return;
		}
		else {
			step++;
			if (step > 6  || timer == 0) {
				start = false;

				Lost ();
				return;
			}
		}

		ObjectManager.EnabledChildrenButton ("Numbers_Panel",true);

		if(_WEB_GL)
			hidden_input.enabled = true;

	}

	public void GetInput(string guess){

		if (level > LEVEL_MAX) {
			start = false;

			ObjectManager.EnabledChildrenButton ("Numbers_Panel",false);
			Won ();
			return;
		}

		if (step > 6  || timer == 0) {
			start = false;

			ObjectManager.EnabledChildrenButton ("Numbers_Panel",false);
			Lost ();
			return;
		}


		if (_WEB_GL) {
			string _n = "";
			int len = hidden_input.text.Length;
			int len_last = user_input_.Length;

			if (len_last < len) {
				_n = hidden_input.text.Substring (len - 1, 1);
				PlaySound (_n);
			} else {
				if (len_last == len + 1)
					PlaySound ("-1");
			}
		}


		for (int i=1; i < (guess.Length + 1); i++) {
			ObjectManager.setTextByName ("n" + i , guess.Substring (i - 1, 1));
		}

		for (int i = guess.Length + 1; i < 7; i++) {
			ObjectManager.setTextByName ("n" + i , "");
		}
		user_input_ = guess;

	}


	private string toTime(float timeSec){
		int _sec = (int)Mathf.Floor (timeSec);
		int _min = (int)Mathf.Floor(_sec / 60);
		_sec -= _min * 60;
		return "00".Substring (0, 2 - _min.ToString ().Length) + _min.ToString() + ":" + "00".Substring (0, 2 - _sec.ToString ().Length) + _sec.ToString();
	}

	private string strReplace(string str,char c,char k){
		if (str.Length > 0) {
			int j = str.IndexOf(c);
			return j >= 0 ? str.Substring(0,j) + k.ToString() + str.Substring(j+1) : str;
		}
		return "";
	}
		
	private void resetTime(){
		if (!_time_img)	_time_img = ObjectManager.getImageByName ("time_full");
		if (_time_img)	_time_img.fillAmount = 1;

	}
	private void setTime(float _amount){
		if (!_time_img) _time_img = ObjectManager.getImageByName ("time_full");
		if (_time_img) _time_img.fillAmount = _amount;
	}

	void Mute(){
		_sound_mute = !_sound_mute;
		Canvas can = ObjectManager.getCanvasByName ("Canvas");
		if (can) {
			AudioSource a = can.GetComponent<AudioSource> () as AudioSource;
			if (a)
				a.mute = _sound_mute;
			Button btn = ObjectManager.getGameObjectByName ("btn_mute").GetComponent<Button>() as Button;
			if (_sound_mute) {
				Button btn_off = ObjectManager.getGameObjectByName ("btn_mute_off").GetComponent<Button>() as Button;
				btn.image.sprite = btn_off.image.sprite;
			} else {
				Button btn_on = ObjectManager.getGameObjectByName ("btn_mute_on").GetComponent<Button>() as Button;
				btn.image.sprite = btn_on.image.sprite;
			}
		}
	}

	void Start(){
		hidden_input = ObjectManager.getInputFieldByName ("input_user");
		if (SceneManager.GetActiveScene().name == "play_scene") GenerateNumber ();
		score = SCORE_MAX;
		ObjectManager.setTextByName ("time", toTime(timer));
		ObjectManager.setTextByName ("Credit", credit.ToString());
		ObjectManager.setTextByName ("score",  score.ToString());
		ObjectManager.BtnAddListener ("NextLevel_Panel","btn_gotoNextLevel",NextLevel);
		ObjectManager.BtnAddListener ("Canvas","btn_restart",Restart);
		ObjectManager.BtnAddListener ("Canvas","btn_exit",Exit);
		ObjectManager.BtnAddListener ("Canvas","btn_mute",Mute);

		_time_img = ObjectManager.getImageByName ("time_full");
		setTime (timer/70);

	} 

	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape))
			if (SceneManager.GetActiveScene ().name=="play_scene")
				SceneManager.LoadScene ("menu");

		if (start && timer <= 0) {
			start = false;
			ObjectManager.EnabledChildrenButton ("Numbers_Panel",false);
			Lost ();
			return;
		}
			
		if (start && timer > 0) {
			if (timer > Time.deltaTime) {
				timer -= Time.deltaTime;
				credit = (int)(CREDIT_MAX - Mathf.Floor (CREDIT_MAX * (timer / TIME_INIT)));
				score = score_levels + (int)(Mathf.Floor (SCORE_MAX * (timer / TIME_INIT)));
				ObjectManager.setTextByName ("time", toTime (timer));
				ObjectManager.setTextByName ("Credit", credit.ToString());
				ObjectManager.setTextByName ("score", score.ToString ());
			} else
				timer = 0;
			setTime (timer/70);
		}

		if (hidden_input && hidden_input.text.Length != 6)
			ObjectManager.EnabledChildButton ("Numbers_Panel", "confirm", false);
		else 
			ObjectManager.EnabledChildButton ("Numbers_Panel", "confirm", true);

		if (_WEB_GL && hidden_input.enabled) {
			ObjectManager.setFocus (hidden_input);
			hidden_input.caretPosition = hidden_input.text.Length;
		}

		if(_WEB_GL && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))){
			Validate ();
		}
			

	}



}
