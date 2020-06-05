using UnityEngine;

public class CupController : MonoBehaviour {
    private void Update() {
        if (Globals.isPaused) {
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float screenSizeX = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        Vector3 cameraPos = Camera.main.transform.position;

        float buffer = 0.75f;
        float rightX = cameraPos.x + screenSizeX - buffer;
        float leftX = cameraPos.x - screenSizeX + buffer;
        float xPosition = mousePosition.x;

        if (mousePosition.x > 0) {
            if (mousePosition.x >= rightX) {
                xPosition = rightX;
            }
        } else {
            if (mousePosition.x <= leftX) {
                xPosition = leftX;
            };
        }

        transform.position = new Vector3(
            xPosition,
            transform.position.y,
            transform.position.z
        );
    }
}
