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

	void Start () {

		rb = GetComponent<Rigidbody> ();
		count = 0;
		countText.text = "Count: " + count.ToString ();
	}

	void FixedUpdate() {

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speedModifier);

		if (this.transform.position.y < -5) {

			countText.text = "Game Over";
			StartCoroutine(RestartScene());
		} 
		
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.CompareTag("PickUp")) {

			other.gameObject.SetActive (false);
			count++;
			countText.text = "Count: " + count.ToString ();
		} else if (other.gameObject.CompareTag("Gate")) {

			countText.text = "Victory!";
			StartCoroutine(RestartScene());
		}
	}

	IEnumerator RestartScene() {
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene("MiniGame");
	}
}
