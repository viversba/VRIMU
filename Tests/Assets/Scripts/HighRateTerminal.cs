using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TechTweaking.Bluetooth;
using UnityEngine.UI;
using UnityEngine.VR;
using UnityEngine.XR;

public class HighRateTerminal : MonoBehaviour
{
	
	public Text devicNameText;
	public static string text;
	public ScrollTerminalUI readDataText;//ScrollTerminalUI is a script used to control the ScrollView text
	public bool isActive;
	public Text dataToSend;
	public GameObject InfoCanvas;
	public GameObject DataCanvas;

	private Device deviceInstance;
	private LevelManager levelManager;

	void Awake ()
	{
		
		text = null;
		isActive = false;
		XRSettings.enabled = false;
		deviceInstance = FindObjectOfType<Device> ();
		levelManager = FindObjectOfType<LevelManager> ();
	}

	void Update(){

		if (deviceInstance.device.IsReading) {
			//Switch to Terminal View
			isActive = true;
			InfoCanvas.SetActive (false);
			DataCanvas.SetActive (true);



			readDataText.set (deviceInstance.textRead);

//			//polll all available packets
//			BtPackets packets = device.device.readAllPackets ();
//
//			if (packets != null) {
//
//				/*
//				 * parse packets, packets are ordered by indecies (0,1,2,3 ... N),
//				 * where Nth packet is the latest packet and 0th is the oldest/first arrived packet.
//				 * 
//				 */
//
//				for (int i = 0; i < packets.Count; i++) {
//
//					//packets.Buffer contains all the needed packets plus a header of meta data (indecies and sizes) 
//					//To parse a packet we need the INDEX and SIZE of that packet.
//					int indx = packets.get_packet_offset_index (i);
//					int size = packets.get_packet_size (i);
//
//					string content = System.Text.ASCIIEncoding.ASCII.GetString (packets.Buffer, indx, size);
//					content = content.Substring (6,content.IndexOf("FINAL")-6);
//
//					string[] data = content.Split (new string[] {" "} , StringSplitOptions.None);
//
//					accelerometer = new Vector3(float.Parse(data[0]),float.Parse(data[1]),float.Parse(data[2]));
//					gyroscope = new Vector3(float.Parse(data[3]),float.Parse(data[4]),float.Parse(data[5]));
//					magnetometer = new Vector3(float.Parse(data[6]),float.Parse(data[7]),float.Parse(data[8]));
//
////					text = content;
//
//					readDataText.set (content);
//				}
//			}
				
		} 
		else {

			//Switch to Menue View after reading stoped
			DataCanvas.SetActive (false);
			InfoCanvas.SetActive (true);	
		}
	}
	
	//############### UI BUTTONS RELATED METHODS #####################
	public void showDevices ()
	{
		deviceInstance.showDevices ();
	}
	
	public void connect ()//Connect to the public global variable "device" if it's not null.
	{
		deviceInstance.connect ();
	}
	
	public void disconnect ()//Disconnect the public global variable "device" if it's not null.
	{
		deviceInstance.disconnect ();
	}


	public void VerifyDeviceAndChangeScene(string name){
	
		if (deviceInstance.device != null) {

			levelManager.LoadScene (name);
		}
	}
}
