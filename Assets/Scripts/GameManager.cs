using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //메인 카메라
    public Camera mainCamera;
    public PlayerController playerController;
    public ComboManager comboManager;
    public SoundManager soundManager;
    public UIController uIController;
    private GameObject backgroundPrefab;
    public int enemiesSlain = 0;
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        comboManager = GameObject.FindObjectOfType<ComboManager>();
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        uIController = GameObject.FindObjectOfType<UIController>();


        backgroundPrefab = Resources.Load("Prefabs/Background") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //TryAddAndDeleteBackground();
    }

    private void TryAddAndDeleteBackground()
    {
       var camTopLeft = mainCamera.ViewportToWorldPoint(new Vector3(0,0,0));

       //Physics2D.OverlapBox(camTopLeft ,new Vector2(0,0.1f,0), )
       //if(camTopLeft)
    }

    public void GameOver()
    {
        uIController.gameOverPanel.SetActive(true);
        uIController.uiButtonPanel.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }
    public void ResetScene() {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("DestructableObjects"), false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
