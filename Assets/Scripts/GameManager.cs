
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{

    [SerializeField] private bool isGameRunning = false;
    private bool isPaused = false;
    // [SerializeField] private NotificationManager notificationManager;
    public static event Action OnGameStart;
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;

   // public static event Action OnGameOver;


    public static event Action OnWin;
    private void OnEnable()
    {
        StartGame();
    }
    private void OnDisable()
    {
        

        

    }
    
    public void StartGame()
    {
        AudioManager.Instance.StopSound();
        AudioManager.Instance.PlayMusicIndex(1);  
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
