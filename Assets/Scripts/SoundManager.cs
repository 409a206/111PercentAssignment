using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound{
    public string name; //사운드의 이름
    public AudioClip clip; //사운드
}
public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;
    public AudioSource[] audioSourceEffects;
    public AudioSource[] audioSourceBGM;

    public string[] playSoundName;

    [SerializeField]
    public Sound[] effectSounds;
    [SerializeField]
    public Sound[] BGMSounds;

    #region singleton
    
    private void Awake() { 
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
           
            Destroy(this.gameObject);  
    }
    #endregion
    void Start()
    {
        playSoundName = new string[audioSourceEffects.Length + audioSourceBGM.Length];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySE(string _name) {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if(_name == effectSounds[i].name) {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if(!audioSourceEffects[j].isPlaying) {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        return; 
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }
    public void StopAllSE() {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }
    public void StopSE(string _name) {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if(playSoundName[i] == _name) {
                audioSourceEffects[i].Stop();
                break;
            }  
        }
        Debug.Log("재생중인 " + _name + " 사운드가 없습니다.");
    }
    public void PlayBGM(string _name) {
        for (int i = 0; i < BGMSounds.Length; i++)
        {
            if(_name == BGMSounds[i].name) {
                for (int j = 0; j < audioSourceBGM.Length; j++)
                {
                    if(!audioSourceBGM[j].isPlaying) {
                        playSoundName[j] = BGMSounds[i].name;
                        audioSourceBGM[j].clip = BGMSounds[i].clip;
                        audioSourceBGM[j].Play();
                        return; 
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }
    public void StopAllBGM() {
        for (int i = 0; i < audioSourceBGM.Length; i++)
        {
            audioSourceBGM[i].Stop();
        }
    }

    public void StopBGM(string _name) {
        for (int i = 0; i < audioSourceBGM.Length; i++)
        {
            if(playSoundName[i] == _name) {
                audioSourceBGM[i].Stop();
                break;
            }  
        }
        Debug.Log("재생중인 " + _name + " 사운드가 없습니다.");
    }
}
