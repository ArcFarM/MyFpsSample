using MyFps;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EScene2Start : MonoBehaviour
{
    #region Variables
    public GameObject player;
    public SceneFader sceneFader;

    [SerializeField] string sequenceText = "";

    #endregion

    #region Properties
    #endregion

    #region Unity Event Method
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked; // 커서 잠금
        Cursor.visible = false; // 커서 숨김
        StartCoroutine(SequencePlay()); // 시퀀스 시작

        PlayerManager.CurrentWeapon = WeaponType.Pistol;
    }
    #endregion

    #region Custom Method
    IEnumerator SequencePlay() {
        sceneFader.FadeStart(); // 페이드 시작
        //1. 캐릭터 비활성화
        player.SetActive(false);
        player.GetComponent<PlayerInput>().enabled = false;
        yield return new WaitForSeconds(1f);
        //2.  대기 후 시나리오 텍스트 출력 및 배경음 재생
        sequenceText = "";

        //e. 캐릭터 재활성화
        player.SetActive(true);
        player.GetComponent<PlayerInput>().enabled = true;

    }
    #endregion
}
