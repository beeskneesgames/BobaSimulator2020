using UnityEngine;

public class CupContainer : MonoBehaviour {
    public GameObject arm;
    public GameObject cup;
    public GameObject straw;
    public IcePlacer icePlacer;

    private void Start() {
        DontDestroyOnLoad(gameObject);
        Globals.cupContainer = this;
    }

    public void PrepareForGradeScreen() {
        // Show the straw and pick a color for it
        straw.SetActive(true);

        // Make cup more transparent
        Renderer cupRenderer = cup.GetComponent<Renderer>();
        Color cupColor = cupRenderer.material.color;
        cupRenderer.material.color = new Color(
            cupColor.r,
            cupColor.g,
            cupColor.b,
            0.5f
        );

        // Move the cup to the center of the container
        cup.transform.localPosition = Vector3.zero;

        // Move the top layer of ice down a little so it looks like there's a
        // lid on the cup lol
        foreach (Ice ice in icePlacer.TopIceLayer) {
            ice.transform.localPosition = new Vector3(
                ice.transform.localPosition.x,
                ice.transform.localPosition.y - 1.03f,
                ice.transform.localPosition.z
            );
            Debug.Log(ice);
        }

        // Hide the arm and clear out unused components
        Destroy(arm);
        foreach (Transform child in transform) {
            ClearComponentsForGradeScreen(child.gameObject);
        }
    }

    private static void ClearComponentsForGradeScreen(GameObject obj) {
        foreach (Component component in obj.GetComponents<Component>()) {
            // Destroy every component on the GameObject except the ones we need
            // for the grade screen.
            if (!IsComponentForGradeScreen(component)) {
                Destroy(component);
            }
        }

        foreach (Transform child in obj.transform) {
            ClearComponentsForGradeScreen(child.gameObject);
        }
    }

    private static bool IsComponentForGradeScreen(Component component) {
        return
            component is Transform ||
            component is Renderer ||
            component is MeshFilter ||
            component is ClippingPlane;
    }
}
