using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningManager : MonoBehaviour
{
    public static TurningManager inst;
    public Animator animator;
    [SerializeField]
    public Model[] models;
    public int ModelIndex;
    public GameObject currentModelGO;

    [System.Serializable]
    public struct Model
    {
        public GameObject pre;
        public TurnType type;
    }

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        foreach (Model model in models)
        {
            model.pre.SetActive(false);
        }
        models[0].pre.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnTurnCamera(models[ModelIndex].type);
        }
    }

    public void OnTurnCamera(TurnType type)
    {
        animator.SetTrigger(type.ToString());
    }


    public void Reset()
    {
        models[ModelIndex].pre.SetActive(false);
        ModelIndex++;
        models[ModelIndex].pre.SetActive(true);
        Debug.Log("reset" + ModelIndex);

        /*
        GameObject g = currentModelGO;
        Destroy(g);
        Transform newModel = models[ModelIndex].pre.transform;
        currentModelGO = Instantiate(newModel.gameObject);
        currentModelGO.transform.SetParent(this.transform, true);
        currentModelGO.transform.localPosition = models[ModelIndex].pre.transform.position;
        currentModelGO.transform.localRotation = models[ModelIndex].pre.transform.rotation;
        */
    }
}

public enum TurnType
{
    turn90,
    turn180
}
