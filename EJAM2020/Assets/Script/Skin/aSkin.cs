using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class aSkin : MonoBehaviour
{
    IAMovement myIA;
    Player_Movement myPlayer;

    [Header("Parties du Skin")]
    public SpriteRenderer Corps;
    public SpriteRenderer Tête, EpauleDroite, BrasDroit, MainDroite, EpauleGauche, BrasGauche, MainGauche;

    // Start is called before the first frame update
    void Awake()
    {
        SpriteRenderer[] LesSprites = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < LesSprites.Length; i++)
        {
            switch (LesSprites[i].gameObject.name)
            {
                case "Corps":
                    Corps = LesSprites[i];
                    break;
                case "Tête":
                    Tête = LesSprites[i];
                    break;
                case "EpauleDroite":
                    EpauleDroite = LesSprites[i];
                    break;
                case "BrasDroite":
                    BrasDroit = LesSprites[i];
                    break;
                case "MainDroite":
                    MainDroite = LesSprites[i];
                    break;
                case "EpauleGauche":
                    EpauleGauche = LesSprites[i];
                    break;
                case "BrasGauche":
                    BrasGauche = LesSprites[i];
                    break;
                case "MainGauche":
                    MainGauche = LesSprites[i];
                    break;
            }
        }

        myIA = GetComponentInParent<IAMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSkin(IAMovement theIA)
    {
        if(theIA.myType == Type.Guard)
        {
            Corps.sprite = Resources.Load<Sprite>("Skins/Guard/Corps");
            Tête.sprite = Resources.Load<Sprite>("Skins/Guard/Tête");
            EpauleDroite.sprite = Resources.Load<Sprite>("Skins/Guard/Epaule");
            EpauleGauche.sprite = Resources.Load<Sprite>("Skins/Guard/Epaule");
            BrasDroit.sprite = Resources.Load<Sprite>("Skins/Guard/Bras");
            BrasGauche.sprite = Resources.Load<Sprite>("Skins/Guard/Bras");
            MainDroite.sprite = Resources.Load<Sprite>("Skins/Guard/Main");
            MainGauche.sprite = Resources.Load<Sprite>("Skins/Guard/Main");

            theIA.SkinChemin = "Skins/Guard/Main";
        }

        else if(theIA.myType == Type.Civilian)
        {
            DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Skins/Civil");
            DirectoryInfo[] info = dir.GetDirectories("*.*");
            int count = dir.GetDirectories().Length;

            int rnd = Random.Range(1, count + 1);

            Corps.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Corps");
            Tête.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Tête");
            EpauleDroite.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Epaule");
            EpauleGauche.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Epaule");
            BrasDroit.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Bras");
            BrasGauche.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Bras");
            MainDroite.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Main");
            MainGauche.sprite = Resources.Load<Sprite>("Skins/Civil/Civil_" + rnd + "/Main");

            theIA.SkinChemin = "Skins/Civil/Civil_" + rnd + "/Main";
        }
    }
}
