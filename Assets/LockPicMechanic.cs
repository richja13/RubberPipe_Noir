using System.Threading;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockPicMechanic : MonoBehaviour
{
    public GameObject InnerLock;
    public GameObject LockPick;
    public int UnlockAngle;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        RandomLock();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (LockPick.transform.position);
		
		//Get the Screen position of the mouse
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
		
		//Get the angle between the points
		angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
    
		//Ta Daaa
        if(angle < -1 && angle > -179)
        {
		    LockPick.transform.localRotation =  Quaternion.Euler (new Vector3(0f,0f,angle));
        }

       
        if(Input.GetKey(KeyCode.A) && InnerLock.transform.eulerAngles.z < 358 )
        {
            InnerLock.transform.localEulerAngles = new Vector3(0,0,InnerLock.transform.localEulerAngles.z + 13f * Time.deltaTime);
        }

         if(Input.GetKey(KeyCode.D)  && InnerLock.transform.eulerAngles.z > 260)
        {
            InnerLock.transform.localEulerAngles = new Vector3(0,0,InnerLock.transform.localEulerAngles.z - 13f * Time.deltaTime);
        }
        else if(InnerLock.transform.localEulerAngles.z <= 260)
        {
            CheckIfUnlocked();
        }
        else if(InnerLock.transform.localEulerAngles.z < 358)
        {
            InnerLock.transform.localEulerAngles = new Vector3(0,0,InnerLock.transform.localEulerAngles.z + 1.3f);
        }

      
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

    void RandomLock()
    {
        UnlockAngle = Random.Range(-1, -179);
        Debug.Log("UnlockAngle: " + UnlockAngle);
    }

    void CheckIfUnlocked()
    {
        if(Mathf.FloorToInt(angle) < Mathf.FloorToInt(angle + 3) && Mathf.FloorToInt(angle) > Mathf.FloorToInt(angle - 3))
        {
            Debug.Log("UNLOCKED");
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Break Pin");
        }
    }

     void OnGUI(){
        GUI.Label(new Rect(Screen.width - 100,10,150,20), Mathf.RoundToInt(angle).ToString());
    }

}
