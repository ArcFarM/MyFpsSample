using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    #region Variables
    float speed = 2.5f;
    public Vector3 direction = Vector3.right;
    private Vector3 startPosition;
    public float period = 2f;
    #endregion

    #region Properties
    #endregion

    #region Unity Event Method
    private void Start() {
        startPosition = transform.position;
        StartCoroutine(Move());
    }
    private void Update() {
        
    }
    #endregion

    #region Custom Method
    IEnumerator Move() {
        float elapsedTime = 0f;
        while(elapsedTime < period) {
            yield return null;
            elapsedTime += Time.deltaTime;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
        direction = -direction; // Reverse direction after moving for the period
        StartCoroutine(Move()); // Restart the movement
    }
    #endregion
}
