using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace SlimUI.ModernMenu{
	public class MainMenuNew : MonoBehaviour {
		Animator CameraObject;

		[Header("Loaded Scene")]
		[Tooltip("The name of the scene in the build settings that will load")]
		public string sceneName = ""; 

		public enum Theme {custom1, custom2, custom3};
		[Header("Theme Settings")]
		public Theme theme;
		int themeIndex;
		public FlexibleUIData themeController;

		[Header("Panels")]
		[Tooltip("The UI Panel parenting all sub menus")]
		public GameObject mainCanvas;
		[Tooltip("The UI Panel that holds the GAME window tab")]
		public GameObject PanelGame;
		[Tooltip("The UI Sub-Panel under KEY BINDINGS for GENERAL")]
		public GameObject PanelGeneral;

		// campaign button sub menu
		[Header("Menus")]
		[Tooltip("The Menu for when the MAIN menu buttons")]
		public GameObject mainMenu;
		[Tooltip("THe first list of buttons")]
		public GameObject firstMenu;
		[Tooltip("The Menu for when the PLAY button is clicked")]
		public GameObject playMenu;
		[Tooltip("The Menu for when the EXIT button is clicked")]
		public GameObject exitMenu;

		// highlights
		[Header("Highlight Effects")]
		[Tooltip("Highlight Image for when GAME Tab is selected in Settings")]
		public GameObject lineGame;



		[Header("LOADING SCREEN")]
		public GameObject loadingMenu;
		public Slider loadBar;
		public TMP_Text finishedLoadingText;

		void Start(){
			CameraObject = transform.GetComponent<Animator>();

			playMenu.SetActive(false);
			exitMenu.SetActive(false);

			SetThemeColors();
		}

		void SetThemeColors(){
			if(theme == Theme.custom1){
				themeController.currentColor = themeController.custom1.graphic1;
				themeController.textColor = themeController.custom1.text1;
				themeIndex = 0;
			}else if(theme == Theme.custom2){
				themeController.currentColor = themeController.custom2.graphic2;
				themeController.textColor = themeController.custom2.text2;
				themeIndex = 1;
			}else if(theme == Theme.custom3){
				themeController.currentColor = themeController.custom3.graphic3;
				themeController.textColor = themeController.custom3.text3;
				themeIndex = 2;
			}
		}


		public void ReturnMenu(){
			playMenu.SetActive(false);
		}

		public void NewGame(){
			if(sceneName != ""){
				StartCoroutine(LoadAsynchronously(sceneName));
				//SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
			}
		}

		public void  DisablePlayCampaign(){
			playMenu.SetActive(false);
		}

		public void Position2(){
			DisablePlayCampaign();
			CameraObject.SetFloat("Animate",1);
		}

		public void Position1(){
			CameraObject.SetFloat("Animate",0);
		}

		void DisablePanels(){
			PanelGame.SetActive(false);

			lineGame.SetActive(false);
			PanelGeneral.SetActive(false);
		}

		public void GamePanel(){
			DisablePanels();
			PanelGame.SetActive(true);
			lineGame.SetActive(true);
		}

		public void VideoPanel(){
			DisablePanels();
		}

		public void ControlsPanel(){
			DisablePanels();
		}


		public void MovementPanel(){
			DisablePanels();
		}

		// Are You Sure - Quit Panel Pop Up
		public void AreYouSure(){
			exitMenu.SetActive(true);
		}

		public void AreYouSureMobile(){
			exitMenu.SetActive(true);
		}


		IEnumerator LoadAsynchronously(string sceneName){ // scene name is just the name of the current scene being loaded
			AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
			operation.allowSceneActivation = false;
			mainCanvas.SetActive(false);
			loadingMenu.SetActive(true);

			while (!operation.isDone){
				float progress = Mathf.Clamp01(operation.progress / .9f);
				loadBar.value = progress;

				if(operation.progress >= 0.9f){
					finishedLoadingText.gameObject.SetActive(true);

					if(Input.anyKeyDown){
						operation.allowSceneActivation = true;
					}
				}
				
				yield return null;
			}
		}
	}
}