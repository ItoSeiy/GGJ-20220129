using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaswardPanel : MonoBehaviour
{
    [SerializeField, Tooltip("開けるドア")] DoorBase openDoor;
    [SerializeField, Tooltip("パスワードを入力するパネル")] GameObject paswardPanel;
    [SerializeField, Tooltip("panelの数字を出す部分")] Text display;
    [SerializeField, Tooltip("霊体でしか見えない数字")] GameObject astralNumber;
    [SerializeField, Tooltip("Playerのタグ")] string playerTag = "Player";
    [SerializeField, Tooltip("答え")] int[] answer = default;
    [Header("Test")]
    [SerializeField] bool inAria = false;

    string currntPanel = "";
    int count = 0;
    void Start()
    {
        paswardPanel?.SetActive(false);
        display.text = currntPanel;
    }

    private void Update()
    {
        if (Input.GetKeyDown("f") && inAria)
        {
            paswardPanel?.SetActive(true);
        }

        if(PlayerController.Instance.IsBodyOrAstral)
        {
            astralNumber.SetActive(true);
        }
        else
        {
            astralNumber.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(playerTag))
        {
            inAria = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            inAria = false;
        }
    }

    public void input(int number)
    {
        SoundManager.Instance.UseSound(SoundType.Button);

        if (answer[count] != number)
        {
            count = 0;
            currntPanel = "";
            display.text = currntPanel;
            paswardPanel?.SetActive(false);
        }
        else
        {
            currntPanel += number.ToString();
            display.text = currntPanel;
            Debug.Log(number);
            count++;
            if (count == 6)
            {
                Debug.Log("成功");
                openDoor?.Push();
                paswardPanel?.SetActive(false);
                this.gameObject.GetComponent<PaswardPanel>().enabled = false;
            }
        }
    }
}
