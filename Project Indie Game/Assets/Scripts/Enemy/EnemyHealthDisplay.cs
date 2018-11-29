using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthDisplay : MonoBehaviour {
    private Camera m_camera;
    [SerializeField]
    private Health m_health;
    [SerializeField]
    private Slider m_slider;

    private void Start()
    {
        m_health.OnHealthDecreased += ChangeHealthValue;
        m_camera = Camera.main;
        m_slider.maxValue = 100;
        m_slider.minValue = 0;
    }
    void OnEnable () {
        
        //m_health.OnHealthIncreased += ChangeHealthValue;
 
        m_slider.value = m_health.HP;
        transform.GetChild(0).gameObject.SetActive(false);

    }

    public void ChangeHealthValue(Health health)
    {
        StopAllCoroutines();
        m_slider.value = health.HP;
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(MakeCanvasEnabledFor(1f));
 
    }

    IEnumerator MakeCanvasEnabledFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        transform.GetChild(0).gameObject.SetActive(false);
    }

	void Update () {


        transform.LookAt(transform.position + m_camera.transform.rotation * Vector3.forward, 
            m_camera.transform.rotation * Vector3.up);
    }
}
