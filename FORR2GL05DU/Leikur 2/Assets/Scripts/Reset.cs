using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;// Þetta sæki ég til þess að geta skipt um senu

public class Reset : MonoBehaviour {
    public void SkiptaUmSenu(int numberOfSceneToChangeTo) {
        SceneManager.LoadScene(numberOfSceneToChangeTo);// Þetta skiptir um senu
    }
}
