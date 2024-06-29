using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Billboard class ensures that the game object always faces the camera.
/// This is typically used for UI elements or indicators in a 3D space that should be readable from any angle.
/// </summary>
public class Billboard : MonoBehaviour
{
    public Transform cam;

    /// <summary>
    /// LateUpdate is called after all Update functions have been called.
    /// This ensures that the billboard's rotation is updated after the camera has moved.
    /// </summary>
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
