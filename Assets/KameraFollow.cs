using UnityEngine;

public class KameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform followObject;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Quaternion rotation;

    private void Start()
    {
        this.gameObject.transform.position = followObject.position + offset;
        this.gameObject.transform.rotation = rotation;
    }

    private void Update()
    {
        this.gameObject.transform.position = followObject.position + offset;
        this.gameObject.transform.rotation = rotation;
    }
}
