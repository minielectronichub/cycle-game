using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.IO.Ports; //Library to read our ardunio data
using System.Collections;//to use Ienumarator in our code

namespace UnityStandardAssets.Vehicles.Car
{
	[RequireComponent(typeof (CarController))]
	public class CarUserControl : MonoBehaviour
	{
		private CarController m_Car; // the car controller we want to use

		SerialPort seri = new SerialPort("COM3",9600);//define our port
		private void Awake()
		{
			// get the car controller
			seri.Open();//open our port
			StartCoroutine(ReadDataFromSerialPort());//start loop
			m_Car = GetComponent<CarController>();

		}

		IEnumerator ReadDataFromSerialPort(){
			while(true){//loop
				string[] values = seri.ReadLine ().Split (',');//we split our string value by , because we write string as carspeed,cartotation in our ardunio codes
				v = (float.Parse(values[0])) / 100;
				h = (float.Parse (values [1])) / 100;
				yield return new WaitForSeconds (.01f);//waiting seconds to read data. It should be same as ardunio code loop delay
			}
		}



		float h;//horizontal (car direction)
		float v;//vertical (car speed)
		private void FixedUpdate()
		{
			// pass the input to the car!
			//Close old codes which created by unity team
			//float h = CrossPlatformInputManager.GetAxis("Horizontal");
			//float v = CrossPlatformInputManager.GetAxis("Vertical");
			#if !MOBILE_INPUT
			float handbrake = CrossPlatformInputManager.GetAxis("Jump");
			m_Car.Move(h, v, v, handbrake);
			#else
			m_Car.Move(h, v, v, 0f);
			#endif
		}
	}
}