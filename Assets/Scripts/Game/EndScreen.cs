using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    [SerializeField] GameObject MessageTextObject;

	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShowEndScren(string Message)
    {
        MessageTextObject.GetComponent<Text>().text = Message;
        gameObject.SetActive(true);
    }
}
