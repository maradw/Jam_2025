
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueControl : MonoBehaviour
{
    [SerializeField] List<string> dialogueLines;
    [SerializeField] Button[] control;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Transform cameraRef;
    [SerializeField] Transform backRef;
    int counter = 0;
  //  [SerializeField] GameManager imageRef;
    [SerializeField] SpriteRenderer imageRef;
    [SerializeField] GameObject nextScene;
    bool isFirstScale = false;
    void Start()
    {cameraRef.DOMoveY(cameraRef.transform.position.y + 5f, 1f);
        dialogueText.text = dialogueLines[0];
        nextScene.SetActive(false);
        ScaleBack();
    }
    void Update()
    {
        
    }
    void FlipImage()
    {
        //imageRef.DOFlip();
       imageRef.flipX = !imageRef.flipX;
        Debug.Log("Flip");
    }
    public void ScaleBack()
    {
        backRef.DOScale(backRef.localScale * 1.5f, 3f);
    }
    public void NextDialogue()
    {
        if (counter < dialogueLines.Count-1)
        {
            counter++;
            FlipImage();
            dialogueText.text = dialogueLines[counter];
            control[1].interactable = true;

           
        }
        if(counter == dialogueLines.Count-1)
        {
            control[0].interactable = false;
            nextScene.SetActive(true);
        }
        if (counter == 1) 
        {
           // ScaleBack();
        }
        Debug.Log(counter);
    }
    public void PreviousDialogue()
    {
        if (counter >0)
        {
            counter--;
            FlipImage();
            dialogueText.text = dialogueLines[counter];
            control[0].interactable = true;
            nextScene.SetActive(false);

        }
        if (counter == 0)
        {
            control[1].interactable = false; nextScene.SetActive(false);
        }
        Debug.Log(counter);
    }
    public void SceneChanger()
    {
        SceneManager.LoadScene("fe");
    }
    /*if (dialogueLines.Count > 0)
        {
            string currentLine = dialogueLines[0];
            dialogueLines.RemoveAt(0);
            Debug.Log(currentLine); // Replace with actual dialogue display logic
        }
        else
        {
            Debug.Log("No more dialogue lines.");
        }*/
}
