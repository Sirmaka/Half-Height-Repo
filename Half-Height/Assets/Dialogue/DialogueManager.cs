using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Image dialogueImage;
    public Animator panelAnimator;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        player = GameObject.FindObjectOfType<PlayerController>();

    }

    public void StartDialogue (Dialogue dialogue)
    {
        panelAnimator.SetBool("InDialogue", true);
        player.enterDialogue();     // tell the player that we're in a dialogue
        nameText.text = dialogue.name;
        dialogueImage.sprite = dialogue.image;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForFixedUpdate();
        }
    }
    void EndDialogue()
    {
        panelAnimator.SetBool("InDialogue", false);
        player.exitDialogue();
    }

}
