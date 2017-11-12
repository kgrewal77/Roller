﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TextController : MonoBehaviour {

	private TextMesh txt;
	public bool IsStart;
	public bool IsQuit;
	public bool IsAbout;

	void Start(){
		txt = gameObject.GetComponent<TextMesh>();
		txt.color = Color.black;
	}

	public void OnMouseEnter()
     {
         txt.color = Color.red;
     }

	public void OnMouseExit()
     {
         txt.color = Color.black;
     }

     public void OnMouseDown()
     {
         if (IsStart) {

         	SceneManager.LoadScene("Level1");
         } else if (IsQuit) {

         	Application.Quit();
 		} else if (IsAbout) {

 		}
     }
}
