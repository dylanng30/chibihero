using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UnlockSkillNPC : MonoBehaviour
{
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private GameObject SkillPanel;
    [SerializeField] TMPro.TextMeshProUGUI textDialogue;
    [SerializeField] private string[] dialogueTexts;
    private float timeChangeText = 5f;

    private bool isDialogueActive = false;
    private Coroutine dialogueCoroutine;

    private void Start()
    {
        DialoguePanel.SetActive(false);
        SkillPanel.SetActive(false);
    }
    private void Flip(Transform player)
    {
        Vector2 direction = player.position - transform.position;
        this.transform.localScale = direction.x < 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }

    private void StartDialogue()
    {
        if (dialogueCoroutine == null)
        {
            isDialogueActive = true;
            dialogueCoroutine = StartCoroutine(ShowDialogue());
        }
    }
    private void HideDialogue()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
        }

        DialoguePanel.SetActive(false);
        isDialogueActive = false;
    }

    public void OpenSkillPanel()
    {
        SkillPanel.SetActive(true);
    }
    public void CloseSkillPanel()
    {
        SkillPanel.SetActive(false);
    }


    private IEnumerator ShowDialogue()
    {
        DialoguePanel.SetActive(true);
        for (int i = 0; i < dialogueTexts.Length; i++)
        {
            textDialogue.text = dialogueTexts[i];
            yield return new WaitForSeconds(timeChangeText);
            Debug.Log("Dialogue: " + dialogueTexts[i]);
        }
        DialoguePanel.SetActive(false);
        yield return new WaitForSeconds(1f); // Thời gian chờ trước khi kết thúc
        OpenSkillPanel();
        PlayerController.Instance.AbilitySkill.UnlockSkill(); // Mở khóa kỹ năng cho người chơi
        yield return new WaitForSeconds(2f); // Thời gian chờ trước khi kết thúc
        CloseSkillPanel();
        yield return new WaitForSeconds(1f); // Thời gian chờ trước khi kết thúc
        GameManager.Instance.CompleteMap(true);
        isDialogueActive = false;
        dialogueCoroutine = null; // Reset coroutine khi kết thúc
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Flip(collision.transform);
            if (!isDialogueActive)
                StartDialogue();
        }        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            HideDialogue();
    }
}
