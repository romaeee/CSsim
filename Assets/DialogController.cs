using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueObject;
    private List<string> currentDialogue = new List<string>();
    [SerializeField] private List<string> dialog0 = new List<string>(); // Hello
    [SerializeField] private List<string> dialog1 = new List<string>(); // Checking finish
    [SerializeField] private List<string> dialog2 = new List<string>(); // Good!
    [SerializeField] private List<string> dialog3 = new List<string>();
    [SerializeField] private List<string> dialog4 = new List<string>();
    [SerializeField] private List<string> dialog5 = new List<string>();
    [SerializeField] private float textSpeed = 0.125f;
    [SerializeField] private float t = 0.05f;
    [SerializeField] private Text phrase;
    [SerializeField] private bool isLoop;
    [SerializeField] private bool isNPC;
    public GameObject btn;

    [SerializeField] private float waitStart;

    private int numOfOhrase = 0;
    private bool isSpeak;
    private bool isReady;
    private bool isFirst=true;

    private void Start()
    {
        if (isFirst)
        {
            isFirst = false;
            currentDialogue = dialog0;
        }
        
        if(gameObject.tag == "Player")
            StartCoroutine(StartDialogue(waitStart));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CheckFinish" && !PlayerMovement.isTalking)
        {
            StartCoroutine(StartDialogue(waitStart));
            currentDialogue = dialog1;
        }
        else if (collision.tag == "Player" && gameObject.tag != "NPCCust1" && gameObject.tag != "NPCCust2" && !PlayerMovement.isTalking)
        {
            StartCoroutine(StartDialogue(waitStart));
            currentDialogue = dialog0;

        }
        else if (collision.tag == "Player" && gameObject.tag == "NPCCust1" && !dialogueObject.activeSelf && !PlayerMovement.isTalking)
        {
            StartCoroutine(StartDialogue(waitStart));
        }
        else if (collision.tag == "Player" && gameObject.tag == "NPCCust2" && !dialogueObject.activeSelf && !PlayerMovement.isTalking)
        {
            PlayerMovement.isQuest = true;
            StartCoroutine(StartDialogue(waitStart));
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogueObject.activeSelf)
        {
            NextPhrase();
        }

        if (isReady)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentDialogue = dialog0;
                StartCoroutine(StartDialogue(waitStart));
            }
                
        }
    }

    public void NextPhrase()
    {
        if (!isSpeak)
        {
            numOfOhrase++;
            if (numOfOhrase == currentDialogue.Count)
            {
                StartCoroutine(StopDialogue());

            }
            else
            {
                if (gameObject.tag == "NPCCust1")
                    isReady = true;
                phrase.text = "";
                StartCoroutine(PlayText());
            }

        }
    }

    IEnumerator PlayText()
    {
        btn.SetActive(false);
        isSpeak = true;
        foreach (char c in currentDialogue[numOfOhrase])
        {
            phrase.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        btn.SetActive(true);
        isSpeak = false;
    }

    IEnumerator StartDialogue(float w)
    {
        numOfOhrase = 0;
        //yield return new WaitForSeconds(w);
        yield return new WaitForSeconds(.25f);
        //yield return new WaitForSeconds(.2f);
        PlayerMovement.isTalking = true;
        dialogueObject.SetActive(true);
        phrase.text = "";
        StartCoroutine(PlayText());
    }

    IEnumerator StopDialogue()
    {
        yield return new WaitForSeconds(.1f);
        dialogueObject.SetActive(false);
        PlayerMovement.isTalking = false;
    }
}
