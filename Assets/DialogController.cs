using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    [SerializeField] private string characterName;
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
    private bool isFinish;

    private void Start()
    {
        if (gameObject.tag == "Player")
        {
            currentDialogue = dialog0;
            StartCoroutine(StartDialogue(waitStart));
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CheckFinish" && !PlayerMovement.isTalking && GameController.currentMoney == 1000)
        {
            CheckPhrase();
            StartCoroutine(StartDialogue(waitStart));
            currentDialogue = dialog1;
        }

        else if (collision.tag == "CheckFinish" && GameController.currentMoney < 1000)
        {
            GameController.priceData.Clear();
            SceneManager.LoadScene("EndScreen");
        }

        else if (collision.tag == "Player" && !PlayerMovement.isTalking)
        {
            CheckPhrase();
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

    public void CheckPhrase()
    {
        if (characterName == "Player" || characterName == "Cust_1" || characterName == "Staff_2" || characterName == "NPC_1" || characterName == "NPC_2")
            currentDialogue = dialog0;
        if (characterName == "Staff_1" && CartScript.cartId.Count == 0)
            currentDialogue = dialog0;
        else if (characterName == "Staff_1" && CartScript.cartId.Count > 0 && GameController.currentMoney-CartScript.totalPrice < 0)
        {
            currentDialogue = dialog1;
            currentDialogue[0] = "It's $" + CartScript.totalPrice.ToString();
        }
            
        else if (characterName == "Staff_1" && CartScript.cartId.Count > 0 && GameController.currentMoney - CartScript.totalPrice > 0)
        {
            currentDialogue = dialog2;
            currentDialogue[0] = "It's $" + CartScript.totalPrice.ToString();
            GameController.currentMoney -= CartScript.totalPrice;
            CartScript.cartId.Clear();
            CartScript.totalPrice = 0;
            
        }
        if (characterName == "Cust_2" && !PlayerMovement.isQuest && !PlayerMovement.isComplited)
        {
            currentDialogue = dialog0;
            PlayerMovement.isQuest = true;
        }
        else if (characterName == "Cust_2" && PlayerMovement.isQuest && !CartScript.cartId.Contains(8) && !PlayerMovement.isComplited)
        {
            currentDialogue = dialog1;
        }
        else if (characterName == "Cust_2" && PlayerMovement.isQuest && CartScript.cartId.Contains(8) && !PlayerMovement.isComplited)
        {
            currentDialogue = dialog2;
            CartScript.cartId.RemoveAt(CartScript.cartId.IndexOf(8));
            PlayerMovement.isComplited = true;
        }
        else if (characterName == "Cust_2" && PlayerMovement.isQuest && PlayerMovement.isComplited)
        {
            currentDialogue = dialog3;

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
