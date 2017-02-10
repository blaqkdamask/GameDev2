﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class npcDialogue : MonoBehaviour {
	
	Dialogue dialogue;
	
	GameObject Name;
	GameObject nodeText;
	GameObject option1;
	GameObject option2;
	GameObject option3;
	GameObject exit;
	
	private int select = -2;
	int nodeID = -1;
	
	public string Path;
	public GameObject dialogueWindow;
	
	// Use this for initialization
	void Start () {
		//load the dialogue from the given path
		dialogue = Dialogue.Load(Path);
		
		//find the required components and initialize the private variables
		var canvas = GameObject.Find("Canvas");
		
		dialogueWindow = Instantiate<GameObject>(dialogueWindow);
		dialogueWindow.transform.SetParent(canvas.transform, false);
		
		RectTransform windowTrans = (RectTransform)dialogueWindow.transform;
		windowTrans.localPosition = new Vector3(0,0,0);
		
		
		Name = dialogueWindow.transform.Find("[npc]").gameObject;
		option1 = dialogueWindow.transform.Find("[option 1]").gameObject;
		option2 = dialogueWindow.transform.Find("[option 2]").gameObject;
		option3 = dialogueWindow.transform.Find("[option 3]").gameObject;
		nodeText = dialogueWindow.transform.Find("[dialogue]").gameObject;
		exit = dialogueWindow.transform.Find("[end convo]").gameObject;
		
		//add an action to the exit button
		exit.GetComponent<Button>().onClick.AddListener(delegate {
				setSelect(-1);
		});
		
		dialogueWindow.SetActive(false);
		
		//to test
		//move this runDialogue to a different function later
		runDialogue();
		
	}
	
	public void runDialogue(){
		StartCoroutine(run());
	}
	
	//method for selecting the next dialogue node to load
	public void setSelect(int x){
		select = x;
	}
	
	//coroutine for displaying the text, add something to allow the player to skip
	//forward later
	//handle text wrapping too
	private IEnumerator DisplayText(string displayText){
		int strLen = displayText.Length;
		int index = 0;
		
		nodeText.GetComponent<Text>().text = "";
		
		while(true){
			nodeText.GetComponent<Text>().text += displayText[index];
			index++;
			
			if(index<strLen){
				//play a sound potentially
				//wait for a moment before adding next character
				yield return new WaitForSeconds(0.05f);
			}
			else{
				break;
			}
		}
	}
	
	//update the text
	private void updateText(Node node){
		Name.GetComponent<Text>().text = node._name;
		
		StartCoroutine(DisplayText(node._text));
		
		//nodeText.GetComponent<Text>().text = node._text;
		
		option1.SetActive(false);
		option2.SetActive(false);
		option3.SetActive(false);
		
		//loop through all of this node's possible options and display them, currently
		//a maximum of 3 options per node
		for(int i=0;i<node._options.Count/*||i<2for later expansion?*/;i++){
			switch(i){
				case 0:
					updateButton(option1, node._options[i]);
				break;
				
				case 1:
					updateButton(option2, node._options[i]);
				break;
				
				case 2:
					updateButton(option3, node._options[i]);
				break;
				
			}
		}
	}
	
	//make the buttons visible
	private void updateButton(GameObject button, dialogueOption option){
		button.SetActive(true);
		button.GetComponentInChildren<Text>().text = option._text;
		button.GetComponent<Button>().onClick.AddListener(delegate {
			Debug.Log(option._dest);
			setSelect(option._dest);});
	}
	
	//run the dialogue tree coroutine
	public IEnumerator run(){
		dialogueWindow.SetActive(true);
		
		nodeID = 0;
		
		//while the node isn't an exit node...
		while(nodeID!=-1){
			updateText(dialogue._nodes[nodeID]);
			
			select = -2;
			while(select == -2){
				yield return new WaitForSeconds(0.25f);
			}
			
			nodeID = select;
		}
		dialogueWindow.SetActive(false);
	}
	
	//shake the dialogue box, maybe play a noise
	void shake(){
		
	}
	
}