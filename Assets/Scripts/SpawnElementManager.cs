using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElementManager : MonoBehaviour {
	public GameObject elementFire;
	public GameObject elementWater;
	public GameObject elementAir;
	public GameObject elementEarth;

	GameObject elementToSpawn;
	int randomValue;
	float spawnTime;

	bool spawn;
	Vector3 placeToSpawn;

	GameObject[] elementSpawnedList;
	int listIndice;

	int indiceFire;
	int indiceWater;
	int indiceAir;
	int indiceEarth;

	int numberFire;
	int numberWater;
	int numberAir;
	int numberEarth;

	float time;

	bool switchCase;

	GameObject[] fireList;
	GameObject[] waterList;
	GameObject[] airList;
	GameObject[] earthList;

	int elementNumber;

	GameObject test;
	GameObject test2;

	// Use this for initialization
	void Start () {
		switchCase = false;
		elementSpawnedList = new GameObject[12];
		listIndice = 0;
		spawn = true;
		spawnTime = 4f;

		indiceFire = 30;
		indiceWater = 55;
		indiceAir = 80;
		indiceEarth = 105;

		numberFire = 0;
		numberWater = 0;
		numberAir = 0;
		numberEarth =0;

		ChooseElementManager ();
		ChooseElementManager ();

		time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		time = Time.timeSinceLevelLoad;
		//print (time);
		if (time >= 20f && time < 40f) {
			spawnTime = 3f;
		} else if (time >= 40f && time < 60f) {
			spawnTime = 2.5f;
		} else if (time >= 60f && time < 80f) {
			spawnTime = 2f;
		} else if (time >= 80f) {
			spawnTime = 1.5f;
		}

		fireList = GameObject.FindGameObjectsWithTag ("Fire");
		waterList = GameObject.FindGameObjectsWithTag ("Water");
		airList = GameObject.FindGameObjectsWithTag ("Air");
		earthList = GameObject.FindGameObjectsWithTag ("Earth");

		elementNumber = fireList.Length + waterList.Length + airList.Length + earthList.Length;

		if (elementNumber < 9) {
			if (spawn == true) {
				ChooseElementManager ();
				ChooseElementManager ();
				StartCoroutine (SpawnElement ());
			}
		}
		/*if (Input.GetMouseButtonDown(0)){
			test = GameObject.FindGameObjectWithTag ("Air");
			if (test == null) {
				test = GameObject.FindGameObjectWithTag ("Earth");
			}
			Destroy (test);
		}
		if (Input.GetMouseButtonDown(1)){
			test2 = GameObject.FindGameObjectWithTag ("Water");
			if (test2 == null) {
				test2 = GameObject.FindGameObjectWithTag ("Fire");
			}
			Destroy (test2);
		}
        */
	}

	IEnumerator SpawnElement () {
		spawn = false;
		yield return new WaitForSeconds (spawnTime);
		spawn = true;
	}

	void ChooseElementManager () {
		IndiceChange ();
		randomValue = Random.Range (0, indiceEarth);

		if (randomValue < indiceFire) {
			elementToSpawn = elementFire;
		}
		else if (randomValue >= indiceFire && randomValue < indiceWater) {
			elementToSpawn = elementWater;
		}
		else if (randomValue >= indiceWater && randomValue < indiceAir) {
			elementToSpawn = elementAir;
		}
		else if (randomValue >= indiceAir) {
			elementToSpawn = elementEarth;
		}

		PlaceToSpawn ();
		Instantiate (elementToSpawn, placeToSpawn, transform.rotation);
		if (listIndice > 11) {
			listIndice = 0;
		} else {
			elementSpawnedList [listIndice] = elementToSpawn;
			listIndice++;
		}


	}

	void PlaceToSpawn () {
		if (!switchCase) {
			placeToSpawn = new Vector3 (Random.Range (-2.5f, 2.6f), Random.Range (-0.2f, 0.6f), -1f);
			switchCase = true;
		} else {
			placeToSpawn = new Vector3 (Random.Range (-2.5f, 2.6f), Random.Range (-0.5f, 0.3f), -1f);
			switchCase = false;
		}
	}

	void IndiceChange () {
		numberFire = 0;
		numberWater = 0;
		numberAir = 0;
		numberEarth =0;

		indiceFire = 30;
		indiceWater = 55;
		indiceAir = 80;
		indiceEarth = 105;

		for (int i = 0; i < 12; i++) {
			if (elementSpawnedList [i] == elementFire) {
				numberFire += 1;
			}
			else if (elementSpawnedList [i] == elementWater) {
				numberWater += 1;
			}
			else if (elementSpawnedList [i] == elementAir) {
				numberAir += 1;
			}
			else if (elementSpawnedList [i] == elementEarth) {
				numberEarth += 1;
			}
		}

		indiceFire = indiceFire - (numberFire * 5);
		indiceWater = indiceWater - (numberWater * 5);
		indiceAir = indiceAir - (numberAir * 5);
		indiceEarth = indiceEarth - (numberEarth * 5);

	}
}
