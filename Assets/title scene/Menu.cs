using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Menu upperMenu;
    public List<TextMesh> textMeshs = new List<TextMesh>();

    public Menu[] subMenu;
    private Vector3 instPos;
    public int currOption;

    private void Start()
    {
        instPos = this.transform.localPosition;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        currOption = 0;
        Reflash();
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Up();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Down();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Back();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Return))
        {
            Select();
        }


        if (gameObject.name == "title" && Input.anyKeyDown)
        {
            Select();
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, instPos + Vector3.up * 1.5f * currOption, Time.unscaledDeltaTime * MenuManager.inst.scrollSpeed);

    }




    public void Up()
    {
        if (currOption <= 0)
        { return; }
        currOption--;
        Reflash();
    }

    public void Down()
    {
        if (currOption >= textMeshs.Count - 1)
        { return; }
        currOption++;
        Reflash();
    }

    public void Select()
    {
        Time.timeScale = 1;
        if (subMenu[currOption] != null)
        {
            subMenu[currOption].Show();
            this.gameObject.SetActive(false);
        }
        else if (textMeshs[currOption].name == "Exit")
        {
            Application.Quit();
        }
        else if (textMeshs[currOption].name == "Resume")
        {
            MenuManager.inst.gameObject.SetActive(false);
        }
        else if (textMeshs[currOption].name == "Back to menu")
        {
            SceneManager.LoadScene("title");
        }
        else if (textMeshs[currOption].name == "Montage Mode")
        {
            SceneManager.LoadScene("MainScene");
            PlayerPrefs.DeleteKey("ArcadeMode");
        }
        else if (textMeshs[currOption].name == "Arcade Mode")
        {
            SceneManager.LoadScene("MainScene");
            PlayerPrefs.SetInt("ArcadeMode", 1);
        }
        else if (textMeshs[currOption].name == "Soccer Mode")
        {
            SceneManager.LoadScene("soccerGame");
        }

    }

    public void Back()
    {
        if (upperMenu)
        {
            upperMenu.Show();
            this.gameObject.SetActive(false);
        }
    }

    private void Reflash()
    {
        foreach (TextMesh t in textMeshs)
        {
            //t.font = Title.inst.thinF;
            t.fontStyle = FontStyle.Normal;
            t.color = new Color(0,0,0, 0);
        }
        //textMeshs[Title.inst.currOption].font = Title.inst.thickF;
        textMeshs[currOption].fontStyle = FontStyle.Bold;
        SetColor(-2, 0.3f);
        SetColor(-1, 0.7f);
        SetColor(0, 1f);
        SetColor(1, 0.7f);
        SetColor(2, 0.3f);
        
        MenuManager.inst.arrowBack.SetActive(upperMenu != null);
        MenuManager.inst.arrowUp.SetActive(currOption > 0);
        MenuManager.inst.arrowDown.SetActive(currOption < textMeshs.Count - 1);
    }

    private void SetColor(int id, float alpha)
    {
        if (currOption + id >=0 && currOption + id < textMeshs.Count)
        {
            textMeshs[currOption + id].color = new Color(0, 0, 0, alpha);
        }
    }

}
