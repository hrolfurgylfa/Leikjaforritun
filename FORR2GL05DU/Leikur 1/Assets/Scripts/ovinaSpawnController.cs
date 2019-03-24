using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ovinaSpawnController : MonoBehaviour {
    
    public GameObject spilari;
    public GameObject ovinur_prefab;
    public float spawnHradi = 10;
    public float erfidleikaStigsMagnariYfirTima = 10;
    public Transform[] spawnStadir;// Transform[] gerir lista svo að það er hægt að setja 3 eða 100 hluti í þennan reit
    
    private float timiSidastaSpawn = 0;

    // Update is called once per frame
    void Update() {
        if (Time.timeSinceLevelLoad > timiSidastaSpawn + 50 / spawnHradi) {// Þetta keyrir á hverjum 5 sekúndum ef spawnHraði er 10 og verður hraðara því sem spawnHradi er hærra
            timiSidastaSpawn = Time.timeSinceLevelLoad;// Þetta endurræsir tima síðasta spawn til þess að það sé alltaf jafn langt á milli spawn tíma
            spawnHradi += .1f * erfidleikaStigsMagnariYfirTima;// Þetta bætir við erfiðleikastigið og gerir það hraðar því sem erfidleikaStigsMagnariYfirTima er hærra
            buaTilOvin();
        }
    }
    
    void buaTilOvin () {// Þetta fall býr til óvin á einum af stöðunum sendum inn í scriptið í arrayinum
        Transform randomSpawnStadur = spawnStadir[Random.Range(0,spawnStadir.Length)];// Þetta setir 1 af spawn stodunum í breytuna randomSpawnStadur til þess að vera notaður seinna
        GameObject nyr_ovinur = Instantiate(ovinur_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;// Þetta býr til nýjann óvin úr prefabinu ovinur_prefab sem kom inn í skriftuna
        nyr_ovinur.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = spilari.transform;// Þetta stillir targetið á óvininum á spilarann
        nyr_ovinur.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().byrjunarStadsetning = randomSpawnStadur;// Þetta stillir byrjunarstadsetninguna á staðsetninguna valda fyrst í þessu falli
        nyr_ovinur.GetComponentInChildren<sverda_hitbox>().Spilari = spilari;// Þetta sendir spilarann inn í skriftu í óvininum sem þarf hann
    }
}
