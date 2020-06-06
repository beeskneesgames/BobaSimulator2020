using UnityEngine;

public class CupController : MonoBehaviour {
    float screenSizeX;
    float buffer = 0.75f;
    float rightX;
    float leftX;
    Vector3 cameraPos;

    private void Start() {
        cameraPos = Camera.main.transform.position;
        screenSizeX = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.55f;
        rightX = cameraPos.x + screenSizeX - buffer;
        leftX = cameraPos.x - screenSizeX + buffer;
    }

    private void Update() {
        if (Globals.isPaused) {
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float xPosition = Mathf.Clamp(mousePosition.x, leftX, rightX);

        transform.position = new Vector3(
            xPosition,
            transform.position.y,
            transform.position.z
        );
    }
}
