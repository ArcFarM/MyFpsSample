using System.Collections;
using UnityEngine;

namespace MyFps {
    public class HiddenDoor : MonoBehaviour {
        #region Variables
        [SerializeField] private GameObject door;
        [SerializeField] private string animParam;
        private bool flag = false;
        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        #endregion

        #region Custom Method
        public IEnumerator DoCheck() {
            while (!flag) {
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(CondCheck());
            }
        }
        IEnumerator CondCheck() {
            yield return new WaitForSeconds(0.1f);
            if (PlayerManager.Instance.HasKey(KeyType.KEY_LEFTPIC) &&
                PlayerManager.Instance.HasKey(KeyType.KEY_RIGHTPIC)) {
                flag = true;
                door.GetComponent<Animator>().SetTrigger(animParam);
            }
        }
        #endregion
    }
}