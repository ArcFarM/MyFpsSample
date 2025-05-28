using UnityEngine;

namespace MyFps {
    [System.Serializable]
    public class Sound {
        //재생할 AudioClip
        public AudioClip clip;
        //취급할 이름
        public string name;
        //음량 크기 (기본값 50%)
        [Range(0f, 1f)] public float volume = 0.5f;
        //재생 속도 (기본값 1.0배속)
        [Range(0.1f, 3f)] public float pitch = 1f;
        //반복 여부 (기본값 아님)
        public bool loop = false;
        //재생할 AudioSource
        public AudioSource source;

    }

}
