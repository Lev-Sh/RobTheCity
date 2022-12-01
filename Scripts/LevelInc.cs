using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Outline))]
public class LevelInc : MonoBehaviour
{
    public float incredibleRadius = 3f;
    public bool isfocus = false;
    GameObject previusObjectInteractable;
    private Outline outline;
    public Transform Noise;
    public houseowner mind;
    private bool isLight = true;
    public List<Light> isLightOn = new List<Light>();
    public List<Light> isLightOff = new List<Light>();
    public List<GameObject> EvilCameras = new List<GameObject>();

    void Start(){
        transform.tag = "Untagged";
    }
    void OnEnable()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0f;

    }
    public void OnMouseEnter()
    {
        outline.OutlineWidth = 5f;

    }
    public void OnMouseExit()
    {
        outline.OutlineWidth = 0;

    }
    public void OnMouseDown()
    {

        if (Mathf.Abs(Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position)) < incredibleRadius){
            if(isLight){
            Noise.transform.position = transform.position;
            mind.Noised = true;
            mind.changelights = true;
            ChangeLight();

            }


        }
    }
    public void ChangeLight(){
        isLight = !isLight;
        if(!isLight){
            LightOffVoid();
        }else{
            LightOnVoid();
        }
    }
    void LightOffVoid(){
            transform.tag = "Level";
            foreach (GameObject k in EvilCameras){
                k.GetComponent<EvilCamera>().On = false;
            }
            foreach (Light j in isLightOn){
                j.intensity = 5;
            }
            foreach (Light j in isLightOff){
                j.intensity = 0;
        }
    }
    public void LightOnVoid(){
            foreach (GameObject k in EvilCameras){
                k.GetComponent<EvilCamera>().On = true;
            }
            foreach (Light j in isLightOn){
                j.intensity = 0;
            }
            foreach (Light j in isLightOff){
                j.intensity = 1;
            }
        
    }
}
