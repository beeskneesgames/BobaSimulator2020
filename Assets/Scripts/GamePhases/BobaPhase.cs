using System.Collections;
using System.Collections.Generic;

public class BobaPhase : GamePhase {
    public BobaSpawner bobaSpawner;
    public override float Time {
        get {
            return 10.0f;
        }
    }

    protected override void Execute() {
        bobaSpawner.gameObject.SetActive(true);
    }
}
