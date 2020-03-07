using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals {
    public static float GetScreenSize(Camera camera) {
        return camera.orthographicSize * 1.5f;
    }
}
