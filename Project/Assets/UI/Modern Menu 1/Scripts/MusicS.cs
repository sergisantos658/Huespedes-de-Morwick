using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

	public class MusicS: MonoBehaviour
	{

		// sliders
		public GameObject musicSlider;
		


		public void Start()
		{
			

			// check slider values
			musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");


			// check full screen
			
			
		}


		public void MusicSlider()
		{
			//PlayerPrefs.SetFloat("MusicVolume", sliderValue);
			PlayerPrefs.SetFloat("MusicVolume", musicSlider.GetComponent<Slider>().value);
		}
	}
