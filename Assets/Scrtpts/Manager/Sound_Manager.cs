﻿using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public AudioSource Belch_Auido;

    public AudioClip[] Sound_Effect;

    public static Sound_Manager instance; 

    private void Awake() 
    {
        if (instance == null) // Sound_Manager 값이 없다면
            instance = this; // Sound_Manager.instance에 자기 자신을 넣습니다.
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(Belch_Auido);
    }

    void Start()
    {
        Belch_Auido = GetComponent<AudioSource>();
    }

    public void Belch_Sound()
    {
        Belch_Auido.volume = 0.5f;
        Belch_Auido.PlayOneShot(Sound_Effect[0]); // Belch_Auido의 PlayOneshot는 안에 있는 사운드를 재생시키는 함수입니다.
    }

    public void Button_Sound()
    {
        Belch_Auido.PlayOneShot(Sound_Effect[1]);
    }

    public void Count_Sound()
    {
        Belch_Auido.PlayOneShot(Sound_Effect[2]);
    }

    public void Cancle_Sound()
    {
        Belch_Auido.PlayOneShot(Sound_Effect[3]);
    }

    public void Start_Sound()
    {
        Belch_Auido.PlayOneShot(Sound_Effect[4]);
    }

    public void Setting_Sound()
    {
        Belch_Auido.PlayOneShot(Sound_Effect[5]);
    }

    public void Support_Sound()
    {
        Belch_Auido.volume = 0.5f;
        Belch_Auido.PlayOneShot(Sound_Effect[6]);
    }

    public void Store_Select_Sound()
    {
        Belch_Auido.PlayOneShot(Sound_Effect[7]);
    }

    
}