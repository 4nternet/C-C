using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


	public static class ObjectManager{


		public static void setFocus(InputField _input){
			if (_input) {
				_input.Select ();
				_input.ActivateInputField ();
			}
		}
		public static void getGameObjectInScene(string scene_name, string go_name){
			Scene sc = SceneManager.GetSceneByName (scene_name);
		sc.GetRootGameObjects ().GetEnumerator ();

		}
		public static Text getTextByName(string _name){
			GameObject go = GameObject.Find (_name);
			if (go) return  go.GetComponent<Text> () as Text;
			return null;
		}

		public static Image getImageByName(string _name){
			GameObject go = GameObject.Find (_name);
			if (go) return  go.GetComponent<Image> () as Image;
			return null;
		}

		public static InputField getInputFieldByName(string _name){
			GameObject go = GameObject.Find (_name);
			if (go) return GameObject.Find (_name).GetComponent<InputField> () as InputField;
			return null;
		}

		public static Canvas getCanvasByName(string _name){
			GameObject go = GameObject.Find (_name);
			if (go) return go.GetComponent<Canvas> () as Canvas;
			return null;
		}

		public static GameObject getGameObjectByName(string _name){
			GameObject go = GameObject.Find (_name);
			if (go) return GameObject.Find (_name);
			return null;
		}

		public static void setTextByName(string _name,string _text){
			Text txt = getTextByName (_name);
			if (txt) txt.text = _text;
		}

		public static void setTextByNameInactive(string _parent, string _name,string _text){
			GameObject Objtxt = getInactiveGameObjectByName(_parent,_name);
			Text txt = Objtxt.GetComponent<Text> () as Text;
			if (txt) txt.text = _text;
		}
		

		public static void HideChildren(string _name){
			GameObject parent = getGameObjectByName (_name);
			if (parent) {
			Component[] children = parent.GetComponentsInChildren (typeof(Component), true);
			foreach (Component child in children) {
				if (child.name != _name) {
					child.gameObject.SetActive (false);
				}
			}
			}
		}

		public static void ShowChildren(string _name){
			GameObject parent = getGameObjectByName (_name);
			if (parent) {
			Component[] children = parent.GetComponentsInChildren (typeof(Component), true);
			foreach (Component child in children) {
				if (child.name != _name) {
					child.gameObject.SetActive (true);
				}
			}
		}
		}
		public static void ShowChild(string _name,string _child){
			GameObject parent = getGameObjectByName (_name);
			if (parent) {
			Component[] children = parent.GetComponentsInChildren (typeof(Component), true);
			foreach (Component child in children) {
				if (child.name == _child) {
					child.gameObject.SetActive (true);
				}
			}
		}
		}
		public static void HideChild(string _name, string _child){
			GameObject parent = getGameObjectByName (_name);
		if (parent) {
			Component[] children = parent.GetComponentsInChildren (typeof(Component), true);
			foreach (Component child in children) {
				if (child.name == _child) {
					child.gameObject.SetActive (false);
				}
			}
		}
		}

		public static GameObject getInactiveGameObjectByName(string _parent,string _name){
			GameObject parent = getGameObjectByName (_parent);
		if (parent) {
			Component[] children = parent.GetComponentsInChildren (typeof(Component), true);
			foreach (Component child in children) {
				if (child.name == _name)
					return child.gameObject;
			}
		}
			return null;
		}


		
	public static void setColor(string _parent,string _name,Color32 color){
		Text _txtnum = ObjectManager.getInactiveGameObjectByName(_parent, _name).GetComponent<Text> () as Text;
		_txtnum.color = color;
	}
	public static void setImage(string _parent,string _name, string _sprite_name ){
		Image _img = ObjectManager.getInactiveGameObjectByName(_parent, _name).GetComponent<Image> () as Image;
		Image _img_tmp = ObjectManager.getInactiveGameObjectByName("Canvas", _sprite_name).GetComponent<Image> () as Image;
		_img.sprite = _img_tmp.sprite;
	}

	public static void BtnAddListener(string _parent,string _name, UnityEngine.Events.UnityAction _controller){
		GameObject go = getInactiveGameObjectByName (_parent, _name);
		Button btn_gotoNextLevel;
		if (go)
			btn_gotoNextLevel = go.GetComponent<Button> ();
		else
			btn_gotoNextLevel = null;
		if (btn_gotoNextLevel) btn_gotoNextLevel.onClick.AddListener (_controller);
	}

	public static void EnabledChildrenButton(string _name, bool _enabled){
		GameObject parent = getGameObjectByName (_name);
		if (parent) {
			Component[] children = parent.GetComponentsInChildren (typeof(Button), true);
			foreach (Component child in children) {
				if (child.name != _name) {
					Button _btn = child.gameObject.GetComponent<Button> ();
					_btn.enabled = _enabled;
				}
			}
		}
	}
	public static void EnabledChildButton(string _name,string _button, bool _enabled){
		GameObject parent = getGameObjectByName (_name);
		if (parent) {
			Component[] children = parent.GetComponentsInChildren (typeof(Button), true);
			foreach (Component child in children) {
				if (child.name == _button) {
					Button _btn = child.gameObject.GetComponent<Button> ();
					_btn.enabled = _enabled;
				}
			}
		}
	}
	
}

