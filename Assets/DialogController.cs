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
    [SerializeField] private List<string> dialog2 = new List<string>(); // Staff help
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

    private void Start()
    {
        currentDialogue = dialog0;
        if(gameObject.tag == "Player")
            StartCoroutine(StartDialogue(waitStart));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CheckFinish")
        {
            StartCoroutine(StartDialogue(waitStart));
            currentDialogue = dialog1;
        }
        else if (collision.tag == "Player")
        {
            StartCoroutine(StartDialogue(waitStart));
            currentDialogue = dialog0;
            //collision.GetComponent<DialogController>().enabled = true;
            //collision.GetComponent<DialogController>().StartCoroutine(StartDialogue(waitStart));
            //currentDialogue = dialog1;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogueObject.activeSelf)
        {
            NextPhrase();
        }
    }

    public void NextPhrase()
    {
        if (!isSpeak)
        {
            numOfOhrase++;
            if (numOfOhrase == currentDialogue.Count)
            {
                if (!isLoop)
                    StartCoroutine(StopDialogue());
                //else
                    //numOfOhrase = 0; StartCoroutine(StartDialogue(waitStart));
            }
            else
            {
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
