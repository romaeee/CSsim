using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private List<string> dialog = new List<string>();
    [SerializeField] private float textSpeed = 0.125f;
    [SerializeField] private float t = 0.05f;
    [SerializeField] private Text phrase;
    public GameObject btn;

    [SerializeField] private float waitStart;

    private int numOfOhrase = 0;
    private bool isSpeak;

    private void Start()
    {
        StartCoroutine(StartDialogue(waitStart));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextPhrase();
        }
    }

    public void NextPhrase()
    {
        if (!isSpeak)
        {
            numOfOhrase++;
            if (numOfOhrase == dialog.Count)
            {
                StartCoroutine(StopDialogue());
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
        foreach (char c in dialog[numOfOhrase])
        {
            phrase.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        btn.SetActive(true);
        isSpeak = false;
    }

    IEnumerator StartDialogue(float w)
    {
        yield return new WaitForSeconds(w);
        yield return new WaitForSeconds(.25f);
        yield return new WaitForSeconds(.2f);
        phrase.text = "";
        StartCoroutine(PlayText());
    }

    IEnumerator StopDialogue()
    {

        yield return new WaitForSeconds(.1f);
        //CloseBox();
    }
}
