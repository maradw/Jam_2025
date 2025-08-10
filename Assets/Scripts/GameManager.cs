
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    [SerializeField] private bool isGameRunning = false;
    private bool isPaused = false;
    // [SerializeField] private NotificationManager notificationManager;
    public static event Action OnGameStart;
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;
    [SerializeField] GameObject coins;

    // public static event Action OnGameOver;
   [SerializeField] Transform objectToMove;
    [SerializeField] List<Transform> sequence;

    [SerializeField] Transform sidePlatform;

    string sceneName ;  
    public static event Action OnWin;
    private void OnEnable()
    {
        StartGame();
        PlayerControl.OnCollisionActivateSide += MoveSide;
        PlayerControl.OnCollisionActivateFall += FallPlatform;
        PlayerControl.OnCollisionActiveteSequence += StartSequence;
        PlayerControl.OnLose += EndGame;
        PlayerControl.OnWin += EndGame;
    }
    private void OnDisable()
    {
        PlayerControl.OnCollisionActivateSide -= MoveSide;
        PlayerControl.OnCollisionActivateFall -= FallPlatform;
        PlayerControl.OnCollisionActiveteSequence -= StartSequence;
        PlayerControl.OnLose -= EndGame;
        PlayerControl.OnWin -= EndGame;
    }
   void MoveSide()
    {
        sidePlatform.DOMoveX(sidePlatform.position.x + 20f, 1f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => Debug.Log("Platform moved successfully!"));
    }

    void FallPlatform()
    {
        objectToMove.DOMoveY(objectToMove.position.y - 20f, 1f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => Debug.Log("Platform fell successfully!"));
    }
    public void StartGame()
    {
        //AudioManager.Instance.StopSound();
        //AudioManager.Instance.PlayMusicIndex(1);  
        isGameRunning = true;
        isPaused = false;
        Time.timeScale = 1f; 
        Debug.Log("Game Started");
    }


    public void EndGame()
    {
        isGameRunning = false;
        Time.timeScale = 0f;
       
       
        Debug.Log("Game Over");
     

    }
    public void GoScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }
    public void PauseGame()
    {
        //if (!isGameRunning || isPaused) return;

        isPaused = true;
        Time.timeScale = 0f;
        OnGamePaused?.Invoke();
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
      //  if (!isGameRunning || !isPaused) return;

        isPaused = false;
        Time.timeScale = 1f;
        OnGameResumed?.Invoke();
        Debug.Log("Game Resumed");
    }
    public void WinGame()
    {
        Time.timeScale = 0f;
        isGameRunning = false;
        
        OnWin?.Invoke();
      

    }
    public void InitializeGameSession()
    {
        OnGameStart?.Invoke();
    }
    IEnumerator SequenceFall()
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            Transform currentObject = sequence[i];
            currentObject.DOMoveY(currentObject.position.y - 20f, 1f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => Debug.Log("Platform " + i + " fell successfully!"));
            yield return new WaitForSeconds(0.6f);
        }
        //yield return new WaitForSeconds (2f);
    }
    void StartSequence()
    {
        StartCoroutine(SequenceFall());
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag== "win")
        {
            OnWin?.Invoke();
        }
    }
    void SendFinalNotification()
    {
        int puntajeFinal = scoreData.puntuacion;
        int puntajeGuardado = gameData.ObtenerPuntaje();

        gameData.GuardarPuntaje(puntajeFinal);

       // notificationManager.EnviarNotificacionRondaTerminada(puntajeFinal);

        if (puntajeFinal > puntajeGuardado)
        {
            //notificationManager.EnviarNotificacionNuevoRecord(puntajeFinal);
        }
    }
    */
    //public bool IsPaused() => isPaused;



}
