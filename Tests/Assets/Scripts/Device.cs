using System;
using UnityEngine;
using System.Collections;
using TechTweaking.Bluetooth;
using System.Collections.Generic;

public class Device : MonoBehaviour {

	public BluetoothDevice device;
	public string statusText;
	public string deviceName;
	public string textRead;

	public static bool isActive;
	public static Vector3 gyroscope;
	public static Vector3 magnetometer;
	public static Vector3 accelerometer;

	// Use this for initialization
	void Start () {

		DontDestroyOnLoad (gameObject);

		statusText = "";
		deviceName = "";
		isActive = false;

		BluetoothAdapter.askEnableBluetooth ();//Ask user to enable Bluetooth
		BluetoothAdapter.OnDeviceOFF += HandleOnDeviceOff;
		BluetoothAdapter.OnDevicePicked += HandleOnDevicePicked; //To get what device the user picked out of the devices list
	}
	
	// Update is called once per frame
	void Update () {

		if (device.IsReading) {

			isActive = true;
			//polll all available packets
			BtPackets packets = device.readAllPackets ();

			if (packets != null) {

				for (int i = 0; i < packets.Count; i++) {

					//packets.Buffer contains all the needed packets plus a header of meta data (indecies and sizes) 
					//To parse a packet we need the INDEX and SIZE of that packet.
					int indx = packets.get_packet_offset_index (i);
					int size = packets.get_packet_size (i);

					string content = System.Text.ASCIIEncoding.ASCII.GetString (packets.Buffer, indx, size);
					content = content.Substring (6,content.IndexOf("FINAL")-6);

					string[] data = content.Split (new string[] {" "} , StringSplitOptions.None);

					accelerometer = new Vector3(float.Parse(data[0]),float.Parse(data[1]),float.Parse(data[2]));
					gyroscope = new Vector3(float.Parse(data[3]),float.Parse(data[4]),float.Parse(data[5]));
					magnetometer = new Vector3(float.Parse(data[6]),float.Parse(data[7]),float.Parse(data[8]));

					textRead = content;
				}
			}

		} 
		else {
			
		}
	}

	void HandleOnDeviceOff (BluetoothDevice dev){

		if (!string.IsNullOrEmpty (dev.Name))
			statusText = "Can't connect to " + dev.Name + ", device is OFF";
		else if (!string.IsNullOrEmpty (dev.Name)) {
			statusText = "Can't connect to " + dev.MacAddress + ", device is OFF";
		}
	}

	//############### UI BUTTONS RELATED METHODS #####################
	public void showDevices ()
	{
		BluetoothAdapter.showDevices ();//show a list of all devices//any picked device will be sent to this.HandleOnDevicePicked()
	}

	public void connect ()//Connect to the public global variable "device" if it's not null.
	{
		if (device != null) {
			device.normal_connect(false,false);
		}
	}

	public void disconnect ()//Disconnect the public global variable "device" if it's not null.
	{
		if (device != null)
			device.close ();
	}

	void HandleOnDevicePicked (BluetoothDevice device)//Called when device is Picked by user
	{

		this.device = device;//save a global reference to the device

		//this.device.UUID = UUID; //This is only required for Android to Android connection

//		/* 
//		 * setEndByte(10) will change how the read() method works.
//		 * 10 equals the char '\n' which is a "new Line" in Ascci representation, 
//		 * so the read() method will retun a packet that was ended by the byte 10, without including 10.
//		 * Which means read() will read lines while excluding the '\n' new line charachter.
//		 * If you don't use the setEndByte() method, device.read() will return any available data (line or not), then you can order/packatize them as you want.
//		 * 
//		 * Note: setEndByte will make reading lines or packest easier.
//		 */

		device.setEndByte (10);


		//Assign the 'Coroutine' that will handle your reading Functionality, this will improve your code style
		//Other way would be listening to the event Bt.OnReadingStarted, and starting the courotine from there
		device.ReadingCoroutine = ManageConnection;
		deviceName = device.Name;

	}

	//############### UnRegister Events  #####################
	void OnDestroy ()
	{
				BluetoothAdapter.OnDevicePicked -= HandleOnDevicePicked; 
				BluetoothAdapter.OnDeviceOFF -= HandleOnDeviceOff;
	}

	IEnumerator  ManageConnection (BluetoothDevice device){
		
		yield return null;
	}
}
