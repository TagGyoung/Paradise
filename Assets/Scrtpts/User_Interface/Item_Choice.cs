﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class Item_Choice : MonoBehaviour
{
    [SerializeField] SpriteAtlas Atlas;

    [SerializeField] Image Store_Space_Ship, Store_Space_Ground = null;
    [SerializeField] Button Character_Button = null;

    [SerializeField] Space_Ship[] Shuttle;
    [SerializeField] Space_Ground[] Space;

    public Button [] Purchase;
    public Button Sound_Setting;
    [SerializeField] Text Diamond;
    public bool Shuttle_Locked, Space_Locked;


    private void Start()
    {
        Space_Unlock();
        Shuttle_Unlocked();
        Change_Planet();

        switch (Singleton.instance.Planet_count)
        {
            case 0:
                Singleton.instance.BGM_Sound.clip = Space[0].Sound;
                Singleton.instance.BGM_Sound.Play();
                break;
            case 1:
                if (Space_Locked)
                {
                    Singleton.instance.BGM_Sound.clip = Space[1].Sound;
                    Singleton.instance.BGM_Sound.Play();
                }
                else
                {
                    Singleton.instance.BGM_Sound.clip = Space[0].Sound;
                    Singleton.instance.BGM_Sound.Play();
                }
                break;
            case 2:
                if (Space_Locked)
                {
                    Singleton.instance.BGM_Sound.clip = Space[2].Sound;
                    Singleton.instance.BGM_Sound.Play();
                }
                else
                {
                    Singleton.instance.BGM_Sound.clip = Space[0].Sound;
                    Singleton.instance.BGM_Sound.Play();
                }
                break;
        }


        //Social.ReportProgress(GPGSIds.achievement, 100, null);
    }

    void Shuttle_Unlocked()
    {
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetInt(Shuttle[i].shuttle_name.ToString()) == 1)
            {
                Shuttle_Locked = true;
            }
        }
    }

    void Space_Unlock()
    {
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetInt(Space[i].space_name.ToString()) == 1)
            {
                Space_Locked = true;
            }
        }
    }

    private void Update()
    {
        Change_Shuttle();
        Auto_Calculate(300, 500);

        if (Shuttle_Locked)
        {
            Purchase[0].interactable = false;
            Purchase[0].gameObject.SetActive(false);
        }

        if (Space_Locked)
        {
            Purchase[1].interactable = false;
            Purchase[1].gameObject.SetActive(false);
        }

        Diamond.text = Singleton.instance.Currency.ToString();

        switch (Singleton.instance.Language_Count)
        {
            case 0:
                Localize.SetCurrentLanguage(SystemLanguage.Korean);
                break;
            case 1:
                Localize.SetCurrentLanguage(SystemLanguage.English);
                break;
            case 2:
                Localize.SetCurrentLanguage(SystemLanguage.Japanese);
                break;
            case 3:
                Localize.SetCurrentLanguage(SystemLanguage.Vietnamese);
                break;
        }

        RenderSettings.skybox.SetFloat("_Rotation", 0);

        if (Singleton.instance.Sound_count == 0)
        {
            AudioListener.volume = 1;
            Sound_Setting.GetComponent<Image>().sprite = Atlas.GetSprite("Sound on");
        }
        else
        {
            AudioListener.volume = 0;
            Sound_Setting.GetComponent<Image>().sprite = Atlas.GetSprite("Sound off");
        }
    }

    public void Shuttle_Right_Button()
    {
        if(++Singleton.instance.Count > Shuttle.Length - 1)
            Singleton.instance.Count = 0;

        Change_Shuttle();

        Singleton.instance.SaveData();
    }

    public void Shuttle_Left_Button()
    { 
        if (--Singleton.instance.Count < 0)
            Singleton.instance.Count = Shuttle.Length - 1;

        Change_Shuttle();

        Singleton.instance.SaveData();
    }

    public void Planet_Right_Button()
    {
        if (++Singleton.instance.Planet_count > Space.Length - 1)
            Singleton.instance.Planet_count = 0;

        Change_Planet();

        Singleton.instance.SaveData();
    }

    public void Planet_Left_Button()
    {
        if (--Singleton.instance.Planet_count < 0)
            Singleton.instance.Planet_count = Space.Length - 1;

        Change_Planet();

        Singleton.instance.SaveData();
    }

    public void Sound_Button()
    {     
        if (++Singleton.instance.Sound_count == 1)
        {
           AudioListener.volume = 0;
           Sound_Setting.GetComponent<Image>().sprite = Atlas.GetSprite("Sound off");
        }

        if (Singleton.instance.Sound_count == 2)
        {
            AudioListener.volume = 1;
            Singleton.instance.Sound_count = 0;
            Sound_Setting.GetComponent<Image>().sprite = Atlas.GetSprite("Sound on");
        }

        Singleton.instance.SaveData();
    }

    public void Auto_Calculate(int First_Calculate, int Two_Calculate)
    {
        switch (Singleton.instance.Count)
        {
            case 0:
                Purchase[0].gameObject.SetActive(false);
                break;
            case 1:
                if (Singleton.instance.Currency >= First_Calculate)
                {
                    Purchase[0].GetComponent<Image>().sprite = Atlas.GetSprite("Buy Now 300");

                    Purchase[0].interactable = true;
                    Purchase[0].gameObject.SetActive(true);
                }
                else
                {
                    Purchase[0].GetComponent<Image>().sprite = Atlas.GetSprite("Buy Now 300");

                    Purchase[0].interactable = false;
                    Purchase[0].gameObject.SetActive(true);
                }
                break;
            case 2:
                if (Singleton.instance.Currency >= Two_Calculate)
                {
                    Purchase[0].GetComponent<Image>().sprite = Atlas.GetSprite("Buy Now 500");

                    Purchase[0].interactable = true;
                    Purchase[0].gameObject.SetActive(true);
                }
                else
                {
                    Purchase[0].GetComponent<Image>().sprite = Atlas.GetSprite("Buy Now 500");

                    Purchase[0].interactable = false;
                    Purchase[0].gameObject.SetActive(true);
                }
                break;
        }

        switch (Singleton.instance.Planet_count)
        {
            case 0:
                Purchase[1].gameObject.SetActive(false);
                break;
            case 1:
                if (Singleton.instance.Currency >= First_Calculate)
                {
                    Purchase[1].GetComponent<Image>().sprite = Atlas.GetSprite("Buy Now 300");

                    Purchase[1].interactable = true;
                    Purchase[1].gameObject.SetActive(true);
                }
                else
                {
                    Purchase[1].GetComponent<Image>().sprite = Atlas.GetSprite("Buy Now 300");

                    Purchase[1].interactable = false;
                    Purchase[1].gameObject.SetActive(true);
                }
                break;
            case 2:
                if (Singleton.instance.Currency >= Two_Calculate)
                {
                    Purchase[1].GetComponent<Image>().sprite = Atlas.GetSprite("Buy Now 500");

                    Purchase[1].interactable = true;
                    Purchase[1].gameObject.SetActive(true);
                }
                else
                {
                    Purchase[1].GetComponent<Image>().sprite = Atlas.GetSprite("Buy Now 500");

                    Purchase[1].interactable = false;
                    Purchase[1].gameObject.SetActive(true);
                }
                break;
        }
    }

    public void Space_Purchase()
    {
        switch (Singleton.instance.Planet_count)
        {
            case 0:
                break;
            case 1:
                    Singleton.instance.Currency -= Space[1].Price;
                      
                    Space_Unlock();
                    RenderSettings.skybox = Space[1].Galaxy;

                    Singleton.instance.BGM_Sound.clip = Space[1].Sound;
                    Singleton.instance.BGM_Sound.Play();

                    PlayerPrefs.SetInt(Space_Ground.Space_Ground_Name.Gliese_876.ToString(), 1);                                   
                break;
            case 2:           
                    Singleton.instance.Currency -= Space[2].Price;
                    
                    Space_Unlock();                      
                    RenderSettings.skybox = Space[2].Galaxy;

                    Singleton.instance.BGM_Sound.clip = Space[2].Sound;
                    Singleton.instance.BGM_Sound.Play();
                    PlayerPrefs.SetInt(Space_Ground.Space_Ground_Name.Earth.ToString(), 1);                   
                break;
        }

        Singleton.instance.SaveData();
    }

    public void Shuttle_Purchase()
    {
        switch (Singleton.instance.Count)
        {
            case 0:
                break;
            case 1:
                    Singleton.instance.Currency -= Shuttle[1].Price;
                    
                    Shuttle_Unlocked();
                    PlayerPrefs.SetInt(Space_Ship.Shuttle_Name.Discovery.ToString(), 1);                                   
                break;
            case 2:
                    Singleton.instance.Currency -= Shuttle[2].Price;
                    
                    Shuttle_Unlocked();
                    PlayerPrefs.SetInt(Space_Ship.Shuttle_Name.Endeavour.ToString(), 1);                                  
                break;
        }
    }

    void Change_Planet()
    {
        switch (Singleton.instance.Planet_count)
        {
            case 0:
                if (PlayerPrefs.GetInt(Space[1].space_name.ToString()) == 1 || PlayerPrefs.GetInt(Space[2].space_name.ToString()) == 1)
                {
                    Singleton.instance.BGM_Sound.clip = Space[0].Sound;
                    Singleton.instance.BGM_Sound.Play();
                }

                RenderSettings.skybox = Space[Singleton.instance.Planet_count].Galaxy;
                Store_Space_Ground.sprite = Space[Singleton.instance.Planet_count].Space_sprite;
                break;
            case 1:
                Store_Space_Ground.sprite = Space[Singleton.instance.Planet_count].Space_sprite;

                if (Space_Locked)
                {
                    Singleton.instance.BGM_Sound.clip = Space[1].Sound;
                    Singleton.instance.BGM_Sound.Play();

                    RenderSettings.skybox = RenderSettings.skybox = Space[1].Galaxy;
                    PlayerPrefs.SetString("Space", Space_Ground.Space_Ground_Name.Gliese_876.ToString());
                }

                break;
            case 2:
                Store_Space_Ground.sprite = Space[Singleton.instance.Planet_count].Space_sprite;

                if (Space_Locked)
                {  
                    Singleton.instance.BGM_Sound.clip = Space[2].Sound;
                    Singleton.instance.BGM_Sound.Play();

                    RenderSettings.skybox = RenderSettings.skybox = Space[2].Galaxy;
                    PlayerPrefs.SetString("Space", Space_Ground.Space_Ground_Name.Earth.ToString());
                }
                break;
        }

        Singleton.instance.SaveData();
    }

    public void Change_Shuttle()
    {
        switch (Singleton.instance.Count)
        {
            case 0:
                Singleton.Equip = Shuttle[Singleton.instance.Count].Shuttle_Sprite;
                Store_Space_Ship.sprite = Shuttle[Singleton.instance.Count].Shuttle_Sprite;
                Character_Button.GetComponent<Image>().sprite = Atlas.GetSprite("Atlantis Button");
                break;
            case 1:
                Store_Space_Ship.sprite = Shuttle[Singleton.instance.Count].Shuttle_Sprite;

                if (Shuttle_Locked)
                {
                    Singleton.Equip = Shuttle[1].Shuttle_Sprite;
                    Character_Button.GetComponent<Image>().sprite = Atlas.GetSprite("Discovery Button");
                    PlayerPrefs.SetString("Shuttle", Space_Ship.Shuttle_Name.Discovery.ToString());
                }
                break;
            case 2:
                Store_Space_Ship.sprite = Shuttle[Singleton.instance.Count].Shuttle_Sprite;

                if (Shuttle_Locked)
                {
                    Singleton.Equip = Shuttle[2].Shuttle_Sprite;
                    Character_Button.GetComponent<Image>().sprite = Atlas.GetSprite("Endeavour Button");
                    PlayerPrefs.SetString("Shuttle", Space_Ship.Shuttle_Name.Endeavour.ToString());
                }
                break;
        }
    }
}
