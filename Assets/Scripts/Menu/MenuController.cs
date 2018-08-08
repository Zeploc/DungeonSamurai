using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour {

    [SerializeField] GameObject BackgroundImage;
    [SerializeField] GameObject PlayButton;

    public float StartX = -10;
    public float EndX = -10;
    public float PanningSpeed = 2.0f;
    public bool DirectionLeft = true;

    // Use this for initialization
    void Start () {
        PlayButton.GetComponent<Button>().Select();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 NewPosition = BackgroundImage.transform.position;
        if (NewPosition.x <= StartX)
            DirectionLeft = false;
        else if (NewPosition.x >= EndX)
            DirectionLeft = true;

        NewPosition.x = StartX;
        if (!DirectionLeft)
            NewPosition.x = EndX;



        BackgroundImage.transform.position = Vector3.MoveTowards(BackgroundImage.transform.position, NewPosition, PanningSpeed * Time.deltaTime);

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(PlayButton);
        }
        else
        {
            PlayButton = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void StartGame()
    {
		Debug.Log ("Start Game");
		FindObjectOfType<AudioManager> ().StopSound ("bgMusic");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
