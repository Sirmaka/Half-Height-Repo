using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    //Based on tutorial by Brackeys, found here: https://www.youtube.com/watch?v=_nRzoTzeyxU
    //Heavily modified though.
    private PlayerController controller;
    public DialogueManager dialogueManager;
    public LayerMask canTalkTo;
    private bool readyToTalk;
    private Dialogue dialogue;
    private bool playerInRange;
    private float dialogueCooldownDuration = 0.5f;
    private float dialogueCooldown;
    public Canvas dialoguePromptPrefab;
    private Canvas dialoguePrompt;
    // Start is called before the first frame update
    void Start()
    {
        dialogueCooldown = dialogueCooldownDuration;
        controller = this.GetComponent<PlayerController>();
        dialoguePrompt = Instantiate(dialoguePromptPrefab);
        dialoguePrompt.transform.SetParent(transform);
        dialoguePrompt.transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
        dialoguePrompt.enabled = false; // to be activated when the player walks into range
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.getInDialogue())
        {
            if(Input.GetButtonDown("Interact"))
            {
                dialogueManager.DisplayNextSentence();
            }
        }

        if(readyToTalk && Input.GetButtonDown("Interact"))
        {
            dialogueManager.StartDialogue(dialogue);
        }
        
        //if you can't enter dialogue, count down the cooldown
        if(!controller.getCanEnterDialogue())
        {
            dialogueCooldown -= Time.deltaTime;
        }
        if(dialogueCooldown <= 0)
        {
            controller.setCanEnterDialogue(true);
            dialogueCooldown = dialogueCooldownDuration;
        }

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(canTalkTo.Contains(collider.gameObject) && !controller.getInDialogue())
        {
            showdialoguePrompt(true);
            //get the dialogue from the friend, ready to execute above code.
            dialogue = collider.gameObject.GetComponent<DialogueTrigger>().getDialogue();
        }
        else
        {
            showdialoguePrompt(false);
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(canTalkTo.Contains(collider.gameObject))
        {
            showdialoguePrompt(false);
        }
    }

    private void showdialoguePrompt(bool set)
    {
        dialoguePrompt.enabled = set;
        readyToTalk = set;
    }
}
