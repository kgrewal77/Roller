using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private int count;
	public float speedModifier;
	public Text countText;
	private int levelCount;
	private int maxLevels;

	void Start () {

		rb = GetComponent<Rigidbody> ();
		count = 0;
		countText.text = "Count: " + count.ToString ();
		levelCount = 1;
		maxLevels = 1;
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
}
