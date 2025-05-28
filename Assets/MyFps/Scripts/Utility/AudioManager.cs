using UnityEngine;

namespace MyFps {
    public class AudioManager : Singleton<AudioManager> {
        #region Variables
        public Sound[] sounds;
        public string mainmusic = "MenuMusic"; // 기본 음악 이름
        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        protected override void Awake() {
            base.Awake();

            //사운드 데이터 세팅
            foreach (Sound sound in sounds) {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;

                sound.source.playOnAwake = false; // 자동 재생 방지
            }
        }
        #endregion

        #region Custom Method
        //음성 재생
        public void Play(string name) {
            Sound sound = null;
            foreach (var s in sounds) {
                if (s.name == name) {
                    sound = s;
                    break;
                }
            }
            //사운드가 존재하면 재생
            if (sound != null) {
                sound.source.Play();
            }
            else {
                Debug.LogWarning("Sound: " + name + " not found!");
            }
        }

        public void Stop(string name) {
            Sound sound = null;
            foreach (var s in sounds) {
                if (s.name == name) {
                    sound = s;
                    break;
                }
            }
            //사운드가 존재하면 중지
            if (sound != null) {
                sound.source.Stop();
            }
            else {
                Debug.LogWarning("Sound: " + name + " not found!");
            }
        }

        public void PlayBgm(string name) {
            if (name == mainmusic) return;
            //기존 BGM 중지
            Stop(mainmusic);
            //새로운 BGM 재생
            Play(name);
        }
        #endregion
    }

}
