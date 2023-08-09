using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowKnifeScript : MonoBehaviour
{
    public GameObject ThrowingKnife;
    public Transform KnifePos;
    public Transform CamPos;
    private float ThrowForce = 0;
    float MinForce = 10;
    float MaxForce = 30;
    public Slider ForceSlider;

    // Start is called before the first frame update
    void Start()
    {
        ThrowForce = MinForce;
        ForceSlider.minValue = MinForce;
        ForceSlider.maxValue = MaxForce;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Debug.Log("Enabled");
            ForceSlider.gameObject.SetActive(true);
            //ForceSlider.GetComponent<Animator>().SetBool("ShowSlider", true);
            if(ThrowForce < MaxForce)
            {
                ThrowForce += 10 * Time.deltaTime;
            }

            ForceSlider.value = ThrowForce;
        }

        if(Input.GetMouseButtonUp(0))
        {
            GameObject knife = Instantiate(ThrowingKnife, KnifePos.position, new Quaternion(transform.rotation.x, KnifePos.transform.rotation.y, ThrowingKnife.transform.rotation.z, transform.rotation.w));
            knife.GetComponent<Rigidbody>().AddForce(CamPos.transform.forward * ThrowForce, ForceMode.Impulse);
            ThrowForce = MinForce;
            ForceSlider.gameObject.SetActive(false);
        }
    }


}
