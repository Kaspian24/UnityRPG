using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages footsteps sounds
/// </summary>
public class FootStepScript : MonoBehaviour
{
    /// <summary>
    /// Class object Footstep
    /// </summary>
    public GameObject FootStep;
    /// <summary>
    /// Footsteps are set to false
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        FootStep.SetActive(false);
    }
    /// <summary>
    /// Plays sound when player presses the move key button and stops when the key is relesed
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown("w"))
            {
            FootStep.SetActive(true);
        }
        if (Input.GetKeyDown("a"))
        {
            FootStep.SetActive(true);
        }
        if (Input.GetKeyDown("s"))
        {
            FootStep.SetActive(true);
        }
        if (Input.GetKeyDown("d"))
        {
            FootStep.SetActive(true);
        }

        if (Input.GetKeyUp("w"))
        {
            FootStep.SetActive(false);
        }
        if (Input.GetKeyUp("a"))
        {
            FootStep.SetActive(false);
        }
        if (Input.GetKeyUp("s"))
        {
            FootStep.SetActive(false);
        }
        if (Input.GetKeyUp("d"))
        {
            FootStep.SetActive(false);
        }
    }
}
