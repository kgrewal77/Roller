using System.Collections;
using System;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public GameObject[] possibleTiles;
	public Queue tileQueue;
	public int startingTiles;
	private Rigidbody rb;
	private int count;
	public float speedModifier;
	public Text countText;
	private int levelCount;
	private int maxLevels;
	private int tilePosition;
	private int tileTrigger;
	public int tileSize;
	private System.Random tileChooser;
	private int numTiles;


	void Start () {

		rb = GetComponent<Rigidbody> ();
		count = 0;
		countText.text = "Count: " + count.ToString ();
		levelCount = 1;
		maxLevels = 1;
		tileChooser = new System.Random();
		numTiles = possibleTiles.Length;
		tileQueue = new Queue();

		tilePosition = 0;
		generateStartingTile();
		tileTrigger = tileSize;

		for (int i = 1; i < startingTiles; i++) {

			tilePosition += tileSize;
			generateTile();
		}

	}

	void Update() {

		if (Input.GetKeyDown(KeyCode.Escape)) {

			SceneManager.LoadScene("Main Menu");
		}
	}

	void FixedUpdate() {

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speedModifier);

		if (this.transform.position.y < -5) {

			countText.text = "Game Over";
			StartCoroutine(SleepThenLoadScene("Level"+levelCount));
		}

		if (this.transform.position.z > tileTrigger) {

			tilePosition += tileSize;
			tileTrigger += tileSize;
			generateTile();

		}
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.CompareTag("PickUp")) {

			other.gameObject.SetActive (false);
			count++;
			countText.text = "Count: " + count.ToString ();
		} else if (other.gameObject.CompareTag("Gate")) {


			if (levelCount >= maxLevels) {
				countText.text = "Game Completed!";
				StartCoroutine(SleepThenLoadScene("Main Menu"));
			} else {
				countText.text = "Level Cleared!";
				levelCount++;
				StartCoroutine(SleepThenLoadScene("Level"+levelCount));
			}
		}
	}

	IEnumerator SleepThenLoadScene(string sceneName) {
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene(sceneName);
	}

	void generateTile() {

		int tileChoice = tileChooser.Next() % numTiles;
		GameObject nextTile = possibleTiles[tileChoice];
		tileQueue.Enqueue(Instantiate(nextTile, new Vector3(0, 0, tilePosition), Quaternion.identity));

		if (tileQueue.Count > startingTiles + 2) {

			UnityEngine.Object.Destroy((GameObject)tileQueue.Dequeue());
		}
	}

	void generateStartingTile() {

		tileQueue.Enqueue(Instantiate(possibleTiles[0],
			new Vector3(0, 0, tilePosition), Quaternion.identity));
	}
}
