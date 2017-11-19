using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularMenu : MonoBehaviour
{
    public List<MenuButton> buttons = new List<MenuButton>();
    private Vector2 MousePosition;
    private Vector2 fromVector2M = new Vector2(0.5f, 1.0f);
    private Vector2 centercircle = new Vector2(0.5f, 0.5f);
    private Vector2 toVector2M;

    public int menuItems;
    public int CurMenuItem;
    private int OldMenuItem;

    public GameObject player;
    public GameObject healthBar;
    public GameObject heartBar;

    private AudioSource source;
    public AudioClip swapBarre;
    public AudioClip swapHeart;

    public CircularMenuManager manager;

    // Use this for initialization
    void Awake()
    {
        menuItems = buttons.Count;
        foreach(MenuButton button in buttons)
        {
            button.sceneimage.color = button.NormalColor;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        /*healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        heartBar = GameObject.FindGameObjectWithTag("HeartBar");*/
        CurMenuItem = 0;
        OldMenuItem = 0;
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        healthBar.SetActive(true);
        heartBar.SetActive(false);
        player.GetComponent<Player>().SwitchState(Player.PlayerState.HealthBar);
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentMenuItem();
        if (Input.GetMouseButtonDown(0))
        {
            ButtonAction();
        }
    }

    public void GetCurrentMenuItem()
    {
        MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        toVector2M = new Vector2(MousePosition.x / Screen.width, MousePosition.y / Screen.height);

        float angle = (Mathf.Atan2(fromVector2M.y - centercircle.y, fromVector2M.x - centercircle.x) - Mathf.Atan2(toVector2M.y - centercircle.y, toVector2M.x - centercircle.x)) * Mathf.Rad2Deg;

        angle += 90;

        if (angle > 360)
        {
            angle -= 360;
        }

        CurMenuItem = (int)(angle / (360 / menuItems));
        if (CurMenuItem != OldMenuItem)
        {
            buttons[OldMenuItem].sceneimage.color = buttons[OldMenuItem].NormalColor;
            OldMenuItem = CurMenuItem;
            buttons[CurMenuItem].sceneimage.color = buttons[CurMenuItem].HightLightedColor;

        }

    }

    public void ButtonAction()
    {
        buttons[CurMenuItem].sceneimage.color = buttons[CurMenuItem].PressedColor;
        if (CurMenuItem == 0)
        {
            player.GetComponent<Player>().SwitchState(Player.PlayerState.HealthBar);
            healthBar.SetActive(true);
            heartBar.SetActive(false);
            if (swapBarre != null)
                source.PlayOneShot(swapBarre, 1F);
        }
        if (CurMenuItem == 1)
        {
            player.GetComponent<Player>().SwitchState(Player.PlayerState.HeartBar);
            healthBar.SetActive(false);
            heartBar.SetActive(true);
            if (swapHeart != null)
                source.PlayOneShot(swapHeart, 1F);
        }
        manager.HideMenu();
    }
}

[System.Serializable]
public class MenuButton
{
    public string name;
    public Image sceneimage;
    public Color NormalColor = Color.white;
    public Color HightLightedColor = Color.grey;
    public Color PressedColor = Color.gray;
}