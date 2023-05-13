using UnityEngine;

public class Openable : MonoBehaviour {
    public Animator animator;

    public void Start() {
        animator.SetBool("IsOpened", false);
    }

    public void Open() {
        animator.SetBool("IsOpened", true);
    }

    public void Close() {
        animator.SetBool("IsOpened", false);
    }

    public void Toggle() {
        if(animator.GetBool("IsOpened")) {
            this.Close();
        } else {
            this.Open();
        }
    }
}