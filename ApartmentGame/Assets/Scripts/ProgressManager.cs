﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Use this to manage the tasks that the player has achieved
*/

public class ProgressManager : MonoBehaviour {
	
	public static ProgressManager Instance;
	
	
	//dialogue's tasks
	public static Dictionary<string, bool> tasks;
	public static Dictionary<string, bool> chapterTasks;
	public static List<string> taskList;

	
	void Awake(){
		if(Instance == null){
			DontDestroyOnLoad (gameObject);
			Instance = this;
		}
		else if(Instance!=this){
			Destroy(gameObject);
		}
	}
}
