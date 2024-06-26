using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepScript : MonoBehaviour
{
    public GameObject FootStep;
    // Start is called before the first frame update
    void Start()
    {
        FootStep.SetActive(false);
    }

    // Update is called once per frame
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
