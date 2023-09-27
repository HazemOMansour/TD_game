using System.Collections;
using TMPro;
using UnityEngine;

public class FinalWave : MonoBehaviour
{
    public string[] sentences;
    public float textSpeed;
    public TextMeshProUGUI dialogueText;

    private int index;
    public GameObject boss;
    public Transform spawnPoint;
    Animator animator;
    public Camera cameraMain;
    Vector3 firstPos;
    Vector3 offset = new Vector3(-3.8f, 12.6f,1.18f);
    bool AnimationPlayed;
    public GameMaster gameMaster;

    private void Start()
    {
        AnimationPlayed = false;
        firstPos = cameraMain.transform.position;
        dialogueText.text = string.Empty;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Enemy.bossIsDead && AnimationPlayed == false)
        {
            animator.Play("WaveEnd");
            cameraMain.transform.position =  Enemy.bossPos + offset;
            AnimationPlayed = true;
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeCharacters());
    }

    IEnumerator TypeCharacters()
    {
        foreach (char c in sentences[index].ToCharArray())
        {
            dialogueText.text += c;
            if (c == '.' || c =='!' || c == '-')
                yield return new WaitForSeconds(0.1f);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void ClearText()
    {
        dialogueText.text = string.Empty;
    }

    public void NextLine()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(TypeCharacters());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SpawnBoss()
    {
        Instantiate(boss, spawnPoint.position, Quaternion.identity);
    }

    public void ResetCameraPos()
    {
        cameraMain.transform.position = firstPos;
    }

    public void WinLevel()
    {
        gameMaster.WinLevel();
    }
}
