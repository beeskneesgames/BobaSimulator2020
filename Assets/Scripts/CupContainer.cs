using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupContainer : MonoBehaviour {
    public GameObject arm;
    public GameObject cup;

    private void Start() {
        DontDestroyOnLoad(gameObject);
        Globals.cupContainer = this;
    }

    public void PrepareForGradeScreen() {
        Renderer cupRenderer = cup.GetComponent<Renderer>();
        Color cupColor = cupRenderer.material.color;
        cupRenderer.material.color = new Color(
            cupColor.r,
            cupColor.g,
            cupColor.b,
            0.5f
        );
        cup.transform.localPosition = Vector3.zero;
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
