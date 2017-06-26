using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour {

    public Image damageImage;
    public float flashSpeed = 5f;
    public Color32 flashColour = new Color32(255, 0, 0, 255);
    public float maxFall = 10f; //custom public float so I can edit maximum fall damage

    Animator anim;
    playerController playerMovement;

    bool damage;

    private void Awake() {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<playerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (Mathf.Abs(collision.relativeVelocity.y) > maxFall) {
            TakeDamage();
        }
    }

    public void TakeDamage() {
        damage = true;
        Death();
    }
    	
	// Update is called once per frame
	private void Update () {
		if(damage) {
            damageImage.color = flashColour;
        }
        else {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damage = false;
	}

    void Death() {
        anim.SetTrigger("Die");
        Debug.Log("lol, he's fucking dead");

        playerMovement.enabled = false;
        GameOver();
    }

    void GameOver() {
        print("Game Over");
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("work", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("work");
    }
}
