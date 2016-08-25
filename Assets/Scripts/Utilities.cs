using UnityEngine;
using System.Collections;

public class Utilities : object {
    public static Vector2 GetDeltaVForce(float mass, Vector2 currentVelocity, Vector2 targetVelocity) {
        Vector2 deltaV = targetVelocity - currentVelocity;
        return (mass / Time.fixedDeltaTime) * deltaV;
    }
}
