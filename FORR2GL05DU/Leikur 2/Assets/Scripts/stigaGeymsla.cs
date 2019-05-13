using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stigaGeymsla : MonoBehaviour {
    public int stig;

    void Start() {
        DontDestroyOnLoad(this.gameObject);// Þetta passar að þessum gameObjecti sé ekki eytt þegar það er skipt um scene og þá geymir þessi gameObject stigin og valmyndin sækir þau svo héðan
    }
}
