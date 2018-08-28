using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Rotator : MonoBehaviour {

	public float movspeed = 5f;
	public float turspeed = 0.0001f;


	SerialPort serial = new SerialPort("COM3",9600);

	// Use this for initialization

	// Update is called once per frames
	void Update () {
		if (!serial.IsOpen)
			serial.Open ();
		int data = int.Parse(serial.ReadLine ());
        print (data);
		switch (data) {
		case 1:
			transform.Translate ( -movspeed*Time.deltaTime*10, 0, 0);
			break;
		case 0:
			transform.Rotate(Vector3.down, turspeed*Time.deltaTime*6, Space.World);
			break;
		case 3:
			transform.Rotate(Vector3.up, turspeed*Time.deltaTime*6, Space.World);
			break;
		}

	}
}
