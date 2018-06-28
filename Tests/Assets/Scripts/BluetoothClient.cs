using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BluetoothClient : MonoBehaviour {

	public Text UITextElement;

	private string rawText;
	public static Vector3 accelerometer;
	public static Vector3 gyroscope;
	public static Vector3 magnetometer;

	// Use this for initialization
	void Start () {

		rawText = "";
		accelerometer = Vector3.zero; 
		gyroscope = Vector3.zero; 
		magnetometer = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

		rawText = HighRateTerminal.text;

		if (rawText != null) {
			
			rawText = rawText.Substring (6,rawText.IndexOf("FINAL")-6);

			string[] data = rawText.Split (new string[] {","} , StringSplitOptions.None);

			accelerometer = new Vector3(float.Parse(data[0]),float.Parse(data[1]),float.Parse(data[2]));
			gyroscope = new Vector3(float.Parse(data[3]),float.Parse(data[4]),float.Parse(data[5]));
			magnetometer = new Vector3(float.Parse(data[6]),float.Parse(data[7]),float.Parse(data[8]));

			UITextElement.text = rawText;
		}

//		UITextElement.text = "nicolas";
	}
}
