using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour {

    public Toggle toggle;

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0;
        float vertical = 0;

        horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        vertical = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

    #if UNITY_ANDROID

        horizontal = CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        vertical = CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime * 3.0f;

#endif

        if (!toggle.isOn)
        {
            transform.Rotate(0, horizontal, 0);
            transform.Translate(0, 0, vertical);
        }
        else
        {
            transform.Translate(horizontal / 30, 0, 0);
            transform.Translate(0, 0, vertical * (5f/3f));
        }
    }

    public void readjustRotation()
    {
        transform.rotation = Quaternion.identity;
    }
}
