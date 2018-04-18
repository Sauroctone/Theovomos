using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame()
	{
		//METTRE LE NOM DE LA SCENE DU JEU ICI ""
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void BackMenu()
	{
		//METTRE LE NOM DE LA SCENE MENU ""
		SceneManager.LoadScene("UI_scene");
	}

	public void GoProfile()
	{
		//METTRE LE NOM DE LA SCENE PROFILE ""
		SceneManager.LoadScene("UI_profile_scene");
	}

	public void GoCollection()
	{
		//METTRE LE NOM DE LA SCENE COLLECION ""
		SceneManager.LoadScene("UI_collection_scene");
	}

}
